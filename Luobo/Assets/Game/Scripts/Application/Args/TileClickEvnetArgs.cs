using UnityEngine;
using System;

// ****************************************************************
// 功能：格子点击事件信息
// 创建：蔡泽深
// 时间：2017/06/02
// 修改内容：										修改者姓名：
// ****************************************************************

public class TileClickEvnetArgs : EventArgs {
    public int mouseButton;             // 0: 鼠标左键  1: 鼠标右键
    public Tile tile;

    public TileClickEvnetArgs(int mouseButton, Tile tile) {
        this.mouseButton = mouseButton;
        this.tile = tile;
    }
}

