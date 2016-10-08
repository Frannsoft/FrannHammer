using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace FrannHammer.Api.TelemetryProcessorFilters
{
    /// <summary>
    /// Filters out Application Insights data that I do not want to store right now.
    /// </summary>
    public class ExcludedDataFilter : ITelemetryProcessor
    {
        private readonly ITelemetryProcessor _next;

        public ExcludedDataFilter(ITelemetryProcessor next)
        {
            _next = next;
        }

        public void Process(ITelemetry item)
        {
            if (item is RequestTelemetry)
            {
                ModifyItem(item);
            }

            _next.Process(item);
        }

        private void ModifyItem(ITelemetry item)
        {
            //will not send location-based data up to app insights instance
            item.Context.Location.Ip = "0.0.0.0"; 
        }
    }
}