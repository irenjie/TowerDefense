using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MScene {
    public class CombatScene : BaseScene {
        private void Awake() {
            UIManager.Combat.Navigation<CombatPanel>("UI/CombatPanel.prefab");
        }
    }
}