using System;
using System.Collections.Generic;
using UnityEngine;

namespace MergeTwoMob.Infrastructure.Controllers
{
    public interface ITickable
    {
        void Tick();
    }

    public interface ILateTickable
    {
        void LateTick();
    }

    public interface IUpdateController
    {
        void Add(object data);
        void Remove(object data);
    }
    
    public class UpdateController : MonoBehaviour, IUpdateController
    {
        private List<ITickable> tickables = new();
        private List<ILateTickable> lateTickables = new();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            for (int i = 0; i < tickables.Count; i++)
            {
                tickables[i].Tick();
            }
        }

        private void LateUpdate()
        {
            for (int i = 0; i < lateTickables.Count; i++)
            {
                lateTickables[i].LateTick();
            }
        }

        public void Add(object data)
        {
            if (data is ITickable tick) tickables.Add(tick);
            if (data is ILateTickable lateTick) lateTickables.Add(lateTick);
        }

        public void Remove(object data)
        {
            if (data is ITickable tick) tickables.Remove(tick);
            if (data is ILateTickable lateTick) lateTickables.Remove(lateTick);
        }
    }
}