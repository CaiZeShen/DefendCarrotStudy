using UnityEngine;
using System.Collections.Generic;

// ****************************************************************
// 功能：关卡信息
// 创建：蔡泽深
// 时间：2017/06/01
// 修改内容：										修改者姓名：
// ****************************************************************

public class Level {
    public string name;                                 // 名字
    public string background;                           // 背景图片
    public string road;                                 // 路径图
    public string cardImage;                            // 卡片图
    public int initSocre;                               // 初始金币
    public List<Point> holders=new List<Point>();       // 炮塔可放置的位置
    public List<Point> roadPoints = new List<Point>();  // 怪物行走的路径
    public List<Round> rounds=new List<Round>();        // 出怪回合信息
}

