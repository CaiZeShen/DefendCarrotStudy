using UnityEngine;

// ****************************************************************
// 功能：预制体加载实例化管理类
// 创建：CaiZeShen
// 时间：2017/05/10
// 修改内容：										修改者姓名：
// ****************************************************************

public class PrefabMgr:Singleton<PrefabMgr>{

    /// <summary>
    /// 实例化预制体
    /// </summary>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    public GameObject Spawn(string prefabName,Vector3 pos,Quaternion rotation,Transform parent) {
        GameObject prefab= ResourcesMgr.Instance.Load<GameObject>(Consts.PrefabResDir);
        if (prefab != null) {
            prefab = Object.Instantiate(prefab, parent);
            prefab.transform.localPosition = pos;
            prefab.transform.localRotation = rotation;
        }
        else
            Debug.LogError("Prefab:" + prefabName + " not founded!");

        return prefab;
    }

    public GameObject Spawn(string effectName) {
        return Spawn(effectName, Vector3.zero, Quaternion.identity,null);
    }

    public GameObject Spawn(string effectName,Transform parent) {
        return Spawn(effectName, Vector3.zero, Quaternion.identity, parent);
    }

    public GameObject Spawn(string effectName, Vector3 pos, Quaternion rotation) {
        return Spawn(effectName, pos, rotation, null);
    }
}
