using System;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class CountdownCompleteCommand : Controller {
    public override void Execute(object args) {
        GameModel gm = GetModel<GameModel>();
        gm.IsPlaying = true;

        RoundModel roundModel = GetModel<RoundModel>();
        roundModel.StartRounds();
    }
}

