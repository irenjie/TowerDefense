using MTL.Data;
using MTL.Event;
using Extensions;
using Helper;
using MTL.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using MScene;
using System;
using MTL.Combat;

namespace MUI {
    public class CombatPanel : BasePanel {
        #region 游戏对象
        Transform towerPanel;
        Transform skillPanel;
        Button btn_setting;
        Button btn_pause;
        Button btn_timeScale;
        Transform dialogueBox;
        Text pauseText;
        Text text_wave;
        Text text_health;
        Text text_energe;
        #endregion

        #region 游戏状态
        PauseState pauseState;
        float curTimeScale = 1f;
        #endregion

        #region 战斗数据
        #endregion

        WaveManager waveManager;

        private IEnumerator Start() {
            SubscriptEvent();
            GameData gameData = GameData.Get();
            waveManager = LevelScene.instance.WaveManager;

            #region 左上角
            {
                Transform legtTop = root.Find("leftTop");
                text_wave = legtTop.Find<Text>("wave/Text");
                text_health = legtTop.Find<Text>("health/Text");
                text_energe = legtTop.Find<Text>("energe/Text");


            }
            #endregion

            #region 炮塔
            {
                towerPanel = root.Find("rightBottom/towerPanel");
                towerPanel.DestroyAllChilds();
                GameObject towerBtnPrefab = LoaderHelper.Get().GetAsset<GameObject>("UI/CombatPanel/towerBtn.prefab");
                foreach (var selectedTower in gameData.selectedTowers) {
                    TowerConfig tower = TowerHelper.GetTowerConfigById(selectedTower);
                    Transform towerBtnTF = Instantiate(towerBtnPrefab, towerPanel).transform;
                    towerBtnTF.name = tower.realID;
                    towerBtnTF.Find<AddressableImage>("icon").SetSprite(tower.btnIconAddress);
                    towerBtnTF.GetComponent<Button>().BindListener(() => {
                        LevelScene.instance.BuildManager.curDragTowerRealID = tower.realID;
                    });
                }
                Addressables.Release(towerBtnPrefab);
            }
            #endregion

            #region 技能
            {
                skillPanel = root.Find("leftBottom/skillPanel");
                skillPanel.DestroyAllChilds();
                GameObject skillBtnPrefab = LoaderHelper.Get().GetAsset<GameObject>("UI/CombatPanel/towerBtn.prefab");
                foreach (var selectedSkill in gameData.selectedSkills) {
                    SkillConfig skill = SkillHelper.GetSkillConfigById(selectedSkill);
                    Transform skillBtnTF = Instantiate(skillBtnPrefab, skillPanel).transform;
                    skillBtnTF.name = skill.id.ToString();
                    skillBtnTF.Find<AddressableImage>("icon").SetSprite(skill.btnIconAddress);
                }
                Addressables.Release(skillBtnPrefab);
            }
            #endregion

            #region 右上角
            {
                Transform rightTop = root.Find("rightTop");
                btn_setting = rightTop.Find<Button>("setting");
                btn_pause = rightTop.Find<Button>("pause");
                btn_timeScale = rightTop.Find<Button>("timeScale");

                btn_setting.BindListener(() => {
                    UIManager.Front.PopUp<SettingPanel>("UI/SettingPanel.prefab");
                });

                #region 暂停、下一波
                pauseState = PauseState.WaitingWaveStart;
                pauseText = btn_pause.transform.Find<Text>("Text");
                btn_pause.BindListener(() => {
                    switch (pauseState) {
                        case PauseState.WaitingWaveStart:
                            pauseState = PauseState.InProgress;
                            pauseText.text = "暂停";
                            EventManager.Get().Fire(btn_pause.gameObject, (int)EventID.WaveStart, null);
                            break;
                        case PauseState.InProgress:
                            Time.timeScale = 0;
                            pauseText.text = "继续";
                            pauseState = PauseState.Paused;
                            EventManager.Get().Fire(btn_pause.gameObject, (int)EventID.GamePause, new BoolEventArgs(true));
                            break;
                        case PauseState.Paused:
                            Time.timeScale = curTimeScale;
                            pauseText.text = "暂停";
                            pauseState = PauseState.InProgress;
                            EventManager.Get().Fire(btn_pause.gameObject, (int)EventID.GamePause, new BoolEventArgs(false));
                            break;
                    }
                });
                #endregion

                Text ScaleText = btn_timeScale.transform.Find<Text>("Text");
                btn_timeScale.BindListener(() => {
                    curTimeScale = Time.timeScale == 1 ? 2 : 1;
                    Time.timeScale = curTimeScale;
                    ScaleText.text = $"{(int)curTimeScale}X";

                    EventManager.Get().Fire(btn_timeScale.gameObject, (int)EventID.ChangeTimeScale, new FloatEventArgs(curTimeScale));

                });
            }
            #endregion
            yield return null;

            Transform midBottom = root.Find("midBottom");
            midBottom.DestroyAllChilds();
        }

        private void Update() {
            text_wave.text = $"{waveManager.curWaveIndex + 1}/{waveManager.waveCount}";
        }

        private void SubscriptEvent() {
            EventManager eventManager = EventManager.Get();
            // 一波完成
            eventManager.Subscribe((int)EventID.WaveCompleted, OnWaveCompleted);
        }

        private void UnSubscriptEvent() {
            EventManager eventManager = EventManager.Get();
            eventManager.UnSubscribe((int)EventID.WaveCompleted, OnWaveCompleted);
        }

        private void OnWaveCompleted(object sender, GameEventArgs e) {
            pauseState = PauseState.WaitingWaveStart;
            pauseText.text = "下一波";
            Time.timeScale = curTimeScale = 1;
        }

        private void OnDestroy() {
            UnSubscriptEvent();
        }
    }

    enum PauseState {
        WaitingWaveStart,
        InProgress,
        Paused,
    }

}