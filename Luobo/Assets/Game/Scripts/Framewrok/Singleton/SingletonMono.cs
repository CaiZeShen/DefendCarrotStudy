using UnityEngine;

// ****************************************************************
// 功能：组件单例模板类
// 创建：蔡泽深
// 时间：17-5-4
// 修改内容：										修改者姓名：
// ****************************************************************

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour {
    private static T instance;

    public static T Instance {
        get {
            if (instance == null) {
                // 创建空物体，物体名字跟组件名字一致
                GameObject empty = new GameObject(typeof(T).Name);
                instance= empty.AddComponent<T>();
            }

            return instance;
        }
    }

    protected virtual void Awake() {
        instance = this as T;
    }

}
