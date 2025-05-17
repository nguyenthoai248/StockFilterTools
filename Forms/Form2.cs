using System;
using System.Windows.Forms;

namespace FilterStockTools
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Đang tìm kiếm: {txtSearch.Text}");
        }
    }
}
