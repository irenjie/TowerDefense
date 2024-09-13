using Data;
using Extensions;
using Helper;
using System.Collections;
using System.Collections.Generic;
using MUI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using MTL.UI;
using MScene;
using UnityEngine.SceneManagement;

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

            foreach (LevelConfig config in LevelHelper.levelConfigs) {
                Transform levelTF = Instantiate(levelGO, content).transform;
                levelTF.name = config.ID.ToString();
                levelTF.Find<Text>("label").text = config.name.ToString();
                levelTF.Find<AddressableImage>("img").SetSprite(config.iconAddress);
                levelTF.GetComponent<Button>().BindListener(() => {
                    GameData.Get().selectedLevel = config;
                    MScene.LoadingScene.TransitionSceneWithLoading<CombatScene>(config.sceneAddress, LoadSceneMode.Single, default);
                });
            }
            Addressables.Release(levelGO);
        }
    }
}