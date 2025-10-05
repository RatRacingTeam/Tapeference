using Features.CoroutineModule.Scripts.Installers;
using UnityEngine;
using Zenject;

namespace Features.BootstrapModule.Scripts.Installers {
    [CreateAssetMenu(fileName = nameof(ProjectInstaller), menuName = "Configurations/Bootstrap/Installers/" + nameof(ProjectInstaller))]
    public class ProjectInstaller : ScriptableObjectInstaller {
        public override void InstallBindings() {
            CoroutineInstaller.Install(Container);
        }
    }
}