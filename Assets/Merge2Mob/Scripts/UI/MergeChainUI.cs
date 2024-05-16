using MergeTwoMob.DIMerge;
using MergeTwoMob.Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace MergeTwoMob.UI
{
    public class MergeChainUI : BaseForm
    {
        public delegate void OnClose();
        [SerializeField] private RectTransform gridRoot;
        [SerializeField] private Button buttonClose;
        [SerializeField] private MergeItemSlotView prefab;
        [Inject] private IMergeChainService mergeChainService;
        [Inject] private IMergeItemService mergeItemService;

        public event OnClose OnCloseEvent;

        private void Awake()
        {
            buttonClose.onClick.AddListener(Close);
        }

        private void Close()
        {
            OnCloseEvent?.Invoke();
            Hide();
        }

        private void BuildUI()
        {
            string[] items = mergeChainService.Shains[0].Items;
            
            foreach (string itemId in items)
            {
                Instantiate(prefab.transform, gridRoot).GetComponent<MergeItemSlotView>().SetData(mergeItemService.Get(itemId));
            }
            
        }

        public override void Show(bool instant = false)
        {
            BuildUI();
            base.Show(instant);
        }
    }
}
