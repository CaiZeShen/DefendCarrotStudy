using UnityEngine;

// ****************************************************************
// 功能：游戏入口
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

[RequireComponent(typeof(SoundMgr))]
[RequireComponent(typeof(SceneMgr))]
[RequireComponent(typeof(ObjectPool))]
public class Game : ApplicationBase<Game> {
    // 全局访问功能
    public ObjectPool ObjectPool { get; private set; }
    public SoundMgr SoundMgr { get; private set; }
    public StaticData StaticData { get; private set; }
    public SceneMgr SceneMgr { get; private set; }

    // 全局方法


    // 游戏入口
    private void Start() {
        DontDestroyOnLoad(gameObject);

        // 全局单例赋值
        ObjectPool = ObjectPool.Instance;
        SoundMgr = SoundMgr.Instance;
        StaticData = StaticData.Instance;
        SceneMgr = SceneMgr.Instance;

        // 注册启动命令
        RegisterController(Consts.E_StartUp, typeof(StartUpCommand));
        // 启动游戏
        SendEvent(Consts.E_StartUp);
    }
}

