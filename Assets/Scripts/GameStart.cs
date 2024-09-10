
using UnityEngine;

namespace Program {
    public class GameStart : MonoBehaviour {
        void Start() {
            GameManager.Get().OnAppStart();
        }

    }
}