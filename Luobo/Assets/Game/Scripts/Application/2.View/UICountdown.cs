using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class UICountdown : View {
    private Transform fire;
    private GameObject[] countImgs = new GameObject[3];

    public override string Name {
        get {
            return Consts.V_Countdown;
        }
    }

    public override void RegisterEvents() {
        attentionEvnets.Add(Consts.E_EnterScene);
    }

    public override void HandleEvent(string eventName, object args) {
        switch (eventName) {
            // 开始关卡就倒计时
            case Consts.E_EnterScene:
                SceneArgs e = args as SceneArgs;
                if (e.Name==Consts.Level) {
                    StartCountdown();
                }
                break;
        }
    }

    #region Unity Lifecycle
    private void Awake() {
        fire = transform.Find("Fire");
        countImgs[0] = transform.Find("Count3Img").gameObject;
        countImgs[1] = transform.Find("Count2Img").gameObject;
        countImgs[2] = transform.Find("Count1Img").gameObject;
    }
    #endregion

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void StartCountdown() {
        Show();
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown() {
        for (int i = 0; i < countImgs.Length; i++) {
            countImgs[i].SetActive(true);
            yield return new WaitForSeconds(1f);
            countImgs[i].SetActive(false);
        }

        Hide();
        // 倒计时完成
        SendEvent(Consts.E_CountdownComplete);
    }
}

