using System;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/09
// 修改内容：										修改者姓名：
// ****************************************************************

public class MapTileClickCommand : Controller {
    public override void Execute(object args) {
        MapTileClickArgs e = args as MapTileClickArgs;
        GameModel gameModel = GetModel<GameModel>();
        TowerPopup towerPopup = GetView<TowerPopup>();
        Spawner spawner = GetView<Spawner>();

        if (!gameModel.IsPlaying) {
            return;
        }

        Debug.Log(towerPopup.IsPopShow);
        if (towerPopup.IsPopShow) {
            towerPopup.HidePopups();
            return;
        }

        if (!e.tile.canHold) {
            towerPopup.HidePopups();
            return;
        }

        Tile tile = e.tile;

        if (tile.tower == null) {
            Vector3 position = e.map.GetPosition(tile);
            bool upSide = tile.y < (Map.RowCount / 2);
            Debug.Log("haha");
            towerPopup.ShowSpawnPanel(position, upSide);
        } else {
            towerPopup.ShowUpgradePanel(tile.tower);
        }
    }
}

