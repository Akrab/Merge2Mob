using MergeTwoMob.DataModels.Merge;
using UnityEngine;
using UnityEngine.UI;


namespace MergeTwoMob.UI
{
    public class MergeItemSlotView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        
        public void SetData(IMergeItemModel model)
        {
            icon.sprite = model.Icon;
        }
    }
}