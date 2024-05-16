using MergeTwoMob.DIMerge;
using MergeTwoMob.Infrastructure.Containers;
using MergeTwoMob.Infrastructure.Controllers;
using MergeTwoMob.Infrastructure.Controllers.Inputs;
using MergeTwoMob.Infrastructure.Factories;
using MergeTwoMob.Infrastructure.Services;
using MergeTwoMob.Merge2Mob.Scripts.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MergeTwoMob.Installers
{
    public class LobbyInstaller : MonoInstaller
    {

        [SerializeField] private ScriptableObject[] configs;
        [SerializeField] private UpdateController updateController;

        private void InitializeConfigs()
        {
            foreach (var item in configs)
            {
                diContainer.BindInstance(item);
            }
        }

        private void InstallContainers()
        {
            diContainer.BindNew<ModelContainer>();
            diContainer.BindNew<UIContainer>();
        }

        private void InstallControllers()
        {
            diContainer.BindInterface<IUpdateController>(updateController);
            diContainer.BindNew<MergeMapCreateController>();

            TimerController newTimerController = new TimerController();
            updateController.Add(newTimerController);
            diContainer.BindInterface<ITimerController>(newTimerController);
            diContainer.BindInterface<ITimerSettings>(newTimerController);

            diContainer.BindNew<SpawnNewItemController>();
            diContainer.BindNew<InputController>();
            diContainer.BindNew<MergeInputController>();
            diContainer.BindNew<MergeItemController>();
        }

        private void InstallServices()
        {
            BaseLoadService[] s = new BaseLoadService[]
            {
                new MergeItemService(),
                new MergeChainService()
            };

            foreach (var item in s)
            {
                item.Load();
                diContainer.BindInterfaces(item);
            }
        }

        private void InstallFactories()
        {
            diContainer.BindNew<MergeItemFabric>();
        }

        public override void InstallBindings()
        {
            InitializeConfigs();
            InstallContainers();
            InstallServices();
            InstallFactories();
            InstallControllers();

            SceneManager.LoadSceneAsync(CONSTANTS.UI_SCENE, LoadSceneMode.Additive);
        }
    }
}
