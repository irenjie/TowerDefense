using Extensions;
using MTL.Combat;
using MTL.Data;
using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MScene {
    public class LevelScene : BaseScene {
        public static LevelScene instance { get; private set; }

        public WaveManager WaveManager { get; private set; }
        public CameraManager CameraManager { get; private set; }
        public BuildManager BuildManager { get; private set; }
        public LevelLifeTimeManager LevelLifeTimeManager { get; private set; }
        public TowerManager TowerManager { get; private set; }
        public EnemyManager EnemyManager { get; private set; }

        private void Awake() {
            instance = this;

            Transform general = GameObject.Find("General").transform;
            WaveManager = general.Find<WaveManager>("WaveManager");
            CameraManager = general.Find<CameraManager>("CameraManager");
            BuildManager = general.Find<BuildManager>("BuildManager");
            LevelLifeTimeManager = general.Find<LevelLifeTimeManager>("LevelLifeManager");
            TowerManager = general.Find<TowerManager>("TowerManager");
            EnemyManager = general.Find<EnemyManager>("EnemyManager");
        }

        private void Start() {
            LevelLifeTimeManager.LevelStart();
        }

        private void OnDestroy() {
        }
    }
}