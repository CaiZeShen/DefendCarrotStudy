using UnityEngine;

// ****************************************************************
// 功能：云的移动效果脚本
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class Cloud : MonoBehaviour {
    public float offsetX = 1000;    // X方向偏移量
    public float duration = 1f;     // 周期时间

    private void Start() {
        iTween.MoveBy(gameObject, iTween.Hash(
            "x", offsetX,
            "easeType", iTween.EaseType.linear,
            "loopType", iTween.LoopType.loop,
            "time", duration
            ));
    }
}

