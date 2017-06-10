using System;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/07
// 修改内容：										修改者姓名：
// ****************************************************************

public class UpgradeTowerCommand : Controller {
    public override void Execute(object args) {
        UpgradeTowerArgs uTArgs = args as UpgradeTowerArgs;
        GameModel gm = GetModel<GameModel>();

        if (gm.Gold>=uTArgs.tower.UpgradePrice) {
            // 扣钱
            gm.Gold -= uTArgs.tower.UpgradePrice;

            // 升级
            uTArgs.tower.Level++;
        }
        
    }
}

