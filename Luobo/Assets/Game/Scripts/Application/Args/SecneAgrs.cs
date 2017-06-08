using UnityEngine;

// ****************************************************************
// 功能：场景切换参数
// 创建：蔡泽深
// 时间：2017/06/01
// 修改内容：										修改者姓名：
// ****************************************************************

public class SceneArgs {
    public string Name { get; private set; }
    public bool IsAsync { get; private set; }

    public SceneArgs(string name,bool isAsync) {
        Name = name;
        IsAsync = isAsync;
    }

    public SceneArgs(string name) {
        Name = name;
        IsAsync = false;
    }
}

