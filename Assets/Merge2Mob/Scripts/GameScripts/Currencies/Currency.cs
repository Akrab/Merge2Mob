using System;
using UnityEngine;

namespace MergeTwoMob.GameScripts.Currencies
{

    public enum CurrencyMode
    {
        None= -1,
        Silver = 0,
        Gold = 1
    }
    
    [Serializable]
    public class Currency
    {
        [SerializeField]
        private CurrencyMode currencyMode;

        [SerializeField]
        private int ammount;

        public CurrencyMode CurrencyMode => currencyMode;
        public int Ammount => ammount;
    }
}