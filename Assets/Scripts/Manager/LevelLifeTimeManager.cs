using Helper;
using MScene;
using MTL.Data;
using MTL.Event;
using MUI;
using System;
using UnityEngine;


namespace MTL.Combat {
    /// <summary>
    /// ����ؿ��������ڣ�����չʾ����һ��...��ʤ��
    /// </summary>
    public class LevelLifeTimeManager : MonoBehaviour {
        LevelConfig levelConfig;
        CombatPanel combatPanel;
        // ��ǰ����
        private int waveNum = 0;
        // ��ǰ����
        private int energyNum = 0;

        Health baseHealth;

        public void Init() {
            levelConfig = GameData.Get().selectedLevel;

            combatPanel = UIManager.Combat.Navigation<CombatPanel>("UI/CombatPanel.prefab");
            PlayStroy();
        }

        /// <summary>
        /// ���ž���
        /// </summary>
        public void PlayStroy() {
            var panel = UIManager.Front.PopUp<StroyPlayPanel>("UI/StoryPlayPanel.prefab");
            panel.PlayStory(levelConfig.stroyDesc);
        }

        private void StartNextWave(object sender, GameEventArgs e) {
            LevelScene.WaveManager.StartNextWave();
        }

        private void Victory() {

        }

        private void Defeat() {

        }

        public bool HasEnoughEnergyToBuildTower(int buildEnergy) {
            return energyNum >= buildEnergy;
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