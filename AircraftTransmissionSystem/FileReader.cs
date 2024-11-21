using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftTransmissionSystem
{
    internal class FileReader(string filePath)
    {
        private string FilePath = filePath;// get a dialogue window so user can select the file with the given extensions allowed

        public string ReadData()
        {
            // read data before seding off to the parser (below)
            throw new NotImplementedException();
        }

        // maybe keep private since it's only used in this class, not needed outside
        private void ParseData(DataAdapter adapter)
        {
            throw new NotImplementedException();
        }

        private class DataAdapter(string data)
        {
            private readonly string Data = data;

            public void ConvertData()
            {
                throw new NotImplementedException();
            }
        }
    }
}
