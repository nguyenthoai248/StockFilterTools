using System;
using System.Linq;
using System.Windows.Forms;

namespace FilterStockTools
{
    partial class Form2
    {
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    private System.Windows.Forms.Panel leftPanel;
    private System.Windows.Forms.TextBox txtSearch;
    private System.Windows.Forms.Button btnSearch;
    private System.Windows.Forms.DataGridView dataGridViewMain;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
        this.leftPanel = new System.Windows.Forms.Panel();
        this.txtSearch = new System.Windows.Forms.TextBox();
        this.btnSearch = new System.Windows.Forms.Button();
        this.dataGridViewMain = new System.Windows.Forms.DataGridView();

        this.tableLayoutPanel.SuspendLayout();
        this.leftPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).BeginInit();
        this.SuspendLayout();

        // TableLayoutPanel
        this.tableLayoutPanel.ColumnCount = 2;
        this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
        this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableLayoutPanel.Controls.Add(this.leftPanel, 0, 0);
        this.tableLayoutPanel.Controls.Add(this.dataGridViewMain, 1, 0);
        this.tableLayoutPanel.RowCount = 1;
        this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

        // leftPanel
        this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.leftPanel.Padding = new System.Windows.Forms.Padding(10);
        this.leftPanel.Controls.Add(this.btnSearch);
        this.leftPanel.Controls.Add(this.txtSearch);

        // txtSearch
        this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
        //this.txtSearch.PlaceholderText = "Nhập từ khoá tìm kiếm...";
        this.txtSearch.Name = "txtSearch";

        // btnSearch
        this.btnSearch.Dock = System.Windows.Forms.DockStyle.Top;
        this.btnSearch.Text = "Tìm kiếm";
        this.btnSearch.Height = 35;
        this.btnSearch.Margin = new Padding(0, 10, 0, 0);
        this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
        this.btnSearch.Name = "btnSearch";

        // dataGridViewMain
        this.dataGridViewMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dataGridViewMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
        this.dataGridViewMain.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.dataGridViewMain.AllowUserToAddRows = false;
        this.dataGridViewMain.Name = "dataGridViewMain";

        // Thêm 20 cột mẫu để test scroll ngang
        for (int i = 1; i <= 20; i++)
        {
            var column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            column.HeaderText = $"Cột {i}";
            column.Width = 150;
            column.Name = $"Col{i}";
            this.dataGridViewMain.Columns.Add(column);
        }

        // Thêm vài dòng mẫu
        for (int row = 1; row <= 10; row++)
        {
            this.dataGridViewMain.Rows.Add(Enumerable.Range(1, 20).Select(i => $"Dữ liệu {row}.{i}").ToArray());
        }

        // MainForm
        this.Controls.Add(this.tableLayoutPanel);
        this.Text = "Tìm kiếm dữ liệu";
        this.Load += new System.EventHandler(this.Form2_Load);

        this.tableLayoutPanel.ResumeLayout(false);
        this.leftPanel.ResumeLayout(false);
        this.leftPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).EndInit();
        this.ResumeLayout(false);
    }

        private void Form2_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
