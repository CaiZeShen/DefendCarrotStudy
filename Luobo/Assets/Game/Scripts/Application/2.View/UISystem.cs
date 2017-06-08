using System;
using UnityEngine;
using UnityEngine.UI;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/04
// 修改内容：										修改者姓名：
// ****************************************************************

public class UISystem : View {
    private Button resumeBtn;
    private Button restartBtn;
    private Button selectBtn;

    public override string Name {
        get {
            return Consts.V_System;
        }
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public override void HandleEvent(string eventName, object arg) {

    }

    #region Unity Lifecycle
    private void Awake() {
        resumeBtn = transform.Find("ResumeBtn").GetComponent<Button>();
        restartBtn = transform.Find("RestartBtn").GetComponent<Button>();
        selectBtn = transform.Find("SelectBtn").GetComponent<Button>();
    }

    private void OnEnable() {
        resumeBtn.onClick.AddListener(OnResumeBtnClick);
        restartBtn.onClick.AddListener(OnRestartBtnClick);
        selectBtn.onClick.AddListener(OnSelectBtnClick);
    }

    private void OnDisable() {
        resumeBtn.onClick.RemoveListener(OnResumeBtnClick);
        restartBtn.onClick.RemoveListener(OnRestartBtnClick);
        selectBtn.onClick.RemoveListener(OnSelectBtnClick);
    }
    #endregion

    private void OnSelectBtnClick() {
        throw new NotImplementedException();
    }

    private void OnRestartBtnClick() {
        throw new NotImplementedException();
    }

    private void OnResumeBtnClick() {
        throw new NotImplementedException();
    }
}

