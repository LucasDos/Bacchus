using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SQLite;

namespace Bacchus
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void importerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormImporter formImpor = new FormImporter();
            formImpor.Show();
        }

        private void exporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormExporter formExport = new FormExporter();
            formExport.Show();
        }
    }
}
