using UnityEngine;

namespace MergeTwoMob.DataModels.Merge
{
    public abstract class BaseItemModel : BaseModel, IItem
    {
        [SerializeField] private Sprite icon;
        public Sprite Icon => icon;
        public string Id => name;
    }
}