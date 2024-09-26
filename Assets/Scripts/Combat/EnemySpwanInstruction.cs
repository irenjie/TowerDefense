
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MTL.Combat {

    /// <summary>
    /// ���ɵ���
    /// һ��level�ж�wave���ˣ�ÿ�����˹̶��ӳ����ɣ�ÿ��wave���ɵĵ����ɶ�� EnemySpwanInstruction ��ã�ÿ��EnemySpwanInstruction����һ�����͵ĵ��˼�����
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