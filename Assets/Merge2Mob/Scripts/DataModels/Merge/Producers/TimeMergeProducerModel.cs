using UnityEngine;

namespace MergeTwoMob.DataModels.Merge
{
    public interface ITimeMergeProducerModel : IMergeItemModel
    {
        float Duration { get; }
    }

    public class TimeTimeMergeProducerModel<T> : MergeItemModel<T>, ITimeMergeProducerModel where T : TimeMergeProducer
    {

        public float Duration => data.Duration;
        
        public TimeTimeMergeProducerModel(T data) : base(data)
        {
        }
    }
}