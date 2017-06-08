using System;
using UnityEngine;

// ****************************************************************
// 功能：可回收的对象
// 创建：蔡泽深
// 时间：2017/05/31
// 修改内容：										修改者姓名：
// ****************************************************************

public abstract class ReusableObject : MonoBehaviour{
    public abstract void OnSpawn();

    public abstract void OnUnspawn();
}

