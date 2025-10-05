using UnityEngine;

namespace Features.BootstrapModule.Scripts {
    public class ProjectBootstrap : MonoBehaviour {
        [SerializeField] private GameObject _projectUtilitiesObject;

        private void Awake() =>
            DontDestroyOnLoad(_projectUtilitiesObject);

        private void Start() =>
            Debug.LogError("Bootstrapping project");
    }
}