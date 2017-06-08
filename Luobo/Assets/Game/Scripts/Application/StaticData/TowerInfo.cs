using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/07
// 修改内容：										修改者姓名：
// ****************************************************************

public struct TowerInfo {
    public int id;
    public int useBulletID;             // 使用子弹的id
    public int basePrice;
    public int maxLevel;
    public int shotRate;                // 每秒发射的子弹数
    public float guardRange;            // 守卫范围
    public string normalIcon;
    public string disableIcon;
    public string prefabName;
}

