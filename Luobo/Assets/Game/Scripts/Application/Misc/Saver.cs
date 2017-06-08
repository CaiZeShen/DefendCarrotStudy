using UnityEngine;

// ****************************************************************
// 功能：存档相关
// 创建：蔡泽深
// 时间：2017/06/05
// 修改内容：										修改者姓名：
// ****************************************************************

public class Saver {
    public static int GetProgress() {
        return PlayerPrefs.GetInt(Consts.GameProgress, -1);
    }

    public static void SetProgress(int levelID) {
        PlayerPrefs.SetInt(Consts.GameProgress, levelID);
    }
}

