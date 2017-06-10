using System;
using System.Collections;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/09
// 修改内容：										修改者姓名：
// ****************************************************************

public abstract class Bullet : ReusableObject {
    public float DelayToDestory = 1f;
    protected bool isExploded = false;
    protected Animator animator;
    private int level;

    public int ID { get; private set; }

    public int Level {
        get { return level; }
        set {
            level = value;
            transform.localScale = Vector3.one * (1 + (level - 1) * 0.15f);
        }
    }

    public float BaseSpeed { get; private set; }

    public float BaseAttack { get; private set; }

    public float Speed {
        get {
            return BaseSpeed * (1 + (Level - 1) * 0.2f);
        }
    }

    public float Attack { get { return BaseAttack * Level; } }

    public Rect MapRect { get; private set; }

    #region Unity Lifecycle
    protected virtual void Awake() {
        animator = GetComponent<Animator>();
    } 
    #endregion

    public override void OnSpawn() {
        animator.Play("Play");

    }

    public override void OnUnspawn() {
        isExploded = false;
        animator.ResetTrigger("IsExplode");
    }

    public void Explode() {
        isExploded = true;
        animator.SetTrigger("IsExplode");
        Invoke("DestoryMyself", DelayToDestory);
    }

    protected void Load(int bulletID, int level, Rect mapRect) {
        MapRect = mapRect;

        ID = bulletID;
        Level = level;

        BulletInfo info = Game.Instance.StaticData.GetBulletInfo(bulletID);
        BaseSpeed = info.baseSpeed;
        BaseAttack = info.baseAttack;
    }

    private void DestoryMyself() {
        Game.Instance.ObjectPool.Unspawn(gameObject);
    }
}

