using System.Collections.Generic;
using UnityEngine;

// ****************************************************************
// 功能：子对象池类
// 创建：蔡泽深
// 时间：2017/05/31
// 修改内容：										修改者姓名：
// ****************************************************************

public class SubPool {
    
    private GameObject prefab;                                          // 预设体
    private Transform parent;                                           // 父对象
    private List<GameObject> objects=new List<GameObject>();            // 池对象集合

    /// <summary>
    /// 名字标识
    /// </summary>
    public string Name { get { return prefab.name; } }

    // 构造
    public SubPool(GameObject prefab,Transform parent) {
        this.prefab = prefab;
        this.parent = parent;
    }

    // 生产对象
    public GameObject Spawn() {
        GameObject go=null;
        
        foreach (GameObject item in objects) {
            // 池中有可使用的对象
            if (!item.activeSelf) {
                go = item;
                go.SetActive(true);
                break;
            }
        }

        // 池中没可使用的对象
        if (go==null) {
            go = Object.Instantiate(prefab, parent);
            objects.Add(go);
        }

        ReusableObject reusableO= go.GetComponent<ReusableObject>();
        if (reusableO!=null) {
            reusableO.OnSpawn();
        }

        return go;
    }

    // 回收对象
    public void Unspawn(GameObject go) {
        if (Contains(go)) {

            ReusableObject reusableO = go.GetComponent<ReusableObject>();
            if (reusableO != null) {
                reusableO.OnUnspawn();
            }

            go.SetActive(false);
        }
    }

    // 回收全部
    public void UnspawnAll() {
        foreach (GameObject item in objects) {
            if (item.activeSelf) {
                Unspawn(item);
            }
        }
    }

    // 是否存在指定对象
    public bool Contains(GameObject gameObject) {
        return objects.Contains(gameObject);
    }
}

