using System;
using NUnit.Framework;

namespace FrannHammer.Services.Tests
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class LongRunning : CategoryAttribute
    {
        public LongRunning()
           : base("LONGRUNNING")
        {

        }
    }
}
