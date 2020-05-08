using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic.FileIO;


namespace Bacchus
{
    class CsvController
    {
        private String path;
        private int writeMode;

        public CsvController(String path)
        {
            this.path = path;
            this.writeMode = 0;
        }

        public CsvController(String path, int writeMode)
        {
            this.path = path;
            this.writeMode = writeMode;
        }

        public void SetWriteMode(int writeMode)
        {
            this.writeMode = writeMode;
        }

        public int Read()
        {
            // https://stackoverflow.com/questions/5282999/reading-csv-file-and-storing-values-into-an-array
        }
    }
}
