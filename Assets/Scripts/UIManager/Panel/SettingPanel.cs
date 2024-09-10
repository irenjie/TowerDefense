using Extensions;
using System.Collections;
using System.Collections.Generic;
using MUI;
using UnityEngine;
using UnityEngine.UI;

namespace MUI {
    public class SettingPanel : BasePanel {
        private void Start() {
            Transform left_mid_tf = root.Find("left_mid_btns");

            left_mid_tf.Find<Button>("btn_exit").BindListener(() => {
                UIManager.Front.Close(this);
            });
        }
    }
}