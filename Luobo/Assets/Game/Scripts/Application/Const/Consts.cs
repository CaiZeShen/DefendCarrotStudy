using UnityEngine;

// ****************************************************************
// 功能：
// 创建：蔡泽深
// 时间：2017/06/01
// 修改内容：										修改者姓名：
// ****************************************************************

public static class Consts {
    // 目录
    public static readonly string LevelDir =Application.dataPath + @"\Game\Res\Levels\";      
    public static readonly string MapDir = Application.dataPath + @"\Game\Res\Maps\";
    public static readonly string CardDir = Application.dataPath + @"\Game\Res\Scene\stages_theme1\";
    // Resources中的目录
    public const string SoundResDir = "Sounds/";
    public const string PrefabResDir = "Prefabs/";
    public const string MapResDir = "Textures/Maps/";
    public const string CardResDir = "Textures/Cards/";
    public const string TowerIconResDir = "Textures/TowerIcons/";

    // 存档
    public const string GameProgress = "GameProgress";

    // Model标识
    public const string M_Game = "M_Game";
    public const string M_Round = "M_Round";

    // View标识
    public const string V_Start = "V_Start";
    public const string V_Select = "V_Select";
    public const string V_Board = "V_Board";
    public const string V_Countdown = "V_Countdown";
    public const string V_Lose = "V_Lose";
    public const string V_Win = "V_Win";
    public const string V_System = "V_System";
    public const string V_Complete = "V_Complete";
    public const string V_Spawner = "V_Spawner";
    public const string V_TowerPopup = "V_TowerPopup";

    // 事件
    public const string E_StartUp           = "E_StartUp";
    public const string E_ExitScene         = "E_ExitScene";            // 参数: SceneArgs
    public const string E_EnterScene        = "E_EnterScene";           // 参数: SceneArgs
    public const string E_LoadScene         = "E_LoadScene";            // 参数: SceneArgs
    public const string E_StartLevel        = "E_StartLevel";           // 参数: StartLevelArgs
    public const string E_EndLevel          = "E_EndLevel";             // 参数: EndLevelArgs
    public const string E_CountdownComplete = "E_CountdownComplete";
    public const string E_StartRound = "E_StartRound";                  // 参数: StartRoundArgs
    public const string E_SpawnMonster = "E_SpawnMonster";              // 参数: SpawnMonsterArgs

    public const string E_ShowSpawnPanel = "E_ShowSpawnPanel";          // 参数: ShowSpawnPanelArgs
    public const string E_ShowUpgradePanel = "E_ShowUpgradePanel";      // 参数: ShowUpgradePanelArgs
    public const string E_HidePopups = "E_HidePopups";
    public const string E_SpawnTower = "E_SpawnTower";                  // 参数: SpawnTowerArgs
    public const string E_UpgradeTower = "E_UpgradeTower";              // 参数: UpgradeTowerArgs
    public const string E_SellTower = "E_SellTower";                    // 参数: SellTowerArgs

    // 场景名
    public const string Init = "0.Init";
    public const string Start = "1.Start";
    public const string Select = "2.Select";
    public const string Level = "3.Level";
    public const string Complete = "4.Complete";
    public const string SceneLoading = "SceneLoading";
    public const string LevelBuilder = "LevelBuilder";
}

