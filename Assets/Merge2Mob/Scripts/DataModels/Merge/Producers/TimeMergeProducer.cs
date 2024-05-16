using UnityEngine;

namespace MergeTwoMob.DataModels.Merge
{
    [CreateAssetMenu(menuName = "MergeTwoMob/Items/New Time Producer", fileName = "TimeProducerItem")]
    public class TimeMergeProducer : MergeProducer
    {
        [SerializeField]
        private float duration;

        public float Duration => duration;
    }
}