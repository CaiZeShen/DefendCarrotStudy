using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/07
// 修改内容：										修改者姓名：
// ****************************************************************

public class SellIcon : MonoBehaviour {
    private Tower tower;

    public void Load(Tower tower) {
        // 保存数据
        this.tower = tower;

    }

    private void OnMouseDown() {
        // 消息冒泡
        SendMessageUpwards("OnSellTower", tower, SendMessageOptions.RequireReceiver);
    }
}

