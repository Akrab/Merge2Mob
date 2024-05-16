using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MergeTwoMob.DIMerge
{
    public class AttributeFieldDuty
    {
        private readonly Type targetType;
        private readonly FieldInfo fieldInfo;

        public Type TargetType => targetType;
        public FieldInfo FieldInfo => fieldInfo;

        public AttributeFieldDuty(Type target, FieldInfo fieldInfo)
        {
            targetType = target;
            this.fieldInfo = fieldInfo;
        }

    }

    public class AttributePropertyDuty
    {
        private readonly Type targetType;
        private readonly PropertyInfo propertyInfo;

        public Type TargetType => targetType;
        public PropertyInfo PropertyInfo => propertyInfo;

        public AttributePropertyDuty(Type target, PropertyInfo propertyInfo)
        {
            targetType = target;
            this.propertyInfo = propertyInfo;
        }

    }

    public class AttributeInfoContainer
    {
        private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        private readonly Dictionary<Type, AttributeObjInfo> container = new();

        private readonly Dictionary<Type, Queue<AttributeFieldDuty>> dutyFieldContainer = new();
        private readonly Dictionary<Type, Queue<AttributePropertyDuty>> dutyPropertyContainer = new();

        private int totalEmptyFields = 0;
        private int totalEmptyProperties = 0;
        public bool HaveEmpty => totalEmptyFields > 0 || totalEmptyProperties > 0;

        public Dictionary<Type, Queue<AttributeFieldDuty>> DutyFieldContainer => dutyFieldContainer;
        public Dictionary<Type, Queue<AttributePropertyDuty>> DutyPropertyContainer => dutyPropertyContainer;

        public AttributeObjInfo Get(Type type)
        {
            if (!container.TryGetValue(type, out AttributeObjInfo data))
            {
                data = CreateInfo(type);
                container.Add(type, data);
            }

            return data;
        }

        private AttributeObjInfo CreateInfo(Type target) =>
            new AttributeObjInfo(GetFields(target), GetProperties(target));

        private FieldInfo[] GetFields(Type target) => target
            .GetFields(Flags)
            .Where(f => f.IsDefined(typeof(InjectAttribute)))
            .ToArray();

        private PropertyInfo[] GetProperties(Type target) =>
            target
                .GetProperties(Flags)
                .Where(p => p.CanWrite && p.IsDefined(typeof(InjectAttribute)))
                .ToArray();

        public void AddEmptyField(Type instanceType, FieldInfo field)
        {
            totalEmptyFields++;

            var newDuty = new AttributeFieldDuty(instanceType, field);

            if (dutyFieldContainer.TryGetValue(field.FieldType, out var queue))
            {
                queue.Enqueue(newDuty);
                return;
            }

            queue = new Queue<AttributeFieldDuty>();
            queue.Enqueue(newDuty);
            dutyFieldContainer.Add(field.FieldType, queue);
        }

        public void AddEmptyProperty(Type type, PropertyInfo propertyInfo)
        {
            totalEmptyProperties++;
            var newDuty = new AttributePropertyDuty(type, propertyInfo);
            if (dutyPropertyContainer.TryGetValue(propertyInfo.PropertyType, out var queue))
            {
                queue.Enqueue(newDuty);
                return;
            }

            queue = new Queue<AttributePropertyDuty>();
            queue.Enqueue(newDuty);
            dutyPropertyContainer.Add(propertyInfo.PropertyType, queue);
        }
    }
}