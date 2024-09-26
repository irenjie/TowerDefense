using Helper;
using MTL.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MTL.Combat {
    /// <summary>
    /// 管理一个关卡的敌人生成
    /// 一个level有多wave敌人，每个敌人固定延迟生成，每个wave生成的敌人由多个 EnemySpwanInstruction 获得，每个EnemySpwanInstruction包含一个类型的敌人及数量
    /// 玩家点击继续才开始下一波次
    /// </summary>
    public class WaveManager : DelayBehaviour {
        [SerializeField] List<Wave> waves;
        public bool allWaveCompleted { get; private set; } = false;
        private int curWaveIndex = 0;
        public bool curWaveCompleted => waves[curWaveIndex].completed;

        #region 回调点
        #endregion

        private void Awake() {
            SubscribeEvents();
        }

        private void Update() {
            if (curWaveIndex >= waves.Count) {
                return;
            }
            waves[curWaveIndex].CheckCompleted();

            if (curWaveCompleted) {
                EventManager eventManager = EventManager.Get();
                eventManager.Fire(this, (int)EventID.WaveCompleted, null);

                if (curWaveIndex < waves.Count - 1)
                    curWaveIndex++;
                else {
                    allWaveCompleted = true;
                    eventManager.Fire(this, (int)EventID.AllWaveCompleted, null);
                }
            }
        }

        public IEnumerator StartNextWave() {
            if (curWaveIndex >= waves.Count)
                yield break;

            yield return waves[curWaveIndex].StartWave();
        }

        public void SubscribeEvents() {
        }

        public void UnsubscribeEvents() {

        }

        private void OnDestroy() {
            UnsubscribeEvents();
        }
    }
}