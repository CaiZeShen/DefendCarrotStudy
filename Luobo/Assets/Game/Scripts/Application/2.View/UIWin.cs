using System;
using UnityEngine;
using UnityEngine.UI;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/04
// 修改内容：										修改者姓名：
// ****************************************************************

public class UIWin : View {
    private Text totalTxt;
    private Text currentTxt;
    private Text levelTxt;
    private Button resumeBtn;
    private Button restartBtn;
    private RoundModel rm;
    private GameModel gm;

    public override string Name {
        get {
            return Consts.V_Win;
        }
    }

    public void Show() {
        gameObject.SetActive(true);

        UpdateInfo(rm.RoundIndex+1,rm.RoundTotal, gm.CurrentLevelIndex+1);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    private void UpdateInfo(int currentRound, int totalRound, int level) {
        currentTxt.text = currentRound.ToString("D2").Insert(1, "  ");
        totalTxt.text = totalRound.ToString();
        levelTxt.text = level.ToString();
    }

    public override void HandleEvent(string eventName, object arg) {

    }

    #region Unity lifecycle
    private void Awake() {
        Transform infoT = transform.Find("Info");
        totalTxt = infoT.Find("TotalTxt").GetComponent<Text>();
        currentTxt = infoT.Find("CurrentTxt").GetComponent<Text>();
        levelTxt = infoT.Find("LevelTxt").GetComponent<Text>();
        resumeBtn = transform.Find("ResumeBtn").GetComponent<Button>();
        restartBtn = transform.Find("RestartBtn").GetComponent<Button>();

        rm = GetModel<RoundModel>();
        gm = GetModel<GameModel>();
    }

    private void OnEnable() {
        resumeBtn.onClick.AddListener(OnResumeBtnClick);
        restartBtn.onClick.AddListener(OnRestartBtnClick);
    }

    private void OnDisable() {
        resumeBtn.onClick.RemoveListener(OnResumeBtnClick);
        restartBtn.onClick.RemoveListener(OnRestartBtnClick);
    }
    #endregion

    private void OnResumeBtnClick() {
        
        if (gm.GameProgress >= gm.LevelCount-1) {
            // 游戏通关
            SendEvent(Consts.E_LoadScene,new SceneArgs(Consts.Complete));
        } else {
            // 开始下一关
            SendEvent(Consts.E_StartLevel, new StartLevelArgs { LevelIndex = gm.CurrentLevelIndex + 1 });
        }
        
    }

    private void OnRestartBtnClick() {
        SendEvent(Consts.E_StartLevel, new StartLevelArgs { LevelIndex = gm.CurrentLevelIndex });
    }
}

