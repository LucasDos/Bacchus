﻿using Bacchus.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bacchus
{
    public partial class FormImporter : Form
    {
        private CsvController csvController;

        public FormImporter()
        {
            InitializeComponent();
            csvController = null;
        }

        /// <summary>
        /// Bouton pour ouvrir un fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofd.Filter = "csv files (*.csv)|*.csv";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;

                // Ouvre une boîte de dialogue sur le Bureau
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Récupère le chemin d'accès au fichier
                    string path = ofd.FileName;
                    csvController = new CsvController(path, progressBar1);
                    csvController.Read();

                    using (StreamReader sr = File.OpenText(path))
                    {
                        // Parse le chemin d'accès avec le caractère '\'
                        string[] parse = path.Split('\\');

                        // Récupère lenom du fichier
                        label2.Text = parse[parse.Length-1];
                    }
                }
            }
        }

        /// <summary>
        /// Detecte le clique sur le bouton ajouter les donnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ajout_Click(object sender, EventArgs e)
        {
            if(csvController != null)
            {
                csvController.SaveInDb();
            }
        }

        /// <summary>
        /// Detecte la clique sur le bouton  ecraser les données
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ecrase_Click(object sender, EventArgs e)
        {
            if (csvController != null)
            {
                Database.Empty();
                csvController.SaveInDb();
            }
        }
    }
}
