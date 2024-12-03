using Helper;
using MScene;
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
    /// 生成敌人
    /// 一个level有多wave敌人，每个敌人固定延迟生成，每个wave生成的敌人由多个 EnemySpwanInstruction 获得，每个EnemySpwanInstruction包含一个类型的敌人及数量
    /// </summary>
    [Serializable]
    public class Wave {

        [SerializeField] Transform _birthPos;
        [SerializeField] Path path;
        private Vector3 birthPos => _birthPos.position;
        /// <summary>
        /// 开始后延迟生成第一个敌人
        /// </summary>
        public float waveStartDelay;
        /// <summary>
        /// 每位敌人生成延迟
        /// </summary>
        public float spawnDelay = 1f;
        /// <summary>
        /// 生成完成
        /// </summary>
        public bool spwanCompleted { get; private set; }
        /// <summary>
        /// 生成完成 + 全部消灭
        /// </summary>
        public bool completed { get; private set; } = false;

        [SerializeField] List<EnemySpwanInstruction> spwanInstructions;

        #region 回调点

        public event UnityAction OnWaveStart;
        // 这波所有敌人生成时触发
        public event UnityAction OnSpwanCompleted;
        #endregion

        /// <summary>
        /// 检查是否生成完成 + 全部消灭
        /// </summary>
        public void CheckCompleted() {
            completed = spwanCompleted && LevelScene.instance.EnemyManager.ExistEnemyNum == 0;
        }

        public IEnumerator StartWave() {
            spwanCompleted = false;
            OnWaveStart?.Invoke();

            yield return CoroutineHelper.WaitForSeconds(waveStartDelay);

            var ratI = Quaternion.identity;
            foreach (var instruction in spwanInstructions) {
                // 生成敌人
                for (int i = 0; i < instruction.number; i++) {
                    //GameObject enemyGO = instruction.InstantiateEnemy(birthPos, ratI).Result;
                    GameObject enemyGO = instruction.InstantiateEnemy(birthPos, ratI, LevelScene.instance.WaveManager.transform);
                    Enemy enemy = enemyGO.GetComponent<Enemy>();
                    enemy.Init(path);
                    LevelScene.instance.EnemyManager.AddEnemy(enemy);
                    yield return CoroutineHelper.WaitForSeconds(spawnDelay);
                }
                instruction.ReleaseAsset();
            }
            spwanCompleted = true;
            OnSpwanCompleted?.Invoke();
        }


    }
}