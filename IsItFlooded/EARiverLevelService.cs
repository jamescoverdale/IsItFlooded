using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IsItFlooded.Data;
using IsItFlooded.Models;
using Newtonsoft.Json;

namespace IsItFlooded
{
    public class EARiverLevelService : IRiverLevelService
    {
        public RiverLevelMeasurement GetRiverLevel(string stationId)
        {
            try
            {
                Trace.TraceInformation("EARiverLevelScraper:GetRiverLevel() - {0}", stationId);
                
                using (WebClient client = new WebClient())
                {
                    string response = client.DownloadString(
                        $"http://environment.data.gov.uk/flood-monitoring/id/stations/{stationId}/readings?_sorted&_limit=2");

                    if (string.IsNullOrWhiteSpace(response) == false)
                    {
                        var json = JsonConvert.DeserializeObject<dynamic>(response);

                        RiverLevelMeasurement level = new RiverLevelMeasurement()
                        {
                            Measurement = Convert.ToDouble(json["items"][0]["value"].ToString()),
                            LastMeasurement = Convert.ToDouble(json["items"][1]["value"].ToString()),
                            MeasurementDateTime = DateTime.Parse(json["items"][0]["dateTime"].ToString())
                        };

                        // grab extra station details
                        string stationDetails = client.DownloadString(
                            $"http://environment.data.gov.uk/flood-monitoring/id/stations/{stationId}.json");

                        if (string.IsNullOrWhiteSpace(stationDetails) == false)
                        {
                            var stationJson = JsonConvert.DeserializeObject<dynamic>(stationDetails);
                            level.StationName = stationJson["items"]["label"].ToString();
                            level.StationRLOIid = stationJson["items"]["RLOIid"].ToString();
                        }

                        return level;
                    }
                    else
                    {
                        Trace.TraceError("EAApiRiverLevelScraper:GetRiverLevel() - response was null");
                        return null;
                    }
                }
            }
            catch (Exception Ex)
            {
                Trace.TraceError("EAApiRiverLevelScraper:GetRiverLevel() - Error. Ex {0}. {1}", Ex.Message, Ex.StackTrace);

                return null;
            }
        }
    }
}
