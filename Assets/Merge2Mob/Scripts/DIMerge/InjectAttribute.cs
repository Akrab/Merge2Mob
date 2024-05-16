using System;
using UnityEngine.Scripting;

namespace MergeTwoMob.DIMerge
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class InjectAttribute : PreserveAttribute
    {

    }
}