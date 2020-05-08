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
        public FormImporter()
        {
            InitializeComponent();
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

                    using (StreamReader sr = File.OpenText(path));
                    {
                        // Parse le chemin d'accès avec le caractère '\'
                        string[] parse = path.Split('\\');

                        // Récupère lenom du fichier
                        label2.Text = parse[parse.Length-1];
                    }
                }
            }
        }
    }
}
