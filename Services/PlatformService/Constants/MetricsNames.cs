using System;

namespace PlatformService.Constants
{
    public class Metrices
    {
        public static MetricsInfo PlatformApiRequestCounter = new MetricsInfo("platformapi_request_counter","Custom Metrics to count requests for each endpoint and the method", new[] { "method", "endpoint" }) ;
    }

    public class MetricsInfo
    {

        public MetricsInfo( string name,string description, string[] labels)
        {
            Name= name;
            Description = description;
            Labels = labels;
        }
        public string Name { get; set; }
         public string Description { get; set; }

         public string[] Labels {get;set;}
    }
}
