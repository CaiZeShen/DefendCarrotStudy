using UnityEngine;

// ****************************************************************
// 功能：开始界面中鸟的飞行效果的脚本
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class Bird : MonoBehaviour {
    public float time = 1;          // 一次循环所需时间
    public float offsetY = 2;       // Y方向浮动偏移

    private void Start() {
        iTween.MoveBy(gameObject, iTween.Hash(
            "y",offsetY,
            "easeType",iTween.EaseType.easeInOutSine,
            "loopType",iTween.LoopType.pingPong,
            "time",time
            ));
    }
}

