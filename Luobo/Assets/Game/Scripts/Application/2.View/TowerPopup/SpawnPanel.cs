using System;
using UnityEngine;

// ****************************************************************
// 功能：管理TowerIcon
// 创建：蔡泽深
// 时间：2017/06/07
// 修改内容：										修改者姓名：
// ****************************************************************

public class SpawnPanel : MonoBehaviour {
    private TowerIcon[] towerIcons;

    public void Show(GameModel gm, Vector3 pos,bool upSide) {
        // 动态加载图标
        for (int i = 0; i < towerIcons.Length; i++) {
            TowerInfo info = Game.Instance.StaticData.GetTowerInfo(i);
            towerIcons[i].Load(gm, info, pos, upSide);
        }

        // 设置位置
        transform.position = pos;

        // 显示
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    #region Unity Lifecycle
    private void Awake() {
        towerIcons = transform.GetComponentsInChildren<TowerIcon>();
    } 
    #endregion
}

