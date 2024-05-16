﻿using System.Reflection;

namespace MergeTwoMob.DIMerge
{
    public class IsometricInjector
    {
        private AttributeInfoContainer attributeInfoContainer = new();
        
        public void Inject(object obj, IContainer container)
        {
            var info = attributeInfoContainer.Get(obj.GetType());
            
            foreach (var field in info.Fields)
                FieldInject(field, obj, container);
            
            foreach (var t in info.Properties)
                PropertyInjector(t, obj, container);
        }

        public void InjectDuty( IContainer container)
        {
            if (!attributeInfoContainer.HaveEmpty) return;

            foreach (var field in attributeInfoContainer.DutyFieldContainer)
            {
                if (!container.ContainsResolve(field.Key)) continue;

                var queue = field.Value;
                while (queue.Count > 0)
                {
                    var item = queue.Dequeue();
                    var obj = container.Resolve(item.TargetType);
                    if (obj == null)
                        continue;

                    FieldInject(item.FieldInfo, obj, container);
                }
                
            }
            
            foreach (var property in attributeInfoContainer.DutyPropertyContainer)
            {
                if (!container.ContainsResolve(property.Key)) continue;

                var queue = property.Value;
                while (queue.Count > 0)
                {
                    var item = queue.Dequeue();
                    var obj = container.Resolve(item.TargetType);
                    if (obj == null)
                        continue;

                    PropertyInjector(item.PropertyInfo, obj, container);
                }
                
            }
         
        }

        private void FieldInject(FieldInfo field, object instance, IContainer container)
        {
            var data = container.Resolve(field.FieldType);

            if (data == null)
            {
                attributeInfoContainer.AddEmptyField(instance.GetType(), field);
                return;
            }

            field.SetValue(instance, data);
        }

        private void PropertyInjector(PropertyInfo property, object instance, IContainer container)
        {
            var data = container.Resolve(property.PropertyType);

            if (data == null)
            {
                attributeInfoContainer.AddEmptyProperty(instance.GetType(), property);
                return;
            }
            property.SetValue(instance, container.Resolve(property.PropertyType));
        }
        
        
    }
}