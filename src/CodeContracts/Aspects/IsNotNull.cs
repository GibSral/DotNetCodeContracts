using System;
using System.Collections.Generic;
using System.Text;

namespace CodeContracts.Aspects
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class IsNotNull : Attribute
    {
    }
}
