using MTL.Combat;
using MTL.Data;
using MUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MScene {
    public class LevelScene : BaseScene {
        private void Awake() {
            LevelLifeTimeManager.Get().Init();
        }
    }
}