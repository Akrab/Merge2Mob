using UnityEngine;

namespace MergeTwoMob.DataModels.Merge
{
    [CreateAssetMenu(menuName = "MergeTwoMob/Settings/New Merge Chain", fileName = "MergeChain")]
    public class MergeChain : BaseModel
    {
        [SerializeField] private BaseItemModel[] mergeItems;

        public BaseItemModel[] Items => mergeItems;
    }
}