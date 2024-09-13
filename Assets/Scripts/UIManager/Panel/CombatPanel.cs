using Data;
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

namespace MUI {
    public class CombatPanel : BasePanel {
        #region 游戏对象
        CombatScene combatScene;
        Transform towerPanel;
        Transform skillPanel;
        Button btn_setting;
        Button btn_pause;
        Button btn_timeScale;
        Transform dialogueBox;
        #endregion

        #region 游戏状态
        float curTimeScale = 1;
        bool gamePaused = false;
        #endregion

        #region 战斗数据
        #endregion


        private IEnumerator Start() {
            GameData gameData = GameData.Get();
            combatScene = FindFirstObjectByType<CombatScene>();

            #region 炮塔
            {
                towerPanel = root.Find("rightBottom/towerPanel");
                towerPanel.DestroyAllChilds();
                GameObject towerBtnPrefab = LoaderHelper.Get().GetAsset<GameObject>("UI/CombatPanel/towerBtn.prefab");
                foreach (var selectedTower in gameData.selectedTowers) {
                    TowerConfig tower = TowerHelper.GetTowerConfig(selectedTower);
                    Transform towerBtnTF = Instantiate(towerBtnPrefab, towerPanel).transform;
                    towerBtnTF.name = tower.realID;
                    towerBtnTF.Find<AddressableImage>("icon").SetSprite(tower.btnIconAddress);
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
                    SkillConfig skill = SkillHelper.GetSkillConfig(selectedSkill);
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

                Text pauseText = btn_pause.transform.Find<Text>("Text");
                btn_pause.BindListener(() => {
                    gamePaused = !gamePaused;
                    Time.timeScale = gamePaused ? 0 : curTimeScale;
                    pauseText.text = gamePaused ? "继续" : "暂停";

                    EventManager.Get().Fire(btn_pause.gameObject, (int)EventID.GamePause, new BoolEventArgs(gamePaused));
                });

                Text ScaleText = btn_timeScale.transform.Find<Text>("Text");
                btn_timeScale.BindListener(() => {
                    curTimeScale = Time.timeScale == 1 ? 2 : 1;
                    Time.timeScale = curTimeScale;
                    ScaleText.text = $"{(int)curTimeScale}X";

                    EventManager.Get().Fire(btn_timeScale.gameObject, (int)EventID.GameTimeScale, new FloatEventArgs(curTimeScale));

                });
            }
            #endregion
            yield return null;

            Transform midBottom = root.Find("midBottom");
            midBottom.DestroyAllChilds();

            PlayStroy();
        }

        /// <summary>
        /// 播放剧情
        /// </summary>
        public void PlayStroy() {
            var panel = UIManager.Front.PopUp<StroyPlayPanel>("UI/StoryPlayPanel.prefab");
            panel.PlayStory(GameData.Get().selectedLevel.stroyDesc);
        }

        private void SubscriptEvent() {

        }

        private void ClearEvent() {

        }
    }
}