using Data;
using Helper;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Program {
    public class GameManager : SingletonBehaviour<GameManager> {

        private void Awake() {
            var go = transform.parent;
            DontDestroyOnLoad(gameObject);

            OnAppStart();
        }

        void OnAppStart() {
            LevelHelper.Init();

            UIManager.Front.Navigation<MainPanel>("UI/MainPanel.prefab");
        }

        private void OnApplicationFocus(bool focus) {
            if (!focus) {
                GameData.Save();
            }
        }
    }
}