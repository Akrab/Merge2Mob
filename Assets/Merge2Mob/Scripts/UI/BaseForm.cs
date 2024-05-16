using System;
using Doozy.Runtime.UIManager.Animators;
using Doozy.Runtime.UIManager.Containers;
using UnityEngine;

namespace MergeTwoMob.UI
{
    public interface IForm
    {
        bool IsShow { get; }
        GameObject gameObject { get; }
        void Show(bool instant = false);
        void Hide(bool instant = false);
    }

    public abstract class BaseForm : MonoBehaviour, IForm
    {
        [SerializeField] private UIView uiView;
        [SerializeField] private UIContainerUIAnimator containerUIAnimator;


        public bool IsShow { get; }


#if UNITY_EDITOR
        private void Reset()
        {
            containerUIAnimator = GetComponent<UIContainerUIAnimator>();
            uiView = GetComponent<UIView>();
        }
#endif

        public virtual void Show(bool instant = false)
        {
            if (instant)
            {
                uiView.InstantShow();
                return;
            }

            containerUIAnimator.Show();
        }

        public virtual void Hide(bool instant = false)
        {
            if (instant)
            {
                uiView.InstantHide();
                return;
            }

            uiView.Hide();

        }
    }
}