using MergeTwoMob.GameScripts.Currencies;
using UnityEngine;

namespace MergeTwoMob.DataModels.Merge
{
    [CreateAssetMenu(menuName = "MergeTwoMob/Items/New item", fileName = "MergeItem")]
    public class MergeItem : BaseItemModel
    {
        [SerializeField] private Currency sell;
    }
}