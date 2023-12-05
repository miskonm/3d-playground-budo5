using System;
using UnityEngine.Scripting;

namespace Playground.DI
{
    [AttributeUsage(AttributeTargets.Method)]
    public class InjectAttribute : PreserveAttribute { }
}