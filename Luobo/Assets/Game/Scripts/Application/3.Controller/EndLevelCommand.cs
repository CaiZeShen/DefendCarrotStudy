using System;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class EndLevelCommand : Controller {
    public override void Execute(object args) {
        EndLevelArgs endLevelArgs = args as EndLevelArgs;
        // 结束回合
        RoundModel rm = GetModel<RoundModel>();
        rm.StopRounds();

        // 保存游戏状态
        GameModel gm = GetModel<GameModel>();
        gm.StopLevel(endLevelArgs.IsWin);

        // 弹出ui
        if (endLevelArgs.IsWin) {
            GetView<UIWin>().Show();
        } else {
            GetView<UILose>().Show();
        }
    }
}

