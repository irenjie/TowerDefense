using Helper;
using MTL.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MTL.Combat {

    /// <summary>
    /// ���ɵ���
    /// һ��level�ж�wave���ˣ�ÿ�����˹̶��ӳ����ɣ�ÿ��wave���ɵĵ����ɶ�� EnemySpwanInstruction ��ã�ÿ��EnemySpwanInstruction����һ�����͵ĵ��˼�����
    /// </summary>
    [Serializable]
    public class Wave {

        [SerializeField] Transform _birthPos;
        [SerializeField] Path path;
        private Vector3 birthPos => _birthPos.position;
        /// <summary>
        /// ��ʼ���ӳ����ɵ�һ������
        /// </summary>
        public float waveStartDelay;
        /// <summary>
        /// ÿλ���������ӳ�
        /// </summary>
        public float spawnDelay = 1f;
        /// <summary>
        /// ������� + ȫ������
        /// </summary>
        public bool completed { get; private set; } = false;

        [SerializeField] List<EnemySpwanInstruction> spwanInstructions;

        #region �ص���

        public event UnityAction OnWaveStart;
        // �Ⲩ���е�������ʱ����
        public event UnityAction OnSpwanCompleted;
        #endregion

        /// <summary>
        /// ����Ƿ�������� + ȫ������
        /// </summary>
        public void CheckCompleted() {

        }

        public IEnumerator StartWave() {
            OnWaveStart?.Invoke();

            yield return CoroutineHelper.WaitForSeconds(waveStartDelay);

            var ratI = Quaternion.identity;
            foreach (var instruction in spwanInstructions) {
                // ���ɵ���
                for (int i = 0; i < instruction.number; i++) {
                    GameObject enemyGO = instruction.InstantiateEnemy(birthPos, ratI).Result;
                    yield return CoroutineHelper.WaitForSeconds(spawnDelay);
                }
                instruction.ReleaseAsset();
            }
            OnSpwanCompleted?.Invoke();
        }


    }
}