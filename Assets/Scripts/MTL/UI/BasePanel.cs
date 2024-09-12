using Helper;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MUI {
    public class BasePanel : DelayBehaviour {
        protected Canvas canvas;
        protected RectTransform root;
        private GraphicRaycaster raycaster;
        [SerializeField] private bool _isFullScreen;
        public bool IsFullScreen => _isFullScreen;
        public int sortingOrder {
            get {
                return canvas.sortingOrder;
            }
            set {
                canvas.sortingOrder = value;
            }
        }

        protected virtual void Awake() {
            root = transform.Find("Canvas") as RectTransform;
            canvas = root.GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
            raycaster = root.GetComponent<GraphicRaycaster>();
        }

        public void SetGraphic(bool enable) {
            canvas.enabled = enable;
        }

        public void SetGraphicRaycast(bool enable) {
            raycaster.enabled = enable;
        }

        public void PlayShowEffect() {

        }

        public void PlayCloseEffect(UnityAction closeAction) {
            closeAction?.Invoke();
        }

    }
}