using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;

namespace FilterStockTools
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void StyleDataGridView(DataGridView dgv)
        {
            // Make columns auto-resize
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // 2. Tự động tăng chiều cao hàng theo nội dung (theo chiều dọc)
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // 3. Tự động tăng chiều rộng cột theo nội dung
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // 4. Tăng chiều cao tiêu đề cột nếu cần
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Font settings
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 16);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 16, FontStyle.Bold);

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
                MessageBox.Show("Nhập mã cổ phiếu để xem dữ liệu.");
                return;
            }

            await LoadDataAsync(symbol);
            //var sync = new Services.SyncService();
            //await sync.SyncAllData();
        }

        private async Task LoadDataAsync(string symbol)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                client.DefaultRequestHeaders.Add("origin", "https://iboard.ssi.com.vn");
                client.DefaultRequestHeaders.Add("referer", "https://iboard.ssi.com.vn/");
                client.DefaultRequestHeaders.Add("x-fiin-user-token", "YOUR_TOKEN_HERE"); // Replace with your token
                // Gọi 2 API song song
                string incomeStatementUrl = $"https://fiin-fundamental.ssi.com.vn/FinancialStatement/GetIncomeStatement?language=vi&OrganCode={symbol}&Skip=0&Frequency=quarterly&numberOfPeriod=12&latestYear=2025";
                string balanceSheetUrl = $"https://fiin-fundamental.ssi.com.vn/FinancialStatement/GetBalanceSheet?language=vi&OrganCode={symbol}&Skip=0&Frequency=quarterly&numberOfPeriod=12&latestYear=2025";

                try
                {
                    var incomeStatementApi = client.GetStringAsync(incomeStatementUrl);
                    var balanceSheetApi = client.GetStringAsync(balanceSheetUrl);

                    // Chờ cả hai hoàn tất
                    await Task.WhenAll(incomeStatementApi, balanceSheetApi);

                    // Lấy kết quả
                    string incomeStatementResponse = await incomeStatementApi;
                    string balanceSheetResponse = await balanceSheetApi;

                    // Parse JSON
                    var incomeStatementObj = JsonConvert.DeserializeObject<Models.IncomeStatementModel>(incomeStatementResponse);
                    var balanceSheetObj = JsonConvert.DeserializeObject<Models.BalanceSheetModel>(balanceSheetResponse);

                    // Xử lý dữ liệu
                    System.Data.DataTable dt = new System.Data.DataTable();
                    for (int i = 0; i < 13; i++)
                    {
                        if (i == 0)
                        {
                            dt.Columns.Add("Tiêu chí CSDL");
                        }
                        else
                        {
                            var quarterData = incomeStatementObj.items[0].quarterly[i - 1];
                            dt.Columns.Add("Q" + quarterData.quarterReport + " " + quarterData.yearReport);
                        }
                    }

                    dgvData.DataSource = dt;

                    var neededRows = new[] {
                                    "Doanh số thuần",
                                    "Lãi gộp",
                                    "Lãi/(lỗ) thuần sau thuế",
                                    "Lãi cơ bản trên cổ phiếu(EPS)",
                                    "Hàng tồn kho, ròng",
                                    "Các khoản phải thu",
                                    "Tài sản dở dang dài hạn"
                                };
                    for (int i = 0; i < neededRows.Length; i++) 
                    {
                        var dataRow = dt.NewRow();
                        for (int j = 0; j < 13; j++)
                        {
                            if (j == 0) {
                                dataRow[j] = neededRows[i];
                            } else
                            {
                                var quarterIncomeStatementData = incomeStatementObj.items[0].quarterly[j-1];
                                var quarterBalanceSheetData = balanceSheetObj.items[0].quarterly[j - 1];
                                switch (i)
                                {
                                    case 0:
                                        dataRow[j] = quarterIncomeStatementData.isa3.ToString("#,##0");
                                        break;
                                    case 1:
                                        dataRow[j] = quarterIncomeStatementData.isa5.ToString("#,##0");
                                        break;
                                    case 2:
                                        dataRow[j] = quarterIncomeStatementData.isa20.ToString("#,##0");
                                        break;
                                    case 3:
                                        dataRow[j] = quarterIncomeStatementData.isa23.ToString("#,##0");
                                        break;
                                    case 4:
                                        dataRow[j] = quarterBalanceSheetData.bsa15.ToString("#,##0");
                                        break;
                                    case 5:
                                        dataRow[j] = quarterBalanceSheetData.bsa8.ToString("#,##0");
                                        break;
                                    case 6:
                                        dataRow[j] = quarterBalanceSheetData.bsa163.ToString("#,##0");
                                        break;
                                }
                            }
                        }
                        dt.Rows.Add(dataRow);
                    }

                    var seperateRow = dt.NewRow();
                    dt.Rows.Add(seperateRow);
                    
                    int lastRowIndex = dgvData.Rows.Count - 1;
                    if (lastRowIndex >= 0)
                    {
                        dgvData.Rows[lastRowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                    }

                    var evaluateCriteria = new[]
                    {
                        "Tốc độ tăng trưởng Doanh số thuần",
                        "Tốc độ tăng trưởng Lãi( lỗ) thuần sau  thuế",
                        "Tốc độ tăng trưởng EPS",
                        "Tốc độ tăng trưởng Hàng tồn kho",
                        "Tốc độ tăng khoản phải thu",
                        "Tỷ suất LNST/ Doanh thu thuần",
                        "Biên lãi gộp"
                    };

                    for (int i = 0; i < evaluateCriteria.Length; i++)
                    {
                        var dataRow = dt.NewRow();
                        for (int j = 0; j < 13; j++)
                        {
                            if (j == 0)
                            {
                                dataRow[j] = evaluateCriteria[i];
                            }
                            else
                            {
                                var quarterIncomeStatementData = incomeStatementObj.items[0].quarterly[j - 1];
                                var quarterBalanceSheetData = balanceSheetObj.items[0].quarterly[j - 1];
                                switch (i)
                                {
                                    case 0:
                                        dataRow[j] = GetNetRevenueGrowthRate(incomeStatementObj, j);
                                        break;
                                    case 1:
                                        dataRow[j] = GetNetProfitGrowthRate(incomeStatementObj, j);
                                        break;
                                    case 2:
                                        dataRow[j] = GetEpsGrowthRate(incomeStatementObj, j);
                                        break;
                                    case 3:
                                        dataRow[j] = GetInventoryGrowthRate(balanceSheetObj, j);
                                        break;
                                    case 4:
                                        dataRow[j] = GetAccReceivableGrowthRate(balanceSheetObj, j);
                                        break;
                                    case 5:
                                        dataRow[j] = GetNetProfitMargin(incomeStatementObj, j);
                                        break;
                                    case 6:
                                        dataRow[j] = GetGrossProfitMargin(incomeStatementObj, j);
                                        break;
                                }
                            }
                        }
                        dt.Rows.Add(dataRow);
                    }

                    StyleDataGridView(dgvData);
                    FormatDataGridViewColumns();

                    // Tắt AutoSize để cho phép scroll
                    dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dgvData.ScrollBars = ScrollBars.Both;
                    dgvData.AutoResizeColumns();
                }
                catch (Exception ex) {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void FormatDataGridViewColumns()
        {
            if (dgvData.Columns.Count == 0) return;

            // Cột đầu tiên: căn trái
            dgvData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvData.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Các cột còn lại: căn giữa
            for (int i = 1; i < dgvData.Columns.Count; i++)
            {
                dgvData.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvData.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            // (Tuỳ chọn) Căn giữa nội dung hàng tiêu đề
            dgvData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        private string GetNetRevenueGrowthRate(Models.IncomeStatementModel incomeStatementObj, int j) {
            var thisQData = incomeStatementObj.items[0].quarterly[j - 1];
            var thisQLastYearData = incomeStatementObj.items[0].quarterly[j + 3];
            var result = (thisQData.isa3 - thisQLastYearData.isa3) / thisQLastYearData.isa3;
            var formattedResult = (result * 100).ToString("F2") + "%";
            return formattedResult;
        }

        private string GetNetProfitGrowthRate(Models.IncomeStatementModel incomeStatementObj, int j)
        {
            var thisQData = incomeStatementObj.items[0].quarterly[j - 1];
            var thisQLastYearData = incomeStatementObj.items[0].quarterly[j + 3];
            var result = (thisQData.isa20 - thisQLastYearData.isa20) / thisQLastYearData.isa20;
            var formattedResult = (result * 100).ToString("F2") + "%";
            return formattedResult;
        }

        private string GetEpsGrowthRate(Models.IncomeStatementModel incomeStatementObj, int j)
        {
            var thisQData = incomeStatementObj.items[0].quarterly[j - 1];
            var thisQLastYearData = incomeStatementObj.items[0].quarterly[j + 3];
            var result = (thisQData.isa23 - thisQLastYearData.isa23) / thisQLastYearData.isa23;
            var formattedResult = (result * 100).ToString("F2") + "%";
            return formattedResult;
        }

        private string GetInventoryGrowthRate(Models.BalanceSheetModel balanceSheetObj, int j)
        {
            var thisQData = balanceSheetObj.items[0].quarterly[j - 1];
            var prevQData = balanceSheetObj.items[0].quarterly[j];
            var result = (thisQData.bsa15 - prevQData.bsa15) / prevQData.bsa15;
            var formattedResult = (result * 100).ToString("F2") + "%";
            return formattedResult;
        }

        private string GetAccReceivableGrowthRate(Models.BalanceSheetModel balanceSheetObj, int j)
        {
            var thisQData = balanceSheetObj.items[0].quarterly[j - 1];
            var prevQData = balanceSheetObj.items[0].quarterly[j];
            var result = (thisQData.bsa8 - prevQData.bsa8) / prevQData.bsa8;
            var formattedResult = (result * 100).ToString("F2") + "%";
            return formattedResult;
        }

        private string GetNetProfitMargin(Models.IncomeStatementModel incomeStatementObj, int j)
        {
            var thisQData = incomeStatementObj.items[0].quarterly[j - 1];
            var result = thisQData.isa20 / thisQData.isa3;
            var formattedResult = (result * 100).ToString("F2") + "%";
            return formattedResult;
        }

        private string GetGrossProfitMargin(Models.IncomeStatementModel incomeStatementObj, int j)
        {
            var thisQData = incomeStatementObj.items[0].quarterly[j - 1];
            var result = thisQData.isa5 / thisQData.isa3;
            var formattedResult = (result * 100).ToString("F2") + "%";
            return formattedResult;
        }
    }
}

