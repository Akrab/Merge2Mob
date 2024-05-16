using System.Collections.Generic;
using MergeTwoMob.DataModels.Merge;
using MergeTwoMob.Merge2Mob.Scripts.Base;
using UnityEngine;

namespace MergeTwoMob.Infrastructure.Services
{
    public interface IMergeItemService
    {
        IReadOnlyList<IMergeItemModel> Items { get; }
        IReadOnlyList<ITimeMergeProducerModel> TimeProducers { get; }
        
        IMergeItemModel Get(string id);
        ITimeMergeProducerModel GetTimeProducer(string id);
    }

    public class MergeItemService : BaseLoadService, IMergeItemService
    {
        private List<IMergeItemModel> items;
        public IReadOnlyList<IMergeItemModel> Items => items;

        
        
        private List<ITimeMergeProducerModel> timeProducer;
        public IReadOnlyList<ITimeMergeProducerModel> TimeProducers => timeProducer;

        private void LoadItems()
        {
            MergeItem[] models = Resources.LoadAll<MergeItem>(CONSTANTS.PATH_MERGE_ITEMS);
            items = new(models.Length);
            foreach (var item in models)
            {
                items.Add(new MergeItemModel<MergeItem>(item));
            }
        }

        private void LoadProducers()
        {
            TimeMergeProducer[] models = Resources.LoadAll<TimeMergeProducer>(CONSTANTS.PATH_MERGE_ITEMS);
            timeProducer = new(models.Length);
            foreach (var item in models)
            {
                timeProducer.Add(new TimeTimeMergeProducerModel<TimeMergeProducer>(item));
            }
        }
        
        public override void Load()
        {
            LoadItems();
            LoadProducers();
        }

        public IMergeItemModel Get(string id)
        {
            return items.Find(D => D.Id == id);
        }

        public ITimeMergeProducerModel GetTimeProducer(string id)
        {
            return timeProducer.Find(D => D.Id == id);
        }
        
    }
}