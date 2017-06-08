using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/04
// 修改内容：										修改者姓名：
// ****************************************************************

public class UIComplete : View {
    private Button restartBtn;
    private Button clearBtn;
    private GameModel gm;

    public override string Name {
        get {
            return Consts.V_Complete;
        }
    }

    public override void HandleEvent(string eventName, object arg) {
        
    }

    #region Unity Lifecycle
    private void Awake() {
        restartBtn = transform.Find("RestartBtn").GetComponent<Button>();
        clearBtn = transform.Find("ClearBtn").GetComponent<Button>();

        gm = GetModel<GameModel>();
    }

    private void OnEnable() {
        restartBtn.onClick.AddListener(OnRestartBtnClick);
        clearBtn.onClick.AddListener(OnClearBtnClick);
    }

    private void OnDisable() {
        restartBtn.onClick.RemoveListener(OnRestartBtnClick);
        clearBtn.onClick.RemoveListener(OnClearBtnClick);
    }
    #endregion

    private void OnRestartBtnClick() {
        SendEvent(Consts.E_LoadScene, new SceneArgs(Consts.Start));
    }

    private void OnClearBtnClick() {
        gm.ClearProgess();
        SendEvent(Consts.E_LoadScene, new SceneArgs(Consts.Start));
    }
}

