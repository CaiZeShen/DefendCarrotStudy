using UnityEngine;
using System.Collections.Generic;

// ****************************************************************
// 功能：MVC框架视图基类
// 创建：蔡泽深
// 时间：2017/06/01
// 修改内容：										修改者姓名：
// ****************************************************************

public abstract class View :MonoBehaviour{
    // 保存感兴趣的事件
    protected List<string> attentionEvnets = new List<string>();

    // 视图标识
    public abstract string Name { get; }

    public bool ContainsEvent(string eventName) {
        return attentionEvnets.Contains(eventName);
    }

    // 事件处理函数
    public abstract void HandleEvent(string eventName, object args);

    // 注册关心的事件
    public virtual void RegisterEvents() {}

    // 获取模型
    protected T GetModel<T>() where T : Model {
        return MVC.GetModel<T>();
    }

    // 发送消息
    protected void SendEvent(string eventName, object args = null) {
        MVC.SendEvent(eventName, args);
    }
}

