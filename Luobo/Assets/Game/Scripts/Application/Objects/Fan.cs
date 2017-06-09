using System;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/08
// 修改内容：										修改者姓名：
// ****************************************************************

public class Fan : Tower {
    private int bulletCount = 6;

    public override void Attack() {
        // 播放动画
        animator.SetTrigger("Fire");

        for (int i = 0; i < bulletCount; i++) {
            // 算出当前角度(弧度为单位)
            float radian = 2 * Mathf.PI / bulletCount * i;

            // 求当前角度的向量
            Vector3 dir = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0);

            // 产生子弹
            BulletInfo info = Game.Instance.StaticData.GetBulletInfo(UseBulletID);
            FanBullet bullet = Game.Instance.ObjectPool.Spawn(info.prefabName).GetComponent<FanBullet>();
            bullet.transform.position = transform.position;
            bullet.Load(UseBulletID, Level, mapRect, dir);
        }
    }
}

