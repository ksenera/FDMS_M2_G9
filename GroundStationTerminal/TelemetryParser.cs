using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundStationTerminal
{
    public class TelemetryParser
    {
        private string dataFormat;

        public TelemetryParser(string format)
        {
            this.dataFormat = format;
        }

        public ParsedData ParseData(string data)
        {

            // if the data is not in the correct format, throw ArgumentException

            // parse based on specific format array of strings and split

            return new ParsedData();
            // should follow the format as per SharedLibrary.ParsedData
        }

        public bool ValidateChecksum((DateTime Timestamp, double AccelX, double AccelY, double AccelZ,
            double Weight, double Altitude, double Pitch, double Bank) body, int providedChecksum)
        {
            double checksumValue = (body.Altitude + body.Pitch + body.Bank) / 3;

            int calculatedChecksum = (int)Math.Round(checksumValue);

            return providedChecksum == calculatedChecksum;
        }
    }
}
