using Bacchus.DAO;
using Bacchus.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bacchus
{
    partial class FormModifFamilles : Form
    {
        public FormModifFamilles()
        {
            InitializeComponent();
        }

        public void InitializeDataComponent(Famille famille)
        {
            referece_lbl.Text = Convert.ToString(famille.Reference);
            name_input.Text = famille.Nom;
        }

        private void modify_btn_Click(object sender, EventArgs e)
        {
            Famille famille = new Famille(Convert.ToInt32(referece_lbl.Text), name_input.Text);
            FamilleDAO.updateFamille(famille);
        }
    }
}
