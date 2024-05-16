using MergeTwoMob.GameScripts.MergeViews;
using UnityEngine;

namespace MergeTwoMob.Configs
{
    [CreateAssetMenu(menuName = "MergeTwoMob/Settings/New Merge View Config", fileName = "MergeViewConfig")]
    public class MergeViewConfig : ScriptableObject
    {
        [SerializeField] private BaseMergeItemView baseMergeItemView;
        [SerializeField] private TimeProducerItemView timeProducerItemView;
        
        public BaseMergeItemView BaseMergeItemView => baseMergeItemView;
        public TimeProducerItemView TimeProducerItemView => timeProducerItemView;
    }
}
