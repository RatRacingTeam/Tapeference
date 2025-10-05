using System.Collections;
using UnityEngine;

namespace Features.CoroutineModule.Scripts {
    public interface ICoroutineRunner {
        public Coroutine StartCoroutine(IEnumerator coroutine);
        public void StopCoroutine(IEnumerator coroutine);
    }
}