using MergeTwoMob.DIMerge;
using MergeTwoMob.GameScripts.MergeViews;
using MergeTwoMob.RuntimeScripts;
using UnityEngine;

namespace MergeTwoMob.Infrastructure.Controllers.Inputs
{
    public class MergeInputController
    {
        [Inject] private InputController inputController;
        [Inject] private RuntimeMergeMap runtimeMergeMap;
        [Inject] private MergeItemController mergeItemController;
        private Camera camera;
        private MapNode selectNode;


        private Vector3 GetWorldPosition(Vector2 screenPosition)
        {
            Vector3 screenPos = new Vector3(screenPosition.x, screenPosition.y, camera.nearClipPlane);
            return  camera.ScreenToWorldPoint(screenPos);
        }
        
        private void InputControllerOnMoveTouchEvent(Vector2 position)
        {
            
            if(selectNode != null)
                selectNode.SetPosition(GetWorldPosition(position));
        }

        private void ResetSlot()
        {
            selectNode.ResetPosition();
            selectNode = null;
        }
        private void InputControllerOnEndTouchEvent(Vector2 position)
        {
            if (selectNode == null)
            {
                return;
            }

            Vector3 worldPos = GetWorldPosition(position);
            MapNode nextNode = runtimeMergeMap.GetMatchItem(worldPos);

            if (nextNode == selectNode)
            {
                ResetSlot();
                return;
            }

            if (nextNode != null)
            {
                mergeItemController.CheckMerge(selectNode,nextNode );
                selectNode = null;
                return;
            }

            nextNode = runtimeMergeMap.GetEmptyMatchItem(worldPos);
            if (nextNode != null)
            {
                nextNode.ItemView = selectNode.ItemView;
                nextNode.ResetPosition();
                selectNode.ItemView = null;
                selectNode = null;
                return;
            }

            ResetSlot();
        }

        private void InputControllerOnStartTouchEvent(Vector2 position)
        {
            selectNode = runtimeMergeMap.GetMatchItem(GetWorldPosition(position));
        }

        public void SetCamera(Camera cam)
        {
            camera = cam;
        }
        
        public void StartGame()
        {
            inputController.OnStartTouchEvent += InputControllerOnStartTouchEvent;
            inputController.OnEndTouchEvent += InputControllerOnEndTouchEvent;
            inputController.OnMoveTouchEvent += InputControllerOnMoveTouchEvent;
        }


        
    }
}