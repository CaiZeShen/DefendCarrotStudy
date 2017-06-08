using UnityEngine;

// ****************************************************************
// 功能：叶子的抖动效果
// 创建：蔡泽深
// 时间：2017/06/03
// 修改内容：										修改者姓名：
// ****************************************************************

public class Leaf : MonoBehaviour {
    private void Start() {
        iTween.PunchRotation(gameObject, iTween.Hash(
            "z", 20,
            "easeType", iTween.EaseType.linear,
            "loopType", iTween.LoopType.loop,
            "delay", 4f,
            "time", 1f
            ));
    }
}

