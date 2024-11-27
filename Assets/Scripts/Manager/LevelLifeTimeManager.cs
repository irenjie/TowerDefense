using Helper;
using MScene;
using MTL.Data;
using MTL.Event;
using MUI;
using System;
using UnityEngine;


namespace MTL.Combat {
    /// <summary>
    /// 管理关卡生命周期，剧情展示、下一波...、胜利
    /// </summary>
    public class LevelLifeTimeManager : MonoBehaviour {
        LevelConfig levelConfig;
        CombatPanel combatPanel;
        // 当前波次
        private int waveNum = 0;
        // 当前能量
        private int energyNum = 0;

        Health baseHealth;

        public void LevelStart() {
            levelConfig = GameData.Get().selectedLevel;
            energyNum = GameData.Get().selectedLevel.startEnerge;

            combatPanel = UIManager.Combat.Navigation<CombatPanel>("UI/CombatPanel.prefab");
            PlayStroy();
        }

        /// <summary>
        /// 播放剧情
        /// </summary>
        public void PlayStroy() {
            var panel = UIManager.Front.PopUp<StroyPlayPanel>("UI/StoryPlayPanel.prefab");
            panel.PlayStory(levelConfig.stroyDesc);
            EventManager.Get().Subscribe((int)EventID.StoryPlayOver, StartNextWave);
        }

        private void StartNextWave(object sender, GameEventArgs e) {
            LevelScene.instance.WaveManager.StartNextWave();
        }

        private void Victory() {

        }

        private void Defeat() {

        }

        public bool HasEnoughEnergyToBuildTower(int buildEnergy) {
            return energyNum >= buildEnergy;
        }

        public void AddEnergyNum(int num) {
            energyNum = Math.Max(0, energyNum - num);
        }

        private void SubscribeEvents() {
            EventManager eventManager = EventManager.Get();
            eventManager.Subscribe((int)EventID.WaveStart, StartNextWave);
        }

        private void UnsubscribeEvents() {
            EventManager eventManager = EventManager.Get();
            eventManager.UnSubscribe((int)EventID.WaveStart, StartNextWave);
        }

        protected void OnDestroy() {
            UnsubscribeEvents();
        }
    }
}