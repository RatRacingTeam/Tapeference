using UnityEngine;

namespace Features.CoroutineModule.Scripts {
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner {
        private void OnDestroy() =>
            StopAllCoroutines();
    }
}