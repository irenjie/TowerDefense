
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MTL.Combat {

    /// <summary>
    /// 生成敌人
    /// 一个level有多wave敌人，每个敌人固定延迟生成，每个wave生成的敌人由多个 EnemySpwanInstruction 获得，每个EnemySpwanInstruction包含一个类型的敌人及数量
    /// </summary>
    [Serializable]
    public struct EnemySpwanInstruction {
        public AssetReference enemy;
        public int number;
        private GameObject prefab;

        public async Task<GameObject> InstantiateEnemy(Vector3 birthPos, Quaternion rotation) {
            if (prefab == null) {
                var handle = enemy.InstantiateAsync();
                await handle.Task;
            }

            return UnityEngine.Object.Instantiate<GameObject>(prefab, birthPos, rotation);
        }

        public void ReleaseAsset() {
            if (prefab != null)
                enemy.ReleaseAsset();
        }
    }
}