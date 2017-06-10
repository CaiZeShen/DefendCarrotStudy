using System;
using UnityEngine;
using UnityEngine.UI;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class UIBoard : View {
    #region 字段
    private Image roundInfoImg;
    private Image pauseInfoImg;
    private Text scoreTxt;
    private Text currentTxt;
    private Text totalTxt;
    private Button speed1Btn;
    private Button speed2Btn;
    private Button resumeBtn;
    private Button pauseBtn;
    private Button menuBtn;
    private bool isPlaying;
    private GameSpeed gameSpeed;
    private int score;
    private RoundModel roundModel;
    private GameModel gameModel;
    #endregion

    #region 属性
    public override string Name {
        get {
            return Consts.V_Board;
        }
    }

    public bool IsPlaying {
        get { return isPlaying; }
        set {
            isPlaying = value;

            pauseBtn.gameObject.SetActive(value);
            resumeBtn.gameObject.SetActive(!value);
            pauseInfoImg.gameObject.SetActive(!value);
            roundInfoImg.gameObject.SetActive(value);
        }
    }

    public GameSpeed GameSpeed {
        get { return gameSpeed; }
        set {
            gameSpeed = value;

            speed1Btn.gameObject.SetActive(gameSpeed == GameSpeed.One);
            speed2Btn.gameObject.SetActive(gameSpeed == GameSpeed.Two);
        }
    }

    public int Score {
        get {
            return score;
        }

        set {
            score = value;

            scoreTxt.text = score.ToString();
        }
    }
    #endregion

    public override void RegisterEvents() {
        attentionEvnets.Add(Consts.E_StartRound);
        attentionEvnets.Add(Consts.E_UpdateGold);
    }

    public override void HandleEvent(string eventName, object arg) {
        switch (eventName) {
            case Consts.E_StartRound:
                UpdateRoundInfo(roundModel.RoundIndex + 1, roundModel.RoundTotal);
                break;
            case Consts.E_UpdateGold:
                Score = gameModel.Gold;
                break;
        }
    }

    #region Unity 生命周期
    private void Awake() {
        roundInfoImg = transform.Find("RoundInfoImg").GetComponent<Image>();
        pauseInfoImg = transform.Find("PauseInfoImg").GetComponent<Image>();
        currentTxt = transform.Find("RoundInfoImg/CurrentTxt").GetComponent<Text>();
        totalTxt = transform.Find("RoundInfoImg/TotalTxt").GetComponent<Text>();
        scoreTxt = transform.Find("ScoreTxt").GetComponent<Text>();
        speed1Btn = transform.Find("Speed1Btn").GetComponent<Button>();
        speed2Btn = transform.Find("Speed2Btn").GetComponent<Button>();
        resumeBtn = transform.Find("ResumeBtn").GetComponent<Button>();
        pauseBtn = transform.Find("PauseBtn").GetComponent<Button>();
        menuBtn = transform.Find("MenuBtn").GetComponent<Button>();

        roundModel = GetModel<RoundModel>();
        gameModel = GetModel<GameModel>();

        IsPlaying = true;
        GameSpeed = GameSpeed.One;
        Score = gameModel.Gold;
    }

    private void OnEnable() {
        speed1Btn.onClick.AddListener(OnSpeed1BtnClick);
        speed2Btn.onClick.AddListener(OnSpeed2BtnClick);
        resumeBtn.onClick.AddListener(OnResumeBtnClick);
        pauseBtn.onClick.AddListener(OnPauseBtnClick);
        menuBtn.onClick.AddListener(OnMenuBtnClick);
    }

    private void OnDisable() {
        speed1Btn.onClick.RemoveListener(OnSpeed1BtnClick);
        speed2Btn.onClick.RemoveListener(OnSpeed2BtnClick);
        resumeBtn.onClick.RemoveListener(OnResumeBtnClick);
        pauseBtn.onClick.RemoveListener(OnPauseBtnClick);
        menuBtn.onClick.RemoveListener(OnMenuBtnClick);
    }
    #endregion

    #region 方法
    /// <summary>
    /// 更新回合信息
    /// </summary>
    /// <param name="current">当前波数</param>
    /// <param name="total">总波数</param>
    private void UpdateRoundInfo(int current, int total) {
        // 转换为两位数,且在两位数中间加入空格
        currentTxt.text = current.ToString("D2").Insert(1, "  ");

        totalTxt.text = total.ToString();
    }

    private void OnSpeed1BtnClick() {
        GameSpeed = GameSpeed.Two;
    }

    private void OnSpeed2BtnClick() {
        GameSpeed = GameSpeed.One;
    }

    private void OnResumeBtnClick() {
        IsPlaying = true;
    }

    private void OnPauseBtnClick() {
        IsPlaying = false;
    }

    private void OnMenuBtnClick() {

    }
    #endregion
}

