using System;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/07
// 修改内容：										修改者姓名：
// ****************************************************************

public class TowerPopup : View {
    private SpawnPanel spawnPanel;
    private UpgradePanel upgradePanel;
    private GameModel gm;

    public override string Name {
        get {
            return Consts.V_TowerPopup;
        }
    }

    public bool IsPopShow {
        get {
            if (spawnPanel.gameObject.activeSelf) {
                return true;
            } else if(upgradePanel.gameObject.activeSelf) {
                return true;
            }

            return false;
        }
    }

    public void HidePopups() {
        spawnPanel.Hide();
        upgradePanel.Hide();
    }

    public void ShowSpawnPanel(Vector3 pos, bool upSide) {
        HidePopups();

        spawnPanel.Show(gm, pos, upSide);
    }

    public void ShowUpgradePanel(Tower tower) {
        HidePopups();

        upgradePanel.Show(tower, gm);
    }

    public override void HandleEvent(string eventName, object args) {

    }

    #region Unity Lifecycle
    private void Awake() {
        spawnPanel = GetComponentInChildren<SpawnPanel>();
        upgradePanel = GetComponentInChildren<UpgradePanel>();

        gm = GetModel<GameModel>();
    }

    private void Start() {
        HidePopups();
    }
    #endregion

    #region 由消息冒泡触发的回调
    private void OnSpawnTower(object[] args) {
        int towerID = (int)args[0];
        Vector3 pos = (Vector3)args[1];

        SendEvent(Consts.E_SpawnTower, new SpawnTowerArgs { towerID = towerID, pos = pos });
        spawnPanel.Hide();
    }

    private void OnUpgradeTower(Tower tower) {
        SendEvent(Consts.E_UpgradeTower, new UpgradeTowerArgs { tower = tower });
    }

    private void OnSellTower(Tower tower) {
        SendEvent(Consts.E_SellTower, new UpgradeTowerArgs { tower = tower });
    }
    #endregion
}

