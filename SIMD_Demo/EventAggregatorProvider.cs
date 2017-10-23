using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;

namespace SIMD_Demo
{
    public static class EventAggregatorProvider
    {
        public static IEventAggregator EventAggregator { get; private set; }

        static EventAggregatorProvider()
        {
            EventAggregator = new EventAggregator();
        }
    }
}
