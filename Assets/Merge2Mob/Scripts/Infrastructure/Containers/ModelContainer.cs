using System;
using System.Collections.Generic;
using MergeTwoMob.DataModels;

namespace MergeTwoMob.Infrastructure.Containers
{
    public class ModelContainer
    {
        private readonly Dictionary<Type, BaseModel> containers = new();

        public void Add(BaseModel container)
        {
            if (containers.ContainsKey(container.GetType()) == false)
            {
                containers[container.GetType()] = container;
            }
        }

        public T Get<T>() where T : BaseModel
        {
            containers.TryGetValue(typeof(T), out var model);
            return (T)model;
        }

        public void Remove<T>()
        {
            containers.Remove(typeof(T));
        }
    }
}