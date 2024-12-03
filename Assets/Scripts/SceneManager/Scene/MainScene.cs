using Helper;
using MUI;

namespace MScene {
    public class MainScene : BaseScene {
        private void Awake() {
            MUI.UIManager.Front.Navigation<MainPanel>("UI/MainPanel.prefab");
        }
    }
}