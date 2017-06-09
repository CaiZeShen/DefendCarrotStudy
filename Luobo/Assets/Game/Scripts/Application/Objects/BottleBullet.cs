using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/09
// 修改内容：										修改者姓名：
// ****************************************************************

public class BottleBullet : Bullet {
    public const float ClosedDistance=0.2f;

    private Monster target;

    public void Load(int bulletID, int level, Rect mapRect, Monster target) {
        base.Load(bulletID, level, mapRect);

        this.target = target;
    }

    #region Unity Lifecycle
    private void Update() {
        Move();
    } 
    #endregion

    private void LookAt() {
        Vector3 dir = target.transform.position - transform.position;
        float dx = dir.x;
        float dy = dir.y;

        // 计算夹角[-180,180]
        float angles = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        // 得到目标欧拉角
        Vector3 eulerAngers = transform.eulerAngles;
        eulerAngers.z = angles - 90f;
        transform.eulerAngles = eulerAngers;
    }

    private void Move() {
        if (isExploded) {
            return;
        }

        if (target != null && !target.IsDead) {
            LookAt();

            // 帧移动
            transform.Translate(Vector3.up * Time.deltaTime * Speed, Space.Self);

            // 打中
            if (Vector3.Distance(transform.position, target.transform.position) <= ClosedDistance) {
                target.Damage((int)Attack);
                Explode();
            }
        } else {
            // 帧移动
            transform.Translate(Vector3.up * Time.deltaTime * Speed, Space.Self);

            // 边间检测
            if (!isExploded && !MapRect.Contains(transform.position)) {
                Explode();
            }
        }
    }
}

