using Data;
using Extensions;
using Helper;
using System.Collections;
using System.Collections.Generic;
using MUI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace MUI {
    public class LevelSelectPanel : MainPanel {
        ScrollRect levelScrollRect;

        private void Start() {
            root.Find<Button>("exit").BindListener(() => {
                UIManager.Front.Close(this);
            });

            root.Find<Button>("bottom/baike").BindListener(() => {
                UIManager.Front.PopUp<BaiKePanel>("UI/BaiKePanel.prefab");
            });

            levelScrollRect = root.Find<ScrollRect>("Levels/Levels");
            RectTransform content = levelScrollRect.transform.Find("viewport/content") as RectTransform;
            GameObject levelGO = LoaderHelper.Get().GetAsset<GameObject>("UI/LevelSelectPanel/Level.prefab");
            content.DestroyAllChilds();

            List<LevelConfig> levelConfigs = LevelHelper.GetLevelConfigs();
            foreach (LevelConfig config in levelConfigs) {
                Transform levelTF = Instantiate(levelGO, content).transform;
                levelTF.Find<Text>("label").text = config.ID.ToString();
            }
            Addressables.Release(levelGO);

        }
    }
}