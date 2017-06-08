using System;
using UnityEngine;

// ****************************************************************
// 功能：进入场景
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class EnterSceneCommand : Controller {
    public override void Execute(object args) {
        SceneArgs sceneArgs = args as SceneArgs;

        // 注册视图 (View)
        switch (sceneArgs.Name) {
            case Consts.Start:
                RegisterView(GameObject.Find("UIStart").GetComponent<UIStart>());
                break;
            case Consts.Select:
                RegisterView(GameObject.Find("UISelect").GetComponent<UISelect>());
                break;
            case Consts.Level:
                Transform levelCanvasT = GameObject.Find("LevelCanvas").transform;
                RegisterView(GameObject.Find("Map").GetComponent<Spawner>());
                RegisterView(levelCanvasT.Find("UIBoard").GetComponent<UIBoard>());
                RegisterView(levelCanvasT.Find("UICountdown").GetComponent<UICountdown>());
                RegisterView(levelCanvasT.Find("UIWin").GetComponent<UIWin>());
                RegisterView(levelCanvasT.Find("UILose").GetComponent<UILose>());
                RegisterView(levelCanvasT.Find("UISystem").GetComponent<UISystem>());
                RegisterView(GameObject.Find("TowerPopup").GetComponent<TowerPopup>());
                break;
            case Consts.Complete:
                RegisterView(GameObject.Find("UIComplete").GetComponent<UIComplete>());
                break;
        }
    }
}

