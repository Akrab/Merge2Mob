﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace MergeTwoMob.DIMerge
{
    public interface IContainer
    {
        bool ContainsResolve(Type target);
        object Resolve(Type target);
    }

    public class DiContainer : IContainer
    {
        private IsometricInjector Injector { get; set; } = new();

        private Dictionary<Type, IResolver> resolvers = new();

        public DiContainer()
        {
            var resolver = new ValueResolver(this);
            resolvers.Add(GetType(), resolver);
        }

        public DiContainer BindInterface<T>(object data)
        {
            if (resolvers.ContainsKey(typeof(T)))
                return this;

            var resolver = new ValueResolver(data);
            resolvers.Add(typeof(T), resolver);
            
            return this;
        }
        
        public DiContainer BindInterfaces(object data)
        {
            var interfaces = data.GetType().GetInterfaces();

            foreach (var iFace in interfaces)
            {
                var resolver = CreateResolver(data);
                if (resolver != null)
                    resolvers.Add(iFace, resolver);
            }

            return this;
        }

        public DiContainer BindInstance(object data)
        {
            AddToContainer(data);
            Inject(data);
            return this;
        }

        public DiContainer Inject(object data)
        {
            Injector.Inject(data, this);
            Injector.InjectDuty(this);
            return this;
        }
        
        public DiContainer BindNew<T>(out T obj) where T : new()
        {
            obj = new T();
            BindInstance(obj);
            return this;
        }

        public DiContainer BindNew<T>() where T : new()
        {
            BindNew<T>(out T obj);
            return this;
        }

        private void AddToContainer(object data)
        {
            var resolver = CreateResolver(data);
            if (resolver != null)
            {
                resolvers.Add(data.GetType(), resolver);
            }
        }

        private IResolver CreateResolver(object data)
        {
            Type type = data.GetType();
            if (resolvers.ContainsKey(type))
            {
                return null;
            }

            return new ValueResolver(data);
        }

        public object Resolve(Type target)
        {
            return resolvers.TryGetValue(target, out var resolver) ? resolver.Resolve() : null;
        }

        public bool ContainsResolve(Type target)
        {
            return resolvers.ContainsKey(target);
        }

        public T Resolve<T>() where T : class
        {
            return Resolve(typeof(T)) as T;
        }
    }
}