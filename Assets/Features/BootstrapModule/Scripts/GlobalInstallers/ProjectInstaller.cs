using Features.AssetsManagementModule.Scripts.Installers;
using Features.CoroutineModule.Scripts.Installers;
using Features.GameFlowModule.Scripts.Installers;
using UnityEngine;
using Zenject;

namespace Features.BootstrapModule.Scripts.GlobalInstallers {
    [CreateAssetMenu(fileName = nameof(ProjectInstaller), menuName = "Configurations/Bootstrap/GlobalInstallers/" + nameof(ProjectInstaller))]
    public class ProjectInstaller : ScriptableObjectInstaller {
        public override void InstallBindings() {
            CoroutineInstaller.Install(Container);
            GameFlowInstaller.Install(Container);
            AssetLoaderInstaller.Install(Container);
            DataInstaller.Install(Container);
            ConfigurationsInstaller.Install(Container);
        }
    }
}