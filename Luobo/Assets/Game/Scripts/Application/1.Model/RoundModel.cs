using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

// ****************************************************************
// 功能：回合数据
// 创建：蔡泽深
// 时间：2017/06/05
// 修改内容：										修改者姓名：
// ****************************************************************

public class RoundModel : Model {
    private const float RoundInterval = 3f;     // 回合间隔时间
    private const float SpawnInterval = 1f;     // 出怪间隔时间

    private List<Round> rounds;                 // 该关卡的所有的出怪信息
    private int roundIndex = -1;                // 当前回合的索引
    private bool allRoundsComplete = false;     // 是否所有的怪物都出来
    private Coroutine runRoundCor = null;         // 运行回合协程

    public override string Name {
        get {
            return Consts.M_Round;
        }
    }

    public int RoundIndex {
        get {
            return roundIndex;
        }
    }

    public int RoundTotal {
        get {
            return rounds.Count;
        }
    }

    public bool AllRoundsComplete {
        get {
            return allRoundsComplete;
        }
    }

    public void LoadLevel(Level level) {
        rounds = level.rounds;
        allRoundsComplete = false;
        roundIndex = -1;
    }

    public void StartRounds() {
        if (runRoundCor != null) {
            Game.Instance.StopCoroutine(runRoundCor);
        }
        runRoundCor = Game.Instance.StartCoroutine(RunRounds());
    }

    public void StopRounds() {
        if (runRoundCor != null) {
            Game.Instance.StopCoroutine(runRoundCor);
            runRoundCor = null;
        }
    }

    private IEnumerator RunRounds() {

        for (roundIndex = 0; roundIndex < rounds.Count; roundIndex++) {
            // 开始回合事件
            StartRoundArgs startRoundArgs = new StartRoundArgs {
                currentRound = roundIndex + 1,
                roundTotal = RoundTotal
            };
            SendEvent(Consts.E_StartRound, startRoundArgs);

            Round round = rounds[roundIndex];

            for (int j = 0; j < round.count; j++) {
                // 开始出怪事件
                SpawnMonsterArgs spawnMonsterArgs = new SpawnMonsterArgs {
                    monsterType = round.monster
                };
                SendEvent(Consts.E_SpawnMonster, spawnMonsterArgs);

                // 出怪间隔
                if (j < round.count - 1) {
                    yield return new WaitForSeconds(SpawnInterval);
                }
            }

            // 回合间隔
            if (roundIndex< rounds.Count-1) {
                yield return new WaitForSeconds(RoundInterval);
            }
        }

        // 出怪完成
        allRoundsComplete = true;
        roundIndex = rounds.Count - 1;
    }
}

