using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Utils {
    public class FaceCamera : MonoBehaviour {
        private Camera mainCamera;

        private void LateUpdate() {
            if (mainCamera == null)
                mainCamera = Camera.main;

            transform.rotation = mainCamera.transform.rotation;
        }
    }
}