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

    public override void RegisterEvents() {
        attentionEvnets.Add(Consts.E_ShowSpawnPanel);
        attentionEvnets.Add(Consts.E_ShowUpgradePanel);
        attentionEvnets.Add(Consts.E_HidePopups);
    }

    public override void HandleEvent(string eventName, object args) {
        switch (eventName) {
            case Consts.E_ShowSpawnPanel:
                ShowSpawnPanelArgs spawnArgs = args as ShowSpawnPanelArgs;
                ShowSpawnPanel(spawnArgs.position, spawnArgs.upSide);
                break;
            case Consts.E_ShowUpgradePanel:
                ShowUpgradePanelArgs upgradeArgs = args as ShowUpgradePanelArgs;
                ShowUpgradePanel(upgradeArgs.tower);
                break;
            case Consts.E_HidePopups:
                HidePopups();
                break;
            default:
                break;
        }
    }

    #region Unity Lifecycle
    private void Awake() {
        spawnPanel = GetComponentInChildren<SpawnPanel>();
        upgradePanel = GetComponentInChildren<UpgradePanel>();

        gm = GetModel<GameModel>();
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

    private void ShowSpawnPanel(Vector3 pos,bool upSide) {
        HidePopups();

        spawnPanel.Show(gm, pos, upSide);
    }

    private void ShowUpgradePanel(Tower tower) {
        HidePopups();

        upgradePanel.Show(tower, gm);
    }

    private void HidePopups() {
        spawnPanel.Hide();
        upgradePanel.Hide();
    }
}

