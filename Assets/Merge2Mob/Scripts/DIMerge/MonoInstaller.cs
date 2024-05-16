using UnityEngine;

namespace MergeTwoMob.DIMerge
{
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        [Inject] protected readonly DiContainer diContainer;
        public virtual void InstallBindings()
        {
            
        }
    }
}