using UnityEngine;

// ****************************************************************
// 功能：萝卜脚本
// 创建：蔡泽深
// 时间：2017/06/06
// 修改内容：										修改者姓名：
// ****************************************************************

public class Luobo : Role {
    private Animator animator;

    public override void Damage(int hit) {
        if (IsDead)
            return;

        base.Damage(hit);

        animator.SetTrigger("IsDamaged");
    }

    protected override void OnDied(Role role) {
        animator.SetBool("IsDead",true);
    }

    public override void OnSpawn() {
        base.OnSpawn();

        animator.Play("Luobo_Idle");

        LuoboInfo info = Game.Instance.StaticData.GetLuoboInfo();
        MaxHP = info.hp;
        HP = info.hp;
    }

    public override void OnUnspawn() {
        // 还原
        base.OnUnspawn();
        animator.SetBool("IsDead", false);
        animator.ResetTrigger("IsDamaged");
    }

    #region Unity 生命周期
    private void Awake() {
        animator = transform.Find("Luobo").GetComponent<Animator>();
    } 
    #endregion
}

