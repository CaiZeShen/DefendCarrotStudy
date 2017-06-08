using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
// ****************************************************************
// 功能：场景加载管理器
// 创建：蔡泽深
// 时间：17-05-01
// 修改内容：										修改者姓名：
// ****************************************************************

public class SceneMgr : SingletonMono<SceneMgr> {
    private string sceneName;
    private float progress;

    public float Progress { get { return progress; } }

    /// <summary>
    /// 场景切换
    /// </summary>
    /// <param name="args"></param>
    public void LoadScene(SceneArgs args) {
        // 发布退出场景事件
        SceneArgs e = new SceneArgs (SceneManager.GetActiveScene().name, args.IsAsync);
        MVC.SendEvent(Consts.E_ExitScene, e);

        // 保存目标场景
        sceneName = args.Name;

        // 加载过渡场景或者目标场景
        string tempSceneName = args.IsAsync ? Consts.SceneLoading : args.Name;
        SceneManager.LoadScene(tempSceneName);
    }

    public IEnumerator LoadSceneAsync() {
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneName);
        // 阻止场景自动跳转
        asyncOper.allowSceneActivation = false;
        do {
            progress = asyncOper.progress;
            yield return null;
        } while (progress < 0.9f);

        while (progress < 1f) {
            yield return null;
            progress += 0.001f;
        }
        asyncOper.allowSceneActivation = true;
    }

    private void OnLevelWasLoaded(int level) {
        // 事件参数
        SceneArgs e = new SceneArgs(SceneManager.GetSceneByBuildIndex(level).name);

        // 发布事件
        MVC.SendEvent(Consts.E_EnterScene, e);
    }
}
