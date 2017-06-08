using UnityEngine;

// ****************************************************************
// 功能：回合信息
// 创建：蔡泽深
// 时间：2017/06/01
// 修改内容：										修改者姓名：
// ****************************************************************

public class Round {
    public int monster;     // 怪物ID
    public int count;       // 怪物数量

    public Round(int monster, int count) {
        this.monster = monster;
        this.count = count;
    }
}

