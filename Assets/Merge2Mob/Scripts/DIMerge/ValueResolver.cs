namespace MergeTwoMob.DIMerge
{
    public class ValueResolver : IResolver
    {
        private readonly object value;
        public ValueResolver(object data)
        {
            value = data;
        } 
        public object Resolve()
        {
            return value;
        }
    }
}