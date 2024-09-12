using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Helper {
    /// <summary>
    /// ��Դ���ذ�����
    /// </summary>
    public class LoaderHelper :SingletonBehaviour<LoaderHelper>{

        #region ͬ������
        public T GetAsset<T>(string address) {
            var handle = Addressables.LoadAssetAsync<T>(address);
            var asset = handle.WaitForCompletion();
            return asset;
        }

        public GameObject InstantiatePrefab(string address, Transform parent = null, bool InstantiateInWorldSpace = false) {
            var handle = Addressables.InstantiateAsync(address, parent, InstantiateInWorldSpace);
            return handle.WaitForCompletion();
        }
        #endregion

        #region

        #endregion
    }
}