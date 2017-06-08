using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/07
// 修改内容：										修改者姓名：
// ****************************************************************

public class TowerIcon : MonoBehaviour {
    private SpriteRenderer render;
    private TowerInfo info;
    private Vector3 position;
    private bool isEnough = false;

    public void Load(GameModel gm,TowerInfo info,Vector3 pos,bool upSide) {
        // 保存必要的信息
        this.info = info;
        position = pos;

        // 金币是否足够
        isEnough = gm.Gold >= info.basePrice;

        // 加载图片
        string targetIcon = isEnough ? info.normalIcon : info.disableIcon;
        render.sprite= ResourcesMgr.Instance.Load<Sprite>(Consts.TowerIconResDir + targetIcon);

        // 摆放位置
        Vector3 locPos = transform.localPosition;
        locPos.y = upSide ? Mathf.Abs(locPos.y) : -Mathf.Abs(locPos.y);
        transform.localPosition = locPos;
    }

    #region Unity Lifecycle
    private void Awake() {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown() {
        // 金币是否足够
        if (!isEnough) {
            return;
        }

        // 参数
        object[] args = { info.id, position };

        // 消息冒泡
        SendMessageUpwards("OnSpawnTower", args, SendMessageOptions.RequireReceiver);
    }
    #endregion
}

