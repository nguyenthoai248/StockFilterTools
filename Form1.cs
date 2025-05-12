using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace FilterStockTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void StyleDataGridView(DataGridView dgv)
        {
            // Make columns auto-resize
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Font settings
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Background colors
            dgv.BackgroundColor = Color.White;
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

            // Borders
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgv.GridColor = Color.LightGray;

            // Row settings
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            // Optional: highlight selected row
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private async void btnFetch_Click(object sender, EventArgs e)
        {
            string symbol = txtSymbol.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(symbol))
            {
                MessageBox.Show("Please enter a stock symbol.");
                return;
            }

            string url = $"https://fiin-fundamental.ssi.com.vn/FinancialStatement/DownloadIncomeStatement?language=vi&OrganCode={symbol}&Skip=0&Frequency=quarterly&numberOfPeriod=12&latestYear=2025";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                client.DefaultRequestHeaders.Add("origin", "https://iboard.ssi.com.vn");
                client.DefaultRequestHeaders.Add("referer", "https://iboard.ssi.com.vn/");
                client.DefaultRequestHeaders.Add("x-fiin-user-token", "YOUR_TOKEN_HERE"); // Replace with your token

                try
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var table = worksheet.RangeUsed().AsTable();

                        var neededRows = new[] {
                            "Doanh số thuần",
                            "Lãi gộp",
                            "Lãi/(lỗ) thuần sau thuế",
                            "Lãi cơ bản trên cổ phiếu"
                        };

                        DataTable dt = new DataTable();
                        foreach (var cell in table.Row(1).Cells())
                        {
                            dt.Columns.Add(cell.GetString());
                        }

                        foreach (var row in table.DataRange.Rows())
                        {
                            if (neededRows.Contains(row.Cell(1).GetString()))
                            {
                                var dataRow = dt.NewRow();
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    dataRow[i] = row.Cell(i + 1).GetString();
                                }
                                dt.Rows.Add(dataRow);
                            }
                        }

                        dgvData.DataSource = dt;
                        StyleDataGridView(dgvData);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}

