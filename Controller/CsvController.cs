using System;
using System.Collections.Generic;
using Bacchus.DAO;
using Bacchus.Model;
using Microsoft.VisualBasic.FileIO;


namespace Bacchus
{
    class CsvController
    {
        private String path;
        private int writeMode;
        private List<Article> data;

        public CsvController(String path)
        {
            this.path = path;
            this.writeMode = 0;
            this.data = new List<Article>();
        }

        public CsvController(String path, int writeMode)
        {
            this.path = path;
            this.writeMode = writeMode;
            this.data = new List<Article>();
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

                    Marque marque = new Marque();
                    marque.Nom = fields[2];

                    Famille famille = new Famille();
                    famille.Nom = fields[3];

                    SousFamille sousFamille = new SousFamille();
                    sousFamille.RefFamille = famille;
                    sousFamille.Nom = fields[4];

                    Article article = new Article(fields[1], fields[0], sousFamille, marque, Convert.ToDouble(fields[5]), 0);
                    Console.WriteLine(article);
                    data.Add(article);
                }
            }
        }

        public void Write()
        {
            foreach(Article article in data)
            {
                ArticleDAO.Insert(article);
            }
        }
    }
}
