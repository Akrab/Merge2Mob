using System;
using System.Collections.Generic;
using MergeTwoMob.UI;

namespace MergeTwoMob.Infrastructure.Containers
{
    public class UIContainer
    {
        private Dictionary<Type, IForm> forms = new Dictionary<Type, IForm>();

        public void AddForm(IForm form)
        {
            if (forms.ContainsKey(form.GetType()) == false)
            {
                forms.Add(form.GetType(), form);
            }
        }
        
        public T GetForm<T>() where T : IForm
        {
            forms.TryGetValue(typeof(T), out var form);
            return (T)form;
        }

        public bool RemoveForm<T>()
        {
            return forms.Remove(typeof(T));
        }

    }
}