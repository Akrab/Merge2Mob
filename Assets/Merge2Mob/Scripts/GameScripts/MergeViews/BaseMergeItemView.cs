using MergeTwoMob.DataModels.Merge;
using UnityEngine;

namespace MergeTwoMob.GameScripts.MergeViews
{
    public class BaseMergeItemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private IMergeItemModel mergeItemModel;
        public string ModelId => mergeItemModel.Id;
        
        public void SetData(IMergeItemModel meta)
        {
            mergeItemModel = meta;
            spriteRenderer.sprite = meta.Icon;
            
        }
    }
}