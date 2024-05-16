using System;
using MergeTwoMob.Configs;
using MergeTwoMob.DataModels.Merge;
using MergeTwoMob.DIMerge;
using MergeTwoMob.GameScripts.MergeViews;
using MergeTwoMob.Infrastructure.Factories;
using MergeTwoMob.Infrastructure.Services;
using MergeTwoMob.RuntimeScripts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MergeTwoMob.Infrastructure.Controllers
{
    public class MergeItemController
    {
        [Inject] private SpawnNewItemController spawnNewItemController;
        [Inject] private RuntimeMergeMap runtimeMergeMap;
        [Inject] private MergeItemFabric mergeItemFabric;
        [Inject] private MergeViewConfig mergeViewConfig;
        [Inject] private IMergeChainService mergeChainService;
        
        private BaseMergeItemView CreateNewItem(string id, Vector3 position)
        {
            BaseMergeItemView newItem = mergeItemFabric.CreateMergeItem(id, position);
            newItem.gameObject.SetActive(true);

            return newItem;
        }

        public void CheckMerge(MapNode nodeMove, MapNode nodeDown)
        {
            if (nodeMove.ModelId != nodeDown.ModelId)
            {
                nodeMove.ResetPosition();
                return;
            }

            IMergeChainModel mergeChainModel = mergeChainService.GetChainFor(nodeMove.ModelId);
            if (mergeChainModel == null)
            {
                nodeMove.ResetPosition();
                return;
            }

            int nextIndex = Array.FindIndex(mergeChainModel.Items, I => I == nodeMove.ModelId) + 1;
            if (nextIndex >= mergeChainModel.Items.Length)
            {
                nodeMove.ResetPosition();
                return;
            }

            string nextItemId = mergeChainModel.Items[nextIndex];

            Object.Destroy(nodeMove.ItemView.gameObject);
            Object.Destroy(nodeDown.ItemView.gameObject);
            nodeDown.ItemView = nodeMove.ItemView = null;

            nodeDown.ItemView = CreateNewItem(nextItemId, Vector3.zero);
            nodeDown.ResetPosition();
            spawnNewItemController.MergeCompleted();
        }
    }
}