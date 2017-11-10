using Prism.Events;

namespace SIMD_Demo.Providers
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
