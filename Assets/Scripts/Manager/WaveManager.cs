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
        private int _curWaveIndex = -1;
        public bool curWaveCompleted => waves[_curWaveIndex].completed;

        public int waveCount => waves.Count;
        public int curWaveIndex => _curWaveIndex;

        #region �ص���
        #endregion

        private void Awake() {
            SubscribeEvents();
        }

        private void Update() {
            if (_curWaveIndex < 0 || _curWaveIndex >= waves.Count || curWaveCompleted) {
                return;
            }
            waves[_curWaveIndex].CheckCompleted();

            if (curWaveCompleted) {
                EventManager eventManager = EventManager.Get();
                eventManager.Fire(this, (int)EventID.WaveCompleted, null);

                if (_curWaveIndex >= waves.Count - 1) {
                    allWaveCompleted = true;
                    eventManager.Fire(this, (int)EventID.AllWaveCompleted, null);
                }
            }
        }

        public void StartNextWave() {
            StartCoroutine(StartNextWaveReal());
        }

        private IEnumerator StartNextWaveReal() {
            ++_curWaveIndex;
            if (_curWaveIndex >= waves.Count)
                yield break;

            yield return waves[_curWaveIndex].StartWave();
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