using System;
using UnityEngine;

// ****************************************************************
// 功能：启程MVC框架控制器
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class StartUpCommand : Controller {

    /*  因为视图不是一直存在的,是根据场景的不同在变化的
     *  所以应该在进入场景的时候注册视图才比较合适,而不是在这里
     */
    public override void Execute(object arg) {
        // 注册模型 (Model)
        RegisterModel(new GameModel());
        RegisterModel(new RoundModel());

        // 注册命令 (Controller)
        RegisterController(Consts.E_EnterScene, typeof(EnterSceneCommand));
        RegisterController(Consts.E_ExitScene, typeof(ExitSceneCommand));
        RegisterController(Consts.E_LoadScene, typeof(LoadSceneCommand));
        RegisterController(Consts.E_StartLevel, typeof(StartLevelCommand));
        RegisterController(Consts.E_EndLevel, typeof(EndLevelCommand));
        RegisterController(Consts.E_CountdownComplete, typeof(CountdownCompleteCommand));

        RegisterController(Consts.E_UpgradeTower, typeof(UpgradeTowerCommand));
        RegisterController(Consts.E_SellTower, typeof(SellTowerCommand));

        //初始化 (连接服务器，加载静态数据。。。)
        GetModel<GameModel>().Initialize();

        // 进入开始界面
        Game.Instance.SceneMgr.LoadScene(new SceneArgs(Consts.Start));
    }
}

