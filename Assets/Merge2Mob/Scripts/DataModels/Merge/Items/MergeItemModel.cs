using UnityEngine;

namespace MergeTwoMob.DataModels.Merge
{

    public interface IMergeItemModel
    {
        Sprite Icon { get; }
        string Id { get; }
    }
    
    public class MergeItemModel<T> : IMergeItemModel where T : BaseItemModel
    {
        protected readonly T data;

        public Sprite Icon => data.Icon;
        public string Id => data.name;
        
        public MergeItemModel(T data)
        {
            this.data = data;
        }
    }
}