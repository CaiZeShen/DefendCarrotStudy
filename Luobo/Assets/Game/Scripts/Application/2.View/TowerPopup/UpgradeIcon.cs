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

    public void Load(Tower tower,GameModel gm) {
        // 保存数据
        this.tower = tower;

        // 加载图片
        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(tower.ID);
        string icon = "upgrade_Top.png";
        if (!tower.IsTopLevel) {
            icon = gm.Gold >= (info.basePrice / 2) ? "upgrade_180.png" : "upgrade_-180.png";
        }
        render.sprite = ResourcesMgr.Instance.Load<Sprite>(Consts.TowerIconResDir+icon);
    }

    #region Unity Lifecycle
    private void Awake() {
        render = GetComponent<SpriteRenderer>();
    } 
    #endregion
}

