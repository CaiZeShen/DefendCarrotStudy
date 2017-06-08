using System;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/07
// 修改内容：										修改者姓名：
// ****************************************************************

[RequireComponent(typeof(CircleCollider2D))]
public abstract class Tower : ReusableObject {
    protected Transform battery;            // 炮台
    protected Animator animator;
    protected Monster target;
    protected int level = 0;
    protected Tile tile;
    protected CircleCollider2D circleCollider;
    protected float guardRange = 0;
    protected float lastAttackTime = 0;

    public int ID { get; private set; }

    public int Price {
        get { return BasePrice * (int)(1 + Level * 0.5f); }
    }

    public int BasePrice { get; private set; }

    public int Level {
        get { return level; }

        set {
            level = Mathf.Clamp(value, 0, MaxLevel);

            // 根据级别设置大小
            transform.localScale = Vector3.one * (1 + level * 0.15f);
        }
    }

    public bool IsTopLevel {
        get { return Level >= MaxLevel; }
    }

    public int MaxLevel { get; private set; }

    public float GuardRange {
        get { return guardRange; }

        set {
            guardRange = value;
            circleCollider.radius = guardRange;
        }
    }

    // 个数/秒
    public float ShotRate { get; private set; }

    public int UseBulletID { get; private set; }

    public void Load(int towerID, Tile tile) {
        this.tile = tile;

        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(towerID);

        ID = towerID;
        MaxLevel = info.maxLevel;
        BasePrice = info.basePrice;
        GuardRange = info.guardRange;
        ShotRate = info.shotRate;
        UseBulletID = info.useBulletID;
        level = 1;

    }

    public abstract void Attack();

    public override void OnSpawn() {
        animator.Play("Idle");
    }

    public override void OnUnspawn() {
        animator.ResetTrigger("Fire");

        target = null;

        ID = 0;
        MaxLevel = 0;
        BasePrice = 0;
        GuardRange = 0;
        ShotRate = 0;
        UseBulletID = 0;
        level = 0;
        lastAttackTime = 0;
    }

    #region Untiy Lifecycle
    protected virtual void Awake() {
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponentInChildren<Animator>();
        battery = transform.GetChild(0).transform;
    }

    protected virtual void Update() {
        LookAt();
        Shoot();
    }

    protected virtual void OnTriggerStay2D(Collider2D collision) {
        if (target == null && collision.tag.Equals("Monster")) {
            target = collision.GetComponent<Monster>();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag.Equals("Monster")) {
            Monster currentMonster = collision.GetComponent<Monster>();
            if (target == currentMonster) {
                target = null;
            }
        }
    }
    #endregion

    private void LookAt() {
        Vector3 eulerAngers = Vector3.zero;
        if (target != null) {
            Vector3 dir = (target.transform.position - battery.position).normalized;
            float dx = dir.x;
            float dy = dir.y;

            // 计算夹角[-180,180]
            float angles = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

            // 得到目标欧拉角
            eulerAngers = battery.eulerAngles;
            eulerAngers.z = angles - 90f;
        }

        battery.eulerAngles = eulerAngers;
    }

    private void Shoot() {
        if (target != null && Time.time >= lastAttackTime + 1 / ShotRate) {
            Attack();
            lastAttackTime = Time.time;
        }
    }
}

