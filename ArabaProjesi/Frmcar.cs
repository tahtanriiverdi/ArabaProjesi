using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ArabaProjesi.Form1;

namespace ArabaProjesi
{
    public partial class Frmcar : Form
    {
        public Frmcar()
        {
            InitializeComponent();
        }
        public Araba araba = null;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            propertyGrid1.SelectedObject= araba;
        }
        private void okkomut(object sender, EventArgs e)
        {
            DialogResult= DialogResult.OK;
        }

        private void iptaltik(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
