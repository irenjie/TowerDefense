using Helper;
using MTL.Event;
using MUI;
using UnityEngine.SceneManagement;

namespace MScene {
    public class LoadingScene : BaseScene {
        private void Awake() {
            UnityDebugHelper.Log("loadingscene");
        }

        public static void TransitionSceneWithLoading<T>(string address, LoadSceneMode mode, SceneTransition<T> sceneTransition, bool clearUIWhenLoad = true) where T : BaseScene {
            SceneTransition<LoadingScene> loadingSceneTrans = new SceneTransition<LoadingScene>();

            loadingSceneTrans.transitionEnd += loadingScene => {
                if (clearUIWhenLoad) {
                    UIManager.ClearAll();
                }

                EventManager.Get().Fire(loadingScene, (int)EventID.SwitchScene, null);

                SceneTransition<T> wrapSceneTransition = new SceneTransition<T>();
                wrapSceneTransition.transitionEnd += targetScene => {
                    // 若为additive加载，加载完成后手动卸载loadingScene
                    if (mode == LoadSceneMode.Additive)
                        MySceneManager.Get().UnloadScene<LoadingScene>(default);
                    sceneTransition.RaiseTransitionEnd(targetScene);

                    EventManager.Get().Fire(targetScene, (int)EventID.SwitchScene, null);
                };

                MySceneManager.Get().LoadScene<T>(address, mode, wrapSceneTransition);
            };

            MySceneManager.Get().LoadScene<LoadingScene>("Scenes/LoadingScene.unity", mode, loadingSceneTrans);
        }
    }
}