using System;
using UnityEngine;

// ****************************************************************
// 功能：Map的入口类
// 创建：蔡泽深
// 时间：2017/06/05
// 修改内容：										修改者姓名：
// ****************************************************************

public class Spawner : View {
    private Map map = null;
    private Luobo luobo;
    private GameModel gameModel;
    private RoundModel roundModel;

    public override string Name {
        get {
            return Consts.V_Spawner;
        }
    }

    public override void RegisterEvents() {
        attentionEvnets.Add(Consts.E_SpawnMonster);
        attentionEvnets.Add(Consts.E_EnterScene);
        attentionEvnets.Add(Consts.E_SpawnTower);
    }

    public override void HandleEvent(string eventName, object args) {
        switch (eventName) {
            case Consts.E_EnterScene:
                SceneArgs sceneArgs = args as SceneArgs;
                if (sceneArgs.Name == Consts.Level) {
                    // 获取数据
                    gameModel = GetModel<GameModel>();
                    roundModel = GetModel<RoundModel>();

                    // 加载地图
                    map = GetComponent<Map>();
                    map.tileClickEvent += OnMapTileClick;
                    map.LoadLevel(gameModel.PlayLevel);

                    // 加载萝卜
                    Vector3[] path = map.Path;
                    SpawnLuobo(path[path.Length - 1]);
                }
                break;
            case Consts.E_SpawnMonster:
                SpawnMonsterArgs smArgs = args as SpawnMonsterArgs;
                SpawnMonster(smArgs.monsterType);
                break;
            case Consts.E_SpawnTower:
                SpawnTowerArgs stArgs = args as SpawnTowerArgs;
                SpawnTower(stArgs.towerID, stArgs.pos);
                break;
        }
    }

    // 地图格子被点击
    private void OnMapTileClick(object sender, TileClickEvnetArgs e) {
        if (!gameModel.IsPlaying) {
            return;
        }

        if (!e.tile.canHold) {
            SendEvent(Consts.E_HidePopups);
            return;
        }

        Tile tile = e.tile;

        if (tile.tower == null) {
            // 发送显示建塔面板事件
            ShowSpawnPanelArgs e1 = new ShowSpawnPanelArgs {
                position = map.GetPosition(tile),
                upSide = tile.y < (Map.RowCount / 2),
            };
            SendEvent(Consts.E_ShowSpawnPanel, e1);
        } else {
            // 发送显示升级塔面板事件
            ShowUpgradePanelArgs e2 = new ShowUpgradePanelArgs {
                tower = tile.tower,
            };
            SendEvent(Consts.E_ShowUpgradePanel, e2);
        }
    }


    // 怪物到达终点
    private void OnMonsterReached(Monster obj) {
        // 萝卜掉血
        luobo.Damage(1);

        // 回收怪物
        Game.Instance.ObjectPool.Unspawn(obj.gameObject);

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        if (!luobo.IsDead && roundModel.AllRoundsComplete && monsters.Length <= 0) {
            SendEvent(Consts.E_EndLevel, new EndLevelArgs { LevelID = gameModel.CurrentLevelIndex, IsWin = true });
        }
    }

    // 怪物血量改变
    private void OnMonsterHPChanged(int hp, int maxHP) {

    }

    // 怪物死亡
    private void OnMonsterDied(Role obj) {

    }

    // 萝卜死亡
    private void OnLuoboDied(Role obj) {
        SendEvent(Consts.E_EndLevel, new EndLevelArgs { LevelID = gameModel.CurrentLevelIndex, IsWin = false });
    }

    // 生产怪物
    private void SpawnMonster(int monsterType) {
        // 构建预设体名
        string prefabName = "Monster" + monsterType.ToString("D3");

        // 从对象池取出怪物
        GameObject go = Game.Instance.ObjectPool.Spawn(prefabName);

        Monster monster = go.GetComponent<Monster>();
        monster.readched += OnMonsterReached;
        monster.hpChanged += OnMonsterHPChanged;
        monster.died += OnMonsterDied;
        monster.LoadRoadPath(map.Path);
    }

    // 生产塔
    private void SpawnTower(int towerID, Vector3 pos) {
        // 生成塔
        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(towerID);
        GameObject go = Game.Instance.ObjectPool.Spawn(info.prefabName);

        // 设置塔信息
        Tower tower = go.GetComponent<Tower>();
        tower.transform.position = pos;
        Tile tile = map.GetTile(pos);
        tower.Load(towerID, tile);

        // 设置塔所在格子信息
        tile.tower = tower;
        

        // 消耗金币(写这里感觉不对,应该在控制器写吧)
        gameModel.Gold -= info.basePrice;
    }

    // 生产萝卜
    private void SpawnLuobo(Vector3 pos) {
        GameObject go = Game.Instance.ObjectPool.Spawn("Luobo");
        luobo = go.GetComponent<Luobo>();
        luobo.transform.position = pos;
        luobo.died += OnLuoboDied;
    }
}

