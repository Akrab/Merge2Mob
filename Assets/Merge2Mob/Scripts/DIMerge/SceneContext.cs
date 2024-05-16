using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MergeTwoMob.DIMerge
{
    public interface ISceneContext
    {
        void Install();
    }

    public class SceneContext : MonoBehaviour, ISceneContext
    {
        [Inject] private DiContainer diContainer;
        [SerializeField] private MonoInstaller[] monoInstallers;

        public void Install()
        {
            foreach (var installer in monoInstallers)
            {
                diContainer.BindInstance(installer);
                installer.InstallBindings();
            }

        }
    }
}