using System;
using UnityEngine;

// ****************************************************************
// 功能：加载场景命令
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class LoadSceneCommand : Controller {
    public override void Execute(object args) {
        SceneArgs sceneArgs = args as SceneArgs;
        Game.Instance.SceneMgr.LoadScene(sceneArgs);
    }
}

