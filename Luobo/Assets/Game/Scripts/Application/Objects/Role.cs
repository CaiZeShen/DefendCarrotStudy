using System;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/06
// 修改内容：										修改者姓名：
// ****************************************************************

public class Role : ReusableObject {
    public event Action<int, int> hpChanged;
    public event Action<Role> died;

    private int hp;
    private int maxHp;

    public int HP {
        get { return hp; }
        set {
            // 范围约束
            value = Mathf.Clamp(value, 0, MaxHP);

            // 没变
            if (value == hp) {
                return;
            }

            // 赋值
            hp = value;

            // 血量变化
            if (hpChanged != null) {
                hpChanged(hp, MaxHP);
            }

            if (died != null && IsDead) {
                died(this);
            }
        }
    }

    public int MaxHP {
        get {return maxHp;}
        set {
            maxHp = value<0?0:value;
        }
    }

    public bool IsDead {
        get { return hp == 0; }
    }

    public virtual void Damage(int hit) {
        if (IsDead) {
            return;
        }

        HP -= hit;
    }

    protected virtual void OnDied(Role role) { }

    public override void OnSpawn() {
        died += OnDied;
    }

    public override void OnUnspawn() {
        while (hpChanged != null) {
            hpChanged -= hpChanged;
        }

        while (died != null) {
            died -= died;
        }

        HP = 0;
        maxHp = 0;
    } 
}

