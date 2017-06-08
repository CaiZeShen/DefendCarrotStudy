using System;
using UnityEngine;

// ****************************************************************
// 功能：管理底下的icon
// 创建：蔡泽深
// 时间：2017/06/07
// 修改内容：										修改者姓名：
// ****************************************************************

public class UpgradePanel : MonoBehaviour {
    private UpgradeIcon upgradeIcon;
    private SellIcon sellIcon;
    private Tower tower;

    public void Show(Tower tower,GameModel gm) {
        // 保存数据
        this.tower = tower;

        // 位置
        transform.position = tower.transform.position;

        //加载图片
        upgradeIcon.Load(tower,gm);
        sellIcon.Load(tower);

        // 显示
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    #region Unity Lifecycle
    private void Awake() {
        upgradeIcon = GetComponentInChildren<UpgradeIcon>();
        sellIcon = GetComponentInChildren<SellIcon>();
    }

    private void OnMouseDown() {
        SendMessageUpwards("OnUpgradeTower",tower, SendMessageOptions.RequireReceiver);
    }
    #endregion
}

