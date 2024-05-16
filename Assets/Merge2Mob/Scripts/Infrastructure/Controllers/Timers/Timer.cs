using UnityEngine;
using UnityEngine.Events;

namespace MergeTwoMob.Infrastructure.Controllers
{
    public class Timer : ITimer
    {
        private UnityAction<float> tickListener;
        private UnityAction completedListener;

        private float duration = 0f;
        private float currentTick = 0;
        private bool isRun = false;
        public bool isLoop { get; private set; }
        
        public Timer(UnityAction<float> tickListener, UnityAction completedListener)
        {
            this.tickListener = tickListener;
            this.completedListener = completedListener;
        }

        public void Play(float duration)
        {
            this.duration = duration;
            currentTick = duration;
            isRun = true;
        }

        public void PlayLoop(float duration)
        {
            this.duration = duration;
            currentTick = duration;
            isRun = true;
            isLoop = true;
        }

        public void Reset()
        {
            tickListener?.Invoke(0);
            isRun = false;
        }

        public void Stop(bool invoke = false)
        {
            if (invoke)
            {
                tickListener?.Invoke(0);
                completedListener?.Invoke();
            }

            Pause();
        }

        public void Pause()
        {
            isRun = false;
        }

        public void Resume()
        {
            isRun = true;
        }

        public void Tick()
        {
            if(!isRun) return;
            currentTick -= Time.deltaTime;

            if (currentTick <= 0)
            {
                tickListener?.Invoke(0);
                completedListener?.Invoke();

                if (isLoop && duration > 0)
                {
                    currentTick = duration;
                    return;
                }
                isRun = false;
                return;
            }
            
            tickListener?.Invoke(currentTick);
        }
    }
}