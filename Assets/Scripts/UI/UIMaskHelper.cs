using DG.Tweening;
using Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    /// <summary>
    /// UI ���������
    /// </summary>
    public class UIMaskHelper : SingletonBehaviour<UIMaskHelper> {
        Canvas canvas;
        GraphicRaycaster raycaster;
        Image mask;
        RectTransform rectTransform;

        private void Awake() {
            canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
            rectTransform = transform as RectTransform;
            rectTransform.anchorMin = rectTransform.anchorMax = MathHelper.ZeroVector2;
            raycaster = gameObject.AddComponent<GraphicRaycaster>();

            mask = new GameObject("mask").AddComponent<Image>();
            mask.transform.SetParent(transform, false);
            mask.rectTransform.anchorMin = MathHelper.ZeroVector2;
            mask.rectTransform.anchorMax = MathHelper.OneVector2;
            mask.color = Color.clear;
        }

        public void UpdateLayer(int layer, float alpha = 0.67f, bool anim = true) {
            DOTween.Kill(gameObject);
            canvas.sortingOrder = layer;
            if (anim) {
                mask.DOFade(alpha, 0.3f).SetTarget(gameObject);
            }
        }
    }
}