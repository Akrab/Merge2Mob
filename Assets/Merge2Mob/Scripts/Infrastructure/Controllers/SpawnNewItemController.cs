using MergeTwoMob.DIMerge;
using MergeTwoMob.Infrastructure.Factories;
using MergeTwoMob.Merge2Mob.Scripts.Base;
using MergeTwoMob.RuntimeScripts;
using UnityEngine;
using UnityEngine.Events;

namespace MergeTwoMob.Infrastructure.Controllers
{
    public class SpawnNewItemController
    {
        private const string ITEM_BASE_NAME = "GardenSheers";
        [Inject] private ITimerController timerController;
        [Inject] private RuntimeMergeMap runtimeMergeMap;
        [Inject] private MergeItemFabric mergeItemFabric;

        private Timer timer;
        private UnityAction<float> tickAction;
        
        private void Tick(float value)
        {
            tickAction?.Invoke(value);
        }

        private void TimerEnd()
        {
            if (!runtimeMergeMap.HaveFreeSlot) return;

            MapNode slot = runtimeMergeMap.GetFreeSlot();
            slot.ItemView =
                mergeItemFabric.CreateMergeItem(ITEM_BASE_NAME, new Vector3(slot.Position.x, slot.Position.y, 0));
            slot.ItemView.gameObject.SetActive(true);
            
            if (runtimeMergeMap.HaveFreeSlot == false)
            {
                timer.Pause();
            }
        }
        
        public void StartGame(Vector2 min, Vector2 max)
        {
            timer = new Timer(Tick, TimerEnd);
            timer.PlayLoop(CONSTANTS.DURATION);
            timerController.AddTimer(timer);
            for (int i = 0; i < 4; i++)
            {
                TimerEnd();
            }
        }

        public void AddTickAction(UnityAction<float> tick)
        {
            tickAction = tick;
        }

        public void MergeCompleted()
        {
            timer.Resume();
        }
    }
}