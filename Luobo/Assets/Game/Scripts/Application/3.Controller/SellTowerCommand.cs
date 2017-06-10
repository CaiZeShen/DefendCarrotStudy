using System;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/07
// 修改内容：										修改者姓名：
// ****************************************************************

public class SellTowerCommand : Controller {
    public override void Execute(object args) {
        SellTowerArgs e = args as SellTowerArgs;
        GameModel gm = GetModel<GameModel>();

        // 回收钱
        gm.Gold += e.tower.SellPrice;

        // 回收塔
        Game.Instance.ObjectPool.Unspawn(e.tower.gameObject);
    }
}

