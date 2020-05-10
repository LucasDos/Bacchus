using Bacchus.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bacchus
{
    public partial class FormExporter : Form
    {
        public FormExporter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var filepath = "export.csv";
            using (StreamWriter writer = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write)))
            {
                writer.WriteLine("Description;Ref;Marque;Famille;Sous-Famille;Prix H.T.");

                SQLiteDataReader data = Database.GetSql("select Description, RefArticle, Marques.Nom, Familles.Nom, SousFamilles.Nom, PrixHT from Articles join Marques using(RefMarque) join SousFamilles using(RefSousFamille) join Familles using(RefFamille);");

                while(data.Read())
                {
                    String line = data.GetString(0) + ";" + data.GetString(1) + ";" + data.GetString(2) + ";" + data.GetString(3) + ";" + data.GetString(4) + ";" + data.GetFloat(5);
                    writer.WriteLine(line);
                }
            }
        }
    }
}
