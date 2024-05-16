using System.Linq;

namespace MergeTwoMob.DataModels.Merge
{
    
    public interface IMergeChainModel
    {
        string[] Items { get; }
        string Id { get; }
    }
    
    public class MergeChainModel<T> :IMergeChainModel where T : MergeChain
    {
        private readonly T data;
        
        public string Id => data.name;
        public string[] Items => data.Items.Select(D => D.name).ToArray();
        
        public MergeChainModel(T data)
        {
            this.data = data;
        }

    }
}
