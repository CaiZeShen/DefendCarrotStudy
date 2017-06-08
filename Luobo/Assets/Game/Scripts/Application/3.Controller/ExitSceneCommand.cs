using System;
using UnityEngine;

// ****************************************************************
// 功能：退出场景
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class ExitSceneCommand : Controller {
    public override void Execute(object arg) {
        // 离开场景前回收所以对象
        Game.Instance.ObjectPool.UnspawnAll();
    }
}

