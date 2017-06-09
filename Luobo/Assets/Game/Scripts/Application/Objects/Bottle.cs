using System;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/08
// 修改内容：										修改者姓名：
// ****************************************************************

public class Bottle : Tower {
    private Transform shotPoint;

    protected override void Awake() {
        base.Awake();
        shotPoint = transform.GetChild(0).Find("ShotPoint");
    }

    public override void Attack() {
        // 播放动画
        animator.SetTrigger("Fire");

        BulletInfo Info= Game.Instance.StaticData.GetBulletInfo(UseBulletID);
        BottleBullet bullet = Game.Instance.ObjectPool.Spawn(Info.prefabName).GetComponent<BottleBullet>();

        bullet.transform.position = shotPoint.position;

        bullet.Load(UseBulletID, Level, mapRect, target);
    } 
}

