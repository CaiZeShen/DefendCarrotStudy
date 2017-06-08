using UnityEngine;

// ****************************************************************
// 功能：格子信息
// 创建：蔡泽深
// 时间：2017/06/01
// 修改内容：										修改者姓名：
// ****************************************************************

public class Tile {
    public int x;
    public int y;
    public bool canHold;    // 是否可以放置炮塔
    public object data;     // 格子所保存的数据

    public Tile(int x,int y) {
        this.x = x;
        this.y = y;
    }

    public override string ToString() {
        return string.Format("[X:{0},Y:{1},canHold:{2}]", x, y, canHold);
    }
}

