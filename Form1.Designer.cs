using System.Windows.Forms;
using ClosedXML.Excel;

namespace FilterStockTools
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtSymbol;
        private Button btnFetch;
        private DataGridView dgvData;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtSymbol = new TextBox();
            this.btnFetch = new Button();
            this.dgvData = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();

            // txtSymbol
            this.txtSymbol.Location = new System.Drawing.Point(20, 20);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(120, 22);
            this.txtSymbol.Text = "MWG";

            // btnFetch
            this.btnFetch.Location = new System.Drawing.Point(160, 18);
            this.btnFetch.Name = "btnFetch";
            this.btnFetch.Size = new System.Drawing.Size(100, 25);
            this.btnFetch.Text = "Tìm kiếm";
            this.btnFetch.Click += new System.EventHandler(this.btnFetch_Click);

            // dgvData
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvData.Location = new System.Drawing.Point(20, 60);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.Size = new System.Drawing.Size(1000, 600);

            // Form1
            this.ClientSize = new System.Drawing.Size(1040, 700);
            this.Controls.Add(this.txtSymbol);
            this.Controls.Add(this.btnFetch);
            this.Controls.Add(this.dgvData);
            this.Name = "Form1";
            this.Text = "Báo cáo tài chính SSI Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}


