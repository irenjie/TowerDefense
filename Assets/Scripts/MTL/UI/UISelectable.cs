using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MTL.UI {
    public class UISelectable : MonoBehaviour, IPointerDownHandler {
        public void OnPointerDown(PointerEventData eventData) {
            if (DOTween.TweensByTarget(gameObject) != null)
                return;

            ///����Ч��
            const float duration = 0.1f;
            Vector2 baseScale = transform.localScale;
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOScaleX(0.95f * baseScale.x, duration));
            sequence.Join(transform.DOScaleY(1.05f * baseScale.y, duration));
            sequence.Append(transform.DOScaleX(1.05f * baseScale.x, duration));
            sequence.Join(transform.DOScaleY(0.95f * baseScale.y, duration));
            sequence.Append(transform.DOScaleX(1.0f * baseScale.x, duration));
            sequence.Join(transform.DOScaleY(1.0f * baseScale.y, duration));
            sequence.SetId(transform.GetInstanceID());
            sequence.SetTarget(gameObject);
        }
    }
}