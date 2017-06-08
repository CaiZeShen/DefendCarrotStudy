using UnityEngine;

// ****************************************************************
// 功能：倒计时火焰的旋转效果
// 创建：蔡泽深
// 时间：2017/06/04
// 修改内容：										修改者姓名：
// ****************************************************************

public class FireRotate : MonoBehaviour {
    private float speed=360;    // 度/秒

    private void Update() {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime,Space.Self);
    }
}

