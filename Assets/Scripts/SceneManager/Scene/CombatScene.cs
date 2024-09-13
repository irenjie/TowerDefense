using Data;
using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MScene {
    public class CombatScene : BaseScene {
        public LevelConfig levelConfig;

        private void Awake() {
            levelConfig = GameData.Get().selectedLevel;
            UIManager.Combat.Navigation<CombatPanel>("UI/CombatPanel.prefab");
        }
    }
}