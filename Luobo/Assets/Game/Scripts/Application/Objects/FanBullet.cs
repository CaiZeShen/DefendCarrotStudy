using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/09
// 修改内容：										修改者姓名：
// ****************************************************************

public class FanBullet : Bullet {
    private const float CloseDistance = 0.6f;
    private const float rotateSpeed=180f; 
    private Vector3 dir;

    public void Load(int bulletID,int level,Rect mapRect,Vector3 dir) {
        base.Load(bulletID, level, mapRect);

        this.dir = dir;
    }

    private void Update() {
        Move();
    }

    private void Move() {
        if (isExploded) {
            return;
        }

        // 自转
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime, Space.Self);

        // 向指定方向移动
        transform.Translate(dir * Speed * Time.deltaTime, Space.World);

        // 检测与怪物的碰撞
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject go in monsters) {
            Monster m = go.GetComponent<Monster>();
            if (m.IsDead) {
                continue;
            }

            if (Vector3.Distance( m.transform.position,transform.position)<=CloseDistance) {
                m.Damage((int)Attack);
                Explode();
                break;
            }
        }

        // 边界检测
        if (!isExploded && !MapRect.Contains(transform.position)) {
            Explode();
        }
    }
}

