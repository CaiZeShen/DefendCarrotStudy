using UnityEngine;

// ****************************************************************
// 功能：资源管理
// 创建：蔡泽深
// 时间：17-5-4
// 修改内容：										修改者姓名：
// ****************************************************************

public class ResourcesMgr:Singleton<ResourcesMgr> {

    public T Load<T>(string path) where T : Object{
        return Resources.Load<T>(path);
    }
}
