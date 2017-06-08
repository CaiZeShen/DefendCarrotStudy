using UnityEngine;
using UnityEngine.UI;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/04
// 修改内容：										修改者姓名：
// ****************************************************************

public class UILose : View {
    private Text totalTxt;
    private Text currentTxt;
    private Text levelTxt;
    private Button restartBtn;
    private RoundModel rm;
    private GameModel gm;

    public override string Name {
        get {
            return Consts.V_Lose;
        }
    }

    public void Show() {
        gameObject.SetActive(true);

        UpdateInfo(rm.RoundIndex, rm.RoundTotal, gm.CurrentLevelIndex + 1);
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
        restartBtn = transform.Find("RestartBtn").GetComponent<Button>();

        rm = GetModel<RoundModel>();
        gm = GetModel<GameModel>();
    }

    private void OnEnable() {
        restartBtn.onClick.AddListener(OnRestartBtnClick);
    }

    private void OnDisable() {
        restartBtn.onClick.RemoveListener(OnRestartBtnClick);
    }
    #endregion

    private void OnRestartBtnClick() {
        SendEvent(Consts.E_StartLevel, new StartLevelArgs { LevelIndex = gm.CurrentLevelIndex });
    }
}

