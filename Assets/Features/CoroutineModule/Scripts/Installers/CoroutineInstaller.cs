using UnityEngine;
using Zenject;

namespace Features.CoroutineModule.Scripts.Installers {
    public class CoroutineInstaller : Installer<CoroutineInstaller> {
        public override void InstallBindings() {
            CoroutineRunner coroutineRunner = Object.FindFirstObjectByType<CoroutineRunner>();

            Container.BindInterfacesTo<CoroutineRunner>()
                .FromInstance(coroutineRunner)
                .AsSingle()
                .NonLazy();
        }
    }
}