using System;
using System.Collections.Generic;
using System.Linq;
using MergeTwoMob.GameScripts.MergeViews;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace MergeTwoMob.RuntimeScripts
{
    public class MapNode
    {
        public BaseMergeItemView ItemView;
        public bool IsFree => ItemView == null;
        public Vector2Int Position;
        public int Index;
        public string ModelId=> IsFree? String.Empty :  ItemView.ModelId;

        public void SetPosition(Vector3 worldPosition)
        {
            worldPosition.z = ItemView.transform.position.z;
            ItemView.transform.position = worldPosition;
        }

        public void ResetPosition()
        {
            ItemView.transform.position = new Vector3(Position.x, Position.y, ItemView.transform.position.z);
        }
    }

    public class RuntimeMergeMap
    {
        private List<MapNode> nodes;

        private int x, y;
        public bool HaveFreeSlot => nodes.FindAll(D => D.IsFree).Count > 0;

        public RuntimeMergeMap(int x, int y, Vector2Int min)
        {
            this.x = x;
            this.y = y;
            nodes = new List<MapNode>(x * y);

            var position = new Vector2Int(min.x, min.y);

            for (int i = 0; i < y; i++)
            {

                for (int j = 0; j < x; j++)
                {
                    nodes.Add(new MapNode() { Position = position, Index = i * x + j });
                    position.x++;
                }

                position.x = min.x;
                position.y++;
            }


        }

        public void Remove(int x, int y)
        {

        }


        public MapNode GetFreeSlot()
        {
            return nodes.First(D => D.IsFree);
        }

        public MapNode GetMatchItem(Vector3 worldPosition)
        {
            foreach (MapNode node in nodes)
            {
                if (node.IsFree)
                {
                    continue;
                }
                if (worldPosition.x > node.Position.x - 0.5f
                    && worldPosition.y > node.Position.y - 0.5f
                    && worldPosition.x < node.Position.x + 0.5f
                    && worldPosition.y < node.Position.y + 0.5f)
                {
                    return node;
                }
            }

            return null;
        }

        public MapNode GetEmptyMatchItem(Vector3 worldPosition)
        {
            foreach (MapNode node in nodes)
            {
                if (node.IsFree == false)
                {
                    continue;
                }
                if (worldPosition.x > node.Position.x - 0.5f
                    && worldPosition.y > node.Position.y - 0.5f
                    && worldPosition.x < node.Position.x + 0.5f
                    && worldPosition.y < node.Position.y + 0.5f)
                {
                    return node;
                }
            }

            return null;
        }
    }

}