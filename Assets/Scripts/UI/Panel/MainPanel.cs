using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class MainPanel : BasePanel {
        private void Start() {
            Transform left_mid_tf = root.Find("left_mid_btns");

            left_mid_tf.Find<Button>("btn_start_game").BindListener(() => {
                UIManager.Front.PopUp<LevelSelectPanel>("UI/LevelSelectPanel.prefab");
            });

            left_mid_tf.Find<Button>("btn_settings").BindListener(() => {
                UIManager.Front.PopUp<SettingPanel>("UI/SettingPanel.prefab");
            });

            left_mid_tf.Find<Button>("btn_credits").BindListener(() => {

            });

            left_mid_tf.Find<Button>("btn_exit_game").BindListener(() => {
                Application.Quit();
            });
        }
    }
}