using Helper;
using MTL.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MTL.Combat {
    /// <summary>
    /// ����һ���ؿ��ĵ�������
    /// һ��level�ж�wave���ˣ�ÿ�����˹̶��ӳ����ɣ�ÿ��wave���ɵĵ����ɶ�� EnemySpwanInstruction ��ã�ÿ��EnemySpwanInstruction����һ�����͵ĵ��˼�����
    /// ��ҵ�������ſ�ʼ��һ����
    /// </summary>
    public class WaveManager : DelayBehaviour {
        [SerializeField] List<Wave> waves;
        public bool allWaveCompleted { get; private set; } = false;
        private int curWaveIndex = 0;
        public bool curWaveCompleted => waves[curWaveIndex].completed;

        #region �ص���
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