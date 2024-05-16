using System.Reflection;

namespace MergeTwoMob.DIMerge
{
    public class AttributeObjInfo
    {
        public readonly FieldInfo[] Fields;
        public readonly PropertyInfo[] Properties;
        
        public AttributeObjInfo(FieldInfo[] injectableFields, PropertyInfo[] injectableProperties)
        {
            Fields = injectableFields;
            Properties = injectableProperties;
        }
    }
}