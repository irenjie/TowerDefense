using MTL.Data;
using Extensions;
using Helper;
using MScene;
using MTL.Debug;
using MUI;

namespace Program {
    public class GameManager : SingletonBehaviour<GameManager> {

        public void OnAppStart() {
            DontDestroyOnLoad(gameObject);

            LevelHelper.Init();
            TowerHelper.Initialize();
            SkillHelper.Init();
            EnemyHelper.Initialize();

#if Debug_Mode || Develop_Mode
            DebugMenu debugMenu = LoaderHelper.Get().InstantiatePrefab("Debug/DebugMenu.prefab").transform.Find<DebugMenu>("DebugMenu");
            DontDestroyOnLoad(debugMenu.transform.parent.gameObject);

#endif

            MySceneManager.Get().LoadScene<MainScene>("Scenes/MainScene.unity", UnityEngine.SceneManagement.LoadSceneMode.Single, default);
        }

        private void OnApplicationFocus(bool focus) {
            if (!focus) {
                GameData.Save();
            }
        }
    }
}