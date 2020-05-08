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
        private List<String[]> data;

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

        public void Read()
        {
            // https://stackoverflow.com/questions/5282999/reading-csv-file-and-storing-values-into-an-array

            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { ";" });
                csvParser.HasFieldsEnclosedInQuotes = false;

                // Skip the row with the column names
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                }
            }
        }
    }
}
