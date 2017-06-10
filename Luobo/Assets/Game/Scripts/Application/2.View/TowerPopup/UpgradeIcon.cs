using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/07
// 修改内容：										修改者姓名：
// ****************************************************************

public class UpgradeIcon : MonoBehaviour {
    private SpriteRenderer render;
    private Tower tower;
    private bool canUpgrade;

    public void Load(Tower tower,GameModel gm) {
        // 保存数据
        this.tower = tower;
        canUpgrade = false;

        // 加载图片
        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(tower.ID);

        // 默认为顶级图片
        string icon = "upgrade_Top.png";

        // 不是顶级再改
        if (!tower.IsTopLevel) {
            if (gm.Gold >= tower.UpgradePrice) {
                icon = "upgrade_180.png";
                canUpgrade = true;
            } else {
                icon= "upgrade_-180.png";
            }
        } 

        render.sprite = ResourcesMgr.Instance.Load<Sprite>(Consts.TowerIconResDir+icon);
    }

    #region Unity Lifecycle
    private void Awake() {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown() {
        if (canUpgrade) {
            SendMessageUpwards("OnUpgradeTower", tower, SendMessageOptions.RequireReceiver);
        }
    }
    #endregion
}

