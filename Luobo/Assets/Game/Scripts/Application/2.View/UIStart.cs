using System;
using UnityEngine;
using UnityEngine.UI;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class UIStart : View {
    private Button adventureBtn;
    private Button bossBtn;
    private Button nestBtn;

    public override string Name {
        get {
            return Consts.V_Start;
        }
    }

    public override void HandleEvent(string name, object arg) {
    }

    #region Unity Lifecycle
    private void Awake() {
        adventureBtn = transform.Find("AdventureBtn").GetComponent<Button>();
        bossBtn = transform.Find("BossBtn").GetComponent<Button>();
        nestBtn = transform.Find("NestBtn").GetComponent<Button>();
    }

    private void OnEnable() {
        adventureBtn.onClick.AddListener(OnAdventureBtnClick);
    }

    private void OnDisable() {
        adventureBtn.onClick.RemoveListener(OnAdventureBtnClick);
    }
    #endregion

    private void OnAdventureBtnClick() {
        SendEvent(Consts.E_LoadScene, new SceneArgs(Consts.Select));
    }
}

