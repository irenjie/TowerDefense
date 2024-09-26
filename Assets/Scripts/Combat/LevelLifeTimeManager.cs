using Helper;
using MTL.Data;
using MTL.Event;
using MUI;
using System;
using UnityEngine;


namespace MTL.Combat {
    /// <summary>
    /// 管理关卡生命周期，剧情展示、下一波...、胜利
    /// </summary>
    public class LevelLifeTimeManager : SingletonBehaviour<LevelLifeTimeManager> {
        LevelConfig levelConfig;
        WaveManager waveManager;
        CombatPanel combatPanel;

        public void Init() {
            levelConfig = GameData.Get().selectedLevel;
            waveManager = GameObject.Find("WavaManager").GetComponent<WaveManager>();

            combatPanel = UIManager.Combat.Navigation<CombatPanel>("UI/CombatPanel.prefab");

        }

        private void SubscribeEvents() {
            EventManager eventManager = EventManager.Get();
            eventManager.Subscribe((int)EventID.WaveStart, StartNextWave);
        }

        private void UnsubscribeEvents() {
            EventManager eventManager = EventManager.Get();
            eventManager.UnSubscribe((int)EventID.WaveStart,StartNextWave);
        }

        private void StartNextWave(object sender, GameEventArgs e) {
            waveManager.StartNextWave();
        }

        private void OnDestroy() {
           UnsubscribeEvents();
        }
    }
}