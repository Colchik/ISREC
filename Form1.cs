using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISREC
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }



        private void gunaButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2ButtonCollapse_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
