using Data;
using Extensions;
using Helper;
using MTL.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MUI {
    public class CombatPanel : BasePanel {
        Transform towerPanel;
        Transform skillPanel;

        private void Start() {
            GameData gameData = GameData.Get();

            #region ÅÚËþ
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
            #endregion

            #region ¼¼ÄÜ
            if (gameData.selectedSkills.Count == 0) {
                gameData.selectedSkills.Add(1001);
            }
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
            #endregion


        }
    }
}