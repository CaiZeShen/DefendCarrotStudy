using System;
using UnityEngine;

// ****************************************************************
// 功能：MVC框架控制器基类
// 创建：蔡泽深
// 时间：2017/06/01
// 修改内容：										修改者姓名：
// ****************************************************************

public abstract class Controller {
    // 处理系统消息
    public abstract void Execute(object args);

    // 获取模型
    protected T GetModel<T>() where T : Model {
        return MVC.GetModel<T>();
    }

    // 获取视图
    protected T GetView<T>() where T : View {
        return MVC.GetView<T>();
    }

    protected void RegisterModel(Model model) {
        MVC.RegisterModel(model);
    }

    protected void RegisterView(View view) {
        MVC.RegisterView(view);
    }

    protected void RegisterController(string eventName, Type controllerType) {
        MVC.RegisterController(eventName, controllerType);
    }
}

