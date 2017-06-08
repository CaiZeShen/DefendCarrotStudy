using System;
using UnityEngine;

// ****************************************************************
// 功能：MVC入口类
// 创建：蔡泽深
// 时间：2017/06/01
// 修改内容：										修改者姓名：
// ****************************************************************

public abstract class ApplicationBase<T> :SingletonMono<T> where T : MonoBehaviour {
    protected void RegisterController(string eventName,Type controllerType) {
        MVC.RegisterController(eventName, controllerType);
    }

    protected void SendEvent(string eventName,object args=null) {
        MVC.SendEvent(eventName, args);
    }
}

