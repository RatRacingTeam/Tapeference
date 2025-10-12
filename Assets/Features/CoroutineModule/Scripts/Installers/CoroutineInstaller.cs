using UnityEngine;
using Zenject;

namespace Features.CoroutineModule.Scripts.Installers {
    public class CoroutineInstaller : Installer<CoroutineInstaller> {
        public override void InstallBindings() {
            Container.BindInterfacesTo<CoroutineRunner>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}