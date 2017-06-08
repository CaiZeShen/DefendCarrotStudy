using System;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class StartLevelCommand : Controller {
    public override void Execute(object args) {
        StartLevelArgs startLevelArgs = args as StartLevelArgs;

        GameModel gameModel = GetModel<GameModel>();
        gameModel.StartLevel(startLevelArgs.LevelIndex);

        RoundModel roundModel = GetModel<RoundModel>();
        roundModel.LoadLevel(gameModel.PlayLevel);
        
        // 进入游戏场景
        Game.Instance.SceneMgr.LoadScene(new SceneArgs(Consts.Level));
    }
}

