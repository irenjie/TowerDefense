using Extensions;
using Helper;
using MScene;
using MTL.Data;
using MTL.Event;
using MUI;
using System;
using UnityEngine;
using UnityEngine.UI;


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

            SubscribeEvents();
            EventManager.Get().Fire(this, (int)EventID.LevelStart, null);
            PlayStroy();
        }

        /// <summary>
        /// 播放剧情
        /// </summary>
        public void PlayStroy() {
            var panel = UIManager.Front.PopUp<StroyPlayPanel>("UI/StoryPlayPanel.prefab");
            panel.PlayStory(levelConfig.stroyDesc);
            //EventManager.Get().Subscribe((int)EventID.StoryPlayOver, StartNextWave);
        }

        private void StartNextWave(object sender, GameEventArgs e) {
            LevelScene.instance.WaveManager.StartNextWave();
        }

        private void Victory(object sender, GameEventArgs e) {
            var panel = UIManager.Front.PopUp<BasePanel>("LevelVictoryPanel.prefab");
            panel.root.Find<Button>("midBottom/exit").BindListener(() => {
                MScene.LoadingScene.TransitionSceneWithLoading<MainScene>("Scenes/MainScene.unity", UnityEngine.SceneManagement.LoadSceneMode.Single, default);

            });

        }

        private void Defeat(object sender, GameEventArgs e) {
            var panel = UIManager.Front.PopUp<BasePanel>("LevelDefeatPanel.prefab");
            panel.root.Find<Button>("midBottom/exit").BindListener(() => {

            });
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
            eventManager.Subscribe((int)EventID.AllWaveCompleted, Victory);
        }

        private void UnsubscribeEvents() {
            EventManager eventManager = EventManager.Get();
            eventManager.UnSubscribe((int)EventID.WaveStart, StartNextWave);
            eventManager.UnSubscribe((int)EventID.AllWaveCompleted, Victory);
        }

        protected void OnDestroy() {
            UnsubscribeEvents();
        }
    }
}