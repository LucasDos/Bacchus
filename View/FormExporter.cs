using Bacchus.DAO;
using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Bacchus
{
    public partial class FormExporter : Form
    {
        public FormExporter()
        {
            InitializeComponent();
        }

        private void FormExporter_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// detecte le clique sur le bouton Exporter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                // Ouvre un boite de dialogue pour choisir le dossier où enregistrer les données
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    // Récupère le chemin du dossier
                    var folderPath = fbd.SelectedPath + "\\BacchusData.csv";

                    using (StreamWriter writer = new StreamWriter(new FileStream(folderPath, FileMode.OpenOrCreate)))
                    {
                        writer.WriteLine("Description;Ref;Marque;Famille;Sous-Famille;Prix H.T.");

                        SQLiteDataReader data = Database.GetSql("select Description, RefArticle, Marques.Nom, Familles.Nom, SousFamilles.Nom, PrixHT from Articles join Marques using(RefMarque) join SousFamilles using(RefSousFamille) join Familles using(RefFamille);");

                        while (data.Read())
                        {
                            String line = data.GetString(0) + ";" + data.GetString(1) + ";" + data.GetString(2) + ";" + data.GetString(3) + ";" + data.GetString(4) + ";" + data.GetFloat(5);
                            writer.WriteLine(line);
                        }

                        // Affiche le chemin du fichier selectionne
                        folder_lbl.Text = folderPath;
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
