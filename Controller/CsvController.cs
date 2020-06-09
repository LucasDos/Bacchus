using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Bacchus.DAO;
using Bacchus.Model;
using Microsoft.VisualBasic.FileIO;


namespace Bacchus
{
    class CsvController
    {
        private String path;
        private List<Article> data;
        private ProgressBar progressBar = null;

        public CsvController(String path)
        {
            this.path = path;
            this.data = new List<Article>();
        }

        public CsvController(String path, ProgressBar progressBar)
        {
            this.path = path;
            this.data = new List<Article>();
            this.progressBar = progressBar;
            progressBar.Minimum = 0;
            progressBar.Step = 1;
        }

        /// <summary>
        /// Lit le fichier CSV
        /// </summary>
        public void Read()
        {
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { ";" });
                csvParser.HasFieldsEnclosedInQuotes = false;

                // Skip the row with the column names
                csvParser.ReadLine();
                int count = 0;

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

                    Article article = new Article(fields[1], fields[0], sousFamille, marque, Single.Parse(fields[5]), 0);
                    
                    data.Add(article);

                    count++;
                }

                progressBar.Maximum = count;
            }
        }

        /// <summary>
        /// Sauvegarde les donnnées dans la BDD
        /// </summary>
        public void SaveInDb()
        {
            foreach(Article article in data)
            {
                ArticleDAO.Insert(article);
                if(progressBar != null)
                {
                    progressBar.PerformStep();
                }
            }
        }
    }
}
