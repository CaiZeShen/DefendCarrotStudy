using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/05
// 修改内容：										修改者姓名：
// ****************************************************************

public class GameModel : Model {
    // 所有的关卡
    private List<Level> levels = new List<Level>();
    // 当前游戏关卡索引
    private int currentLevelIndex = -1;
    // 最大通关关卡索引
    private int gameProgress = -1;
    // 游戏当前分数
    private int gold = 0;
    // 是否游戏中
    private bool isPlaying = false;

    public override string Name {
        get {return Consts.M_Game;}
    }

    public List<Level> AllLevel {
        get { return levels; }
    }

    public int LevelCount {
        get { return levels.Count; }
    }

    public bool IsGamePassed {
        get { return GameProgress >= LevelCount - 1; }
    }

    public Level PlayLevel {
        get {
            
            if (CurrentLevelIndex<0 || CurrentLevelIndex>=levels.Count) {
                throw new IndexOutOfRangeException("没有指定id的关卡: " + CurrentLevelIndex);
            }

            return levels[CurrentLevelIndex];
        }
    }

    public int GameProgress {
        get {return gameProgress;}
    }

    public int Gold {
        get {return gold;}
        set { gold = value;}
    }

    public bool IsPlaying {
        get {return isPlaying;}
        set { isPlaying = value;}
    }

    public int CurrentLevelIndex {
        get {return currentLevelIndex;}
    }

    // 初始化
    public void Initialize() {
        // 构建Level集合
        List<FileInfo> fileInfos = Tools.GetLevelFiles();
        levels.Clear();
        foreach (FileInfo f in fileInfos) {
            Level level = new Level();
            Tools.FillLevel(f.FullName, ref level);
            levels.Add(level);
        }

        // 读取进度
        gameProgress = Saver.GetProgress();
    }

    // 游戏开始
    public void StartLevel(int levelIndex) {
        currentLevelIndex = levelIndex;
        gold = PlayLevel.initSocre;
        isPlaying = true;
    }

    // 游戏结束
    public void StopLevel(bool isWin) {
        if (isWin && CurrentLevelIndex > gameProgress) {
            // 保存进度
            gameProgress = currentLevelIndex;
            Saver.SetProgress(gameProgress);
        }
        IsPlaying = false;
    }

    // 清档
    public void ClearProgess() {
        currentLevelIndex = -1;
        isPlaying = false;
        gameProgress = -1;

        Saver.SetProgress(gameProgress);
    }
}

