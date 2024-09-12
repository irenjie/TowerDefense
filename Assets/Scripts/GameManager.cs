using Data;
using Helper;
using MScene;
using MUI;

namespace Program {
    public class GameManager : SingletonBehaviour<GameManager> {

        public void OnAppStart() {
            LevelHelper.Init();
            TowerHelper.Initialize();
            SkillHelper.Init();

            SceneTransition<MainScene> mainSceneTransition = new SceneTransition<MainScene>();
            mainSceneTransition.transitionEnd += mainScene => {
                MUI.UIManager.Front.Navigation<MainPanel>("UI/MainPanel.prefab");
            };

            MySceneManager.Get().LoadScene<MainScene>("Scenes/MainScene.unity", UnityEngine.SceneManagement.LoadSceneMode.Single, mainSceneTransition);
        }

        private void Transition_transitionEnd(MainScene obj) {
            throw new System.NotImplementedException();
        }

        private void OnApplicationFocus(bool focus) {
            if (!focus) {
                GameData.Save();
            }
        }
    }
}