using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class UISelect : View {
    private Button backBtn;
    private Button helpBtn;
    private Button playBtn;
    private List<Card> cards;
    private CardImg leftCardImg;
    private CardImg midCardImg;
    private CardImg rightCardImg;
    private int selectedCardIndex = -1;
    private GameModel gameModel;

    public override string Name {
        get {
            return Consts.V_Select;
        }
    }

    public override void HandleEvent(string name, object arg) {
        throw new NotImplementedException();
    }

    #region Unity Lifecycle
    private void Awake() {
        backBtn = transform.Find("BackBtn").GetComponent<Button>();
        helpBtn = transform.Find("HelpBtn").GetComponent<Button>();
        playBtn = transform.Find("PlayBtn").GetComponent<Button>();
        leftCardImg = transform.Find("Cards/LeftCardImg").GetComponent<CardImg>();
        midCardImg = transform.Find("Cards/MidCardImg").GetComponent<CardImg>();
        rightCardImg = transform.Find("Cards/RightCardImg").GetComponent<CardImg>();

        gameModel = GetModel<GameModel>();
    }

    private void Start() {
        LoadCards();
        leftCardImg.IsTranslucent = true;
        rightCardImg.IsTranslucent = true;
    }

    private void OnEnable() {
        backBtn.onClick.AddListener(OnBackBtnClick);
        playBtn.onClick.AddListener(OnPlayBtnClick);
    }

    private void OnDisable() {
        backBtn.onClick.RemoveListener(OnBackBtnClick);
        playBtn.onClick.RemoveListener(OnPlayBtnClick);
    }
    #endregion

    private void OnPlayBtnClick() {
        StartLevelArgs args = new StartLevelArgs() { LevelIndex = selectedCardIndex };
        SendEvent(Consts.E_StartLevel, args);
    }

    private void OnBackBtnClick() {
        SceneArgs args = new SceneArgs(Consts.Start);
        SendEvent(Consts.E_LoadScene, args);
    }

    private void LoadCards() {
        List<Level> levels= gameModel.AllLevel;

        // 构建Card集合
        List<Card> cardTemps = new List<Card>();
        for (int i = 0; i < levels.Count; i++) {
            Card card = new Card {
                levelIndex = i,
                image = levels[i].cardImage,
                isLocked = i > gameModel.GameProgress+1,
            };

            cardTemps.Add(card);
        }

        // 保存
        cards = cardTemps;

        // 监听卡片点击事件
        CardImg[] cardImgs = new CardImg[] { leftCardImg, midCardImg, rightCardImg };
        foreach (CardImg c in cardImgs) {
            c.clickCard += (card) => SelectCard(card.levelIndex);
        }

        // 默认选择第一个卡片
        SelectCard(0);
    }

    private void SelectCard(int cardIndex) {
        if (cardIndex == selectedCardIndex) {
            return;
        }

        selectedCardIndex = cardIndex;

        int leftCardIndex = cardIndex - 1;
        bool isShow = leftCardIndex >= 0;
        leftCardImg.gameObject.SetActive(isShow);
        if (isShow) {
            leftCardImg.BindData(cards[leftCardIndex]);
        }
        
        midCardImg.BindData(cards[cardIndex]);

        int rightCardIndex= cardIndex + 1;
        isShow = rightCardIndex < cards.Count;
        rightCardImg.gameObject.SetActive(isShow);
        if (isShow) {
            rightCardImg.BindData(cards[rightCardIndex]);
        }

        // 控制游戏开始按钮状态
        playBtn.interactable = !cards[cardIndex].isLocked;
    }
}

