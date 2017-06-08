using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ****************************************************************
// 功能：选择场景关卡图片UI
// 创建：蔡泽深
// 时间：2017/06/04
// 修改内容：										修改者姓名：
// ****************************************************************

public class CardImg : MonoBehaviour,IPointerDownHandler {
    public event Action<Card> clickCard;
    private Card card=null;                  // 卡牌信息
    private Image cardImg;
    private Image lockImg;
    private bool isTranslucent=false;         // 半透明
    
    public bool IsTranslucent {
        get {return isTranslucent;}

        set {
            isTranslucent = value;

            Image[] images = new Image[] { cardImg, lockImg };
            for (int i = 0; i < images.Length; i++) {
                Color c = images[i].color;
                c.a = value ? 0.5f : 1f;
                images[i].color = c;
            }
        }
    }

    public void BindData (Card card) {
        this.card = card;

        // 加载图片
        cardImg.sprite = ResourcesMgr.Instance.Load<Sprite>(Consts.CardResDir + card.image);

        // 设置锁定
        lockImg.gameObject.SetActive(card.isLocked);
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (clickCard!=null) {
            clickCard(card);
        }
    }

    #region Unity Lifecycle
    private void Awake() {
        cardImg = GetComponent<Image>();
        lockImg = transform.Find("LockImg").GetComponent<Image>();
    }

    private void OnDestroy() {
        // 为什么不直接clickCard=null 而用循环.  因为方法注册其实在内部有个数据结构对很多方法的地址进行维护的,你直接null没有把方法的地址清掉

        while (clickCard != null) {
            clickCard -= clickCard;
        }
    }
    #endregion


}

