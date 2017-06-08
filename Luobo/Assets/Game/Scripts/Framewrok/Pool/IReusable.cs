using UnityEngine;

// ****************************************************************
// 功能：可重用对象接口，用于对象池
// 创建：蔡泽深
// 时间：2017/05/31
// 修改内容：										修改者姓名：
// ****************************************************************

public interface IReusable {
    // 生产对象时被调用
    void OnSpawn();

    // 回收对象时被调用
    void OnUnspawn();
}

