using Helper;
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

                if (mode == LoadSceneMode.Additive) {
                    // 若为additive加载，加载完成后手动卸载loadingScene
                    SceneTransition<T> wrapSceneTransition = new SceneTransition<T>();
                    wrapSceneTransition.transitionEnd += targetScene => {
                        MySceneManager.Get().UnloadScene<LoadingScene>(default);
                        sceneTransition.RaiseTransitionEnd(targetScene);
                    };

                    MySceneManager.Get().LoadScene<T>(address, mode, wrapSceneTransition);
                } else {
                    MySceneManager.Get().LoadScene<T>(address, mode, sceneTransition);
                }

            };

            MySceneManager.Get().LoadScene<LoadingScene>("Scenes/LoadingScene.unity", mode, loadingSceneTrans);
        }
    }
}