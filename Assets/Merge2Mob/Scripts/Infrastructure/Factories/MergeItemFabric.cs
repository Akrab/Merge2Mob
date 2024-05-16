using MergeTwoMob.Configs;
using MergeTwoMob.DataModels.Merge;
using MergeTwoMob.DIMerge;
using MergeTwoMob.GameScripts.MergeViews;
using MergeTwoMob.Infrastructure.Services;
using UnityEngine;

namespace MergeTwoMob.Infrastructure.Factories
{
    public class MergeItemFabric
    {
        [Inject] private IMergeItemService mergeItemService;
        [Inject] private MergeViewConfig mergeViewConfig;
        [Inject] private DiContainer diContainer;
        
        private T CreateObj<T>(Component component) where T : Object
        {
            Transform newTransform = Object.Instantiate(component.transform);
            newTransform.position = Vector3.zero;
            newTransform.gameObject.SetActive(false);
            return newTransform.GetComponent<T>();
        }

        public TimeProducerItemView CreateProducer(string id, Vector3 position)
        {
            ITimeMergeProducerModel meta = mergeItemService.GetTimeProducer(id);
            TimeProducerItemView newObj = CreateObj<TimeProducerItemView>(mergeViewConfig.TimeProducerItemView);
            diContainer.Inject(newObj);
            newObj.SetData(meta);
            newObj.transform.position = position;

            return newObj;
        }

        public BaseMergeItemView CreateMergeItem(string id, Vector3 position)
        {
            IMergeItemModel meta = mergeItemService.Get(id);
            BaseMergeItemView newObj = CreateObj<BaseMergeItemView>(mergeViewConfig.BaseMergeItemView);
            diContainer.Inject(newObj);
            newObj.SetData(meta);
            newObj.transform.position = position;

            return newObj;
        }
    }
}