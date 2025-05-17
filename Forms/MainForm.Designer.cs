using System.Drawing;
using System.Windows.Forms;

namespace FilterStockTools
{
    partial class MainForm
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
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.btnFetch = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSymbol
            // 
            this.txtSymbol.Location = new System.Drawing.Point(10, 19);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(120, 20);
            this.txtSymbol.TabIndex = 0;
            this.txtSymbol.Text = "MWG";
            // 
            // btnFetch
            // 
            this.btnFetch.Location = new System.Drawing.Point(10, 45);
            this.btnFetch.Name = "btnFetch";
            this.btnFetch.Size = new System.Drawing.Size(75, 23);
            this.btnFetch.TabIndex = 1;
            this.btnFetch.Text = "Tìm kiếm";
            this.btnFetch.Click += new System.EventHandler(this.btnFetch_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 20);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.Size = new System.Drawing.Size(1434, 654);
            this.dgvData.TabIndex = 0;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.panelLeft, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.panelRight, 1, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(1610, 700);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.txtSymbol);
            this.panelLeft.Controls.Add(this.btnFetch);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(3, 3);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.panelLeft.Size = new System.Drawing.Size(144, 694);
            this.panelLeft.TabIndex = 0;
            // 
            // panelRight
            // 
            this.panelRight.AutoScroll = true;
            this.panelRight.Controls.Add(this.dgvData);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(153, 3);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(0, 20, 20, 20);
            this.panelRight.Size = new System.Drawing.Size(1454, 694);
            this.panelRight.TabIndex = 1;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1610, 700);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "Form1";
            this.Text = "Báo cáo tài chính SSI Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private TableLayoutPanel tableLayoutPanel;
        private Panel panelLeft;
        private Panel panelRight;
    }
}


