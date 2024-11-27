using Helper;
using MScene;
using MTL.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace MTL.Combat {
    /// <summary>
    /// �������������뽨��������
    /// </summary>
    public class Buildable : MonoBehaviour {
        public bool HasTower { get; private set; } = false;
        public bool CanBuild { get; private set; } = false;
        private BaseTower curTower = null;
        [SerializeField]protected Transform towerPivot = null;

        private void Awake() {
            CanBuild = true;
            HasTower = false;
        }

        public void BuildTower(TowerConfig towerConfig) {
            CanBuild = false;
            GameObject towerGO = LoaderHelper.Get().InstantiatePrefab($"Tower/{towerConfig.realID}.prefab", towerPivot);
            towerGO.transform.localPosition = MathHelper.ZeroVector3;
            curTower = towerGO.GetComponent<BaseTower>();
            curTower.Init();

            LevelScene.instance.TowerManager.AddTower(curTower);
            LevelScene.instance.LevelLifeTimeManager.AddEnergyNum(towerConfig.energeCost);
        }

        /// <summary>
        /// ��ʾ����˵�
        /// </summary>
        public void ShowBuildMenu() {

        }

        /// <summary>
        /// ���ؽ���˵�
        /// </summary>
        public void HideBuildMenu() {

        }
    }
}