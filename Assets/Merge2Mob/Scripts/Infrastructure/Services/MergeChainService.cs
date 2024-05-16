using System;
using System.Collections.Generic;
using MergeTwoMob.DataModels.Merge;
using MergeTwoMob.Merge2Mob.Scripts.Base;
using UnityEngine;

namespace MergeTwoMob.Infrastructure.Services
{

    public interface IMergeChainService
    {
        IMergeChainModel GetChainFor(string nodeMoveModelId);
        IReadOnlyList<IMergeChainModel> Shains { get; }
    }

    public class MergeChainService : BaseLoadService, IMergeChainService
    {
        private List<IMergeChainModel> chains;
        public IReadOnlyList<IMergeChainModel> Shains => chains;

        public override void Load()

        {
            MergeChain[] mergeChains = Resources.LoadAll<MergeChain>(CONSTANTS.PATH_MERGE_CHAINS);
            chains = new(mergeChains.Length);
            foreach (var item in mergeChains)
            {
                chains.Add(new MergeChainModel<MergeChain>(item));
            }
        }

        public IMergeChainModel GetChainFor(string modelId)
        {
            foreach (IMergeChainModel chain in chains)
            {

                if (Array.FindIndex(chain.Items, A => A == modelId) != -1)
                {
                    return chain;
                } 
            }

            return null;
        }
    }
}
