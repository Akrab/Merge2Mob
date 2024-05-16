using Doozy.Runtime.UIManager.Components;
using MergeTwoMob.DIMerge;
using MergeTwoMob.Infrastructure.Controllers;
using MergeTwoMob.Merge2Mob.Scripts.Base;
using UnityEngine;

namespace MergeTwoMob.UI
{
    public class GameUI : BaseForm
    {
        [SerializeField] private UISlider uiSlider;

        [Inject] private SpawnNewItemController spawnNewItemController;
        
        private void Tick(float t)
        {
            uiSlider.value = t / CONSTANTS.DURATION;
        }

        public override void Show(bool instant = false)
        {
            spawnNewItemController.AddTickAction(Tick);
            base.Show(instant);
        }
        
        
        
        
    }
}