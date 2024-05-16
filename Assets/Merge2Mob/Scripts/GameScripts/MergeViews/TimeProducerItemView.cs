using MergeTwoMob.DataModels.Merge;
using MergeTwoMob.DIMerge;
using MergeTwoMob.Infrastructure.Controllers;
using TMPro;
using UnityEngine;

namespace MergeTwoMob.GameScripts.MergeViews
{
    public class TimeProducerItemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer icon;
        [SerializeField] private TextMeshProUGUI timerText;
        
        [Inject] private ITimerController timerController;
       
        private Timer timer;
        private int oldValueTimer = 0;

        private void Tick(float value)
        {
            int newValue = (int)value;
            if (newValue != oldValueTimer)
            {
                PrintUI(((int)value + 1).ToString());
                oldValueTimer = newValue;
            }
        }

        private void PrintUI(string text)
        {
            timerText.text = text;
        }

        private void TimerEnd()
        {
         
        }

        public void SetData(ITimeMergeProducerModel meta)
        {
            timer = new Timer(Tick, TimerEnd);
            timerController.AddTimer(timer);
            icon.sprite = meta.Icon;
#if UNITY_EDITOR
            gameObject.name = meta.Id;
#endif
            oldValueTimer = (int)meta.Duration;
            timer.PlayLoop(meta.Duration);
            PrintUI((oldValueTimer +1).ToString());
        }
    }
}