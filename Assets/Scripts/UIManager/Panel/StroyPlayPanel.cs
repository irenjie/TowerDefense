using Extensions;
using MTL.Event;
using System.Collections.Generic;
using UnityEngine.UI;

namespace MUI {
    public class StroyPlayPanel : BasePanel {
        public void PlayStory(List<string> dialogue) {
            if (dialogue == null || dialogue.Count == 0)
                UIManager.Front.Close(this);

            root.Find<Button>("skip").BindListener(() => {
                UIManager.Front.Close(this);
            });

            Button dialogBtn = root.Find<Button>("dialogBG");

            Text contentText = root.Find<Text>("content");
            SetContentText(0);

            void SetContentText(int index) {
                contentText.text = dialogue[index];
                if (index < dialogue.Count - 1) {
                    dialogBtn.BindListener(() => {
                        SetContentText(index + 1);
                    });
                } else {
                    dialogBtn.BindListener(() => {
                        UIManager.Front.Close(this);
                        EventManager.Get().Fire(this, (int)EventID.StoryPlayOver, null);
                    });
                }
            }
        }
    }
}
