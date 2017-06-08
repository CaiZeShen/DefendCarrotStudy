using System;
using UnityEngine;

// ****************************************************************
// 功能：怪物脚本
// 创建：蔡泽深
// 时间：2017/06/06
// 修改内容：										修改者姓名：
// ****************************************************************

public class Monster : Role {
    public const float CloseDistance = 0.1f;      // 非常接近的距离

    public event Action<Monster> readched;
    public MonsterType monsterType;
    private float moveSpeed=0;
    private Vector3[] roadPoints;   // 路点集合
    private int pointIndex=0;      // 当前拐点索引
    private bool isReached=false;

    public float MoveSpeed {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public void LoadRoadPath(Vector3[] path) {
        roadPoints = path;
        // 放置在起点
        transform.position = roadPoints[0];
    }

    public override void OnSpawn() {
        base.OnSpawn();

        MonsterInfo info = Game.Instance.StaticData.GetMonsterInfo((int)monsterType);
        MaxHP = info.hp;
        HP = info.hp;
        MoveSpeed = info.moveSpeed;
    }

    public override void OnUnspawn() {
        base.OnUnspawn();
        // 还原
        while (readched != null) {
            readched -= readched;
        }

        roadPoints = null;
        isReached = false;
        pointIndex = 0;
        moveSpeed = 0;
    }

    #region Unity Lifecycle
    private void Update() {
        Move();
    }
    #endregion

    // 帧移动
    private void Move() {
        // 到达了终点
        if (isReached) {
            return;
        }

        // 当前位置
        Vector3 pos = transform.position;

        // 目标位置
        Vector3 dest = roadPoints[pointIndex];

        // 计算距离
        float distance = Vector3.Distance(pos, dest);
        if (distance < CloseDistance) {
            // 到达拐点
            transform.position = dest;

            if (Next()) {
                // 朝向目标方向
                Vector3 direction = (roadPoints[pointIndex] - transform.position).normalized;
                Vector3 euler = transform.eulerAngles;
                euler.y = direction.x >= 0 ? 0 : 180;
                transform.eulerAngles = euler;
            } else {
                // 到达终点
                isReached = true;

                // 触发事件
                if (readched != null) {
                    readched(this);
                }
            }
        } else {
            // 移动方向
            Vector3 direction = (dest - pos).normalized;

            // 进行帧移动
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    private bool Next() {
        bool ret = false;
        if (++pointIndex < roadPoints.Length) {
            ret = true;
        }

        return ret;
    }
}

