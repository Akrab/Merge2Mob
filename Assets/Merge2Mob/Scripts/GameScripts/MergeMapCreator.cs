using MergeTwoMob.DIMerge;
using MergeTwoMob.Infrastructure.Containers;
using MergeTwoMob.Infrastructure.Controllers;
using MergeTwoMob.Infrastructure.Controllers.Inputs;
using MergeTwoMob.RuntimeScripts;
using MergeTwoMob.UI;
using UnityEngine;

namespace MergeTwoMob.GameScripts
{
    public class MergeMapCreator : MonoBehaviour
    {
        [SerializeField] private Camera cameraTarget;
        [SerializeField] private Vector2Int min;
        [SerializeField] private Vector2Int max;
        
        [Inject] private ITimerSettings timerController;
        [Inject] private DiContainer diContainer;
        [Inject] private SpawnNewItemController spawnNewItemController;
        [Inject] private UIContainer uiContainer;
        [Inject] private InputController inputController;
        [Inject] private MergeInputController mergeInputController;

        private void StartGame()
        {
            timerController.RunTimers();
            inputController.Enable();
            mergeInputController.StartGame();
        }
        
        public void Create()
        {
            mergeInputController.SetCamera(cameraTarget);
            RuntimeMergeMap runtimeMergeMap = new(5, 7, min);
            diContainer.BindInstance(runtimeMergeMap);
            spawnNewItemController.StartGame(min, max);
            
            uiContainer.GetForm<GameUI>().Show();

            MergeChainUI chainUI = uiContainer.GetForm<MergeChainUI>();
            chainUI.OnCloseEvent += StartGame;
            chainUI.Show();

        }
    }
}