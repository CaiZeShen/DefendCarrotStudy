using UnityEngine;
using System.Collections.Generic;

// ****************************************************************
// 功能：对象池类
// 创建：蔡泽深
// 时间：2017/05/31
// 修改内容：										修改者姓名：
// ****************************************************************

public class ObjectPool:SingletonMono<ObjectPool> {
    // 子对象池集合
    private Dictionary<string, SubPool> pools = new Dictionary<string, SubPool>();

    // 生产指定对象
    public GameObject Spawn(string prefabName) {
        if (!pools.ContainsKey(prefabName))
            RegisterNewSubPool(prefabName);

        SubPool pool = pools[prefabName];
        return pool.Spawn();
    }

    // 回收对象
    public void Unspawn(GameObject gameObject) {
        SubPool pool = null;

        foreach (SubPool p in pools.Values) {
            if (p.Contains(gameObject)) {
                pool = p;
                break;
            }
        }

        pool.Unspawn(gameObject);
    }

    // 回收全部对象
    public void UnspawnAll() {
        foreach (SubPool p in pools.Values) {
            p.UnspawnAll();
        }
    }

    // 创建新子池子
    private void RegisterNewSubPool(string prefabName) {
        // 获取预设体
        GameObject prefab = ResourcesMgr.Instance.Load<GameObject>(Consts.PrefabResDir+prefabName);

        // 创建新子池子
        SubPool pool = new SubPool(prefab,transform);
        pools.Add(pool.Name, pool);
    }
}

