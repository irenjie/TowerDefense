using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Helper {
    public class DelayBehaviour : MonoBehaviour {
        protected IEnumerator DelayDo(float delay, UnityAction action) {
            yield return CoroutineHelper.WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}