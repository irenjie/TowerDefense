using Extensions;
using MTL.Combat;
using MTL.Data;
using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MScene {
    public class LevelScene : BaseScene {

        public static WaveManager WaveManager { get; private set; }
        public static CameraManager CameraManager { get; private set; }
        public static BuildManager BuildManager { get; private set; }
        public static LevelLifeTimeManager LevelLifeTimeManager { get; private set; }
        public static TowerManager TowerManager { get; private set; }

        private void Awake() {
            Transform general = GameObject.Find("General").transform;
            WaveManager = general.Find<WaveManager>("WaveManager");
            CameraManager = general.Find<CameraManager>("CameraManager");
            BuildManager = general.Find<BuildManager>("BuildManager");
            LevelLifeTimeManager = general.Find<LevelLifeTimeManager>("LevelLifeManager");
            TowerManager = general.Find<TowerManager>("TowerManagerr");
        }

        private void OnDestroy() {
            EnemyManager.Dispose();
        }
    }
}