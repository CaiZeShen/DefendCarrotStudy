using UnityEngine;
using System.Collections.Generic;
using System;

// ****************************************************************
// 功能：MVC框架管理类
// 创建：蔡泽深
// 时间：2017/06/01
// 修改内容：										修改者姓名：
// ****************************************************************

public static class MVC {
    // 存储MVC
    private static Dictionary<string, Model> models = new Dictionary<string, Model>();     // 名字标识---模型
    private static Dictionary<string, View> views = new Dictionary<string, View>();        // 名字标识---视图
    private static Dictionary<string, Type> commandMap = new Dictionary<string, Type>();  // 事件名字---控制器类型(事件与控制器存在映射关系)

    #region Model 方法
    public static void RegisterModel(Model model) {
        if (models.ContainsKey(model.Name)) {
            throw new UnityException(model.Name + "的模型已经注册!");
        }

        models.Add(model.Name, model);
    }

    public static void UnRegisterModel(Model moel) {
        if (models.ContainsKey(moel.Name))
            models.Remove(moel.Name);
    }

    public static T GetModel<T>() where T : Model {
        foreach (Model model in models.Values) {
            if (model is T) {
                return model as T;
            }
        }

        return null;
    }
    #endregion

    #region View 方法
    /* 因为视图不是一直存在的,是根据场景的不同在变化的,所以应该在进入场景的时候注册视图才比较合适.View脚本所依赖的GameObject只有在当前场景存在,切换到别的视图就会被销毁,
     * 但是View对象依然存在内存中,此时如果有个View关心的事件发出,那么View的执行结果就会报错,因为View的功能是显示,而显示相关的对象都因为切换场景销毁了.重复进入一个场景时
     * 注册的View对象,名字虽然一样,但对象其实是不一样的.因此有必要在切换场景前取消注册当前场景的View视图,那样做要写多点代码,还有一个方法就是如下方法:在注册的时候判断是否
     * 已经注册过该视图,有就删除保存的视图(因为那个View对象所关联的GameObject已经被销毁了,无法正常使用,直接删除引用,等待被c#回收机制回收即可),重新注册.
    */
    public static void RegisterView(View view) {
        if (views.ContainsKey(view.Name)) {
            views.Remove(view.Name);
        }

        view.RegisterEvents();
        views.Add(view.Name, view);
    }

    public static void UnRegisterView(View view) {
        if (views.ContainsKey(view.Name)) {
            views.Remove(view.Name);
        }
    }

    public static T GetView<T>() where T : View {
        foreach (View view in views.Values) {
            if (view is T) {
                return view as T;
            }
        }

        return null;
    }
    #endregion

    #region Controller 方法
    public static void RegisterController(string eventName, Type controllerType) {
        if (commandMap.ContainsKey(eventName)) {
            throw new UnityException("一个事件只能对应一个控制器类型!");
        }

        commandMap.Add(eventName, controllerType);
    }

    public static void UnRegisterController(string eventName) {
        if (commandMap.ContainsKey(eventName))
            commandMap.Remove(eventName);
    }

    // 发送事件
    public static void SendEvent(string eventName, object args = null) {
        // 控制器响应事件
        if (commandMap.ContainsKey(eventName)) {
            // 反射创建实例
            Type controllerType = commandMap[eventName];
            Controller controller = Activator.CreateInstance(controllerType) as Controller;
            // 控制器执行
            controller.Execute(args);
        }

        // 视图响应事件
        foreach (View v in views.Values) {
            if (v.ContainsEvent(eventName)) {
                v.HandleEvent(eventName, args);
            }
        }
    }
    #endregion
}

