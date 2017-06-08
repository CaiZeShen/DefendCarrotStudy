using System;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/08
// 修改内容：										修改者姓名：
// ****************************************************************

public class Bottle : Tower {

    public override void Attack() {
        // 播放动画
        animator.SetTrigger("Fire");

        // 声音

    } 
}

