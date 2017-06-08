using UnityEngine;
// ****************************************************************
// 功能：单例模板类
// 创建：蔡泽深
// 时间：17-05-02
// 修改内容：										修改者姓名：
// ****************************************************************

public class Singleton<T> where T:new() {
    private static readonly T instance = new T();

    public static T Instance {
        get { return instance; }
    }
}
