using System.Threading.Tasks;
using FilterStockTools.Utils;

namespace FilterStockTools.Services
{
    public class SyncService
    {
        public async Task SyncAllData()
        {
            var api = new ApiService();
            var db = new DatabaseService();

            var orgJson = await api.GetListOrganizationsAsync();
            db.SaveJson("Organizations", "ALL", orgJson);

            //var symbols = JsonHelper.ParseSymbols(orgJson);

            //foreach (var symbol in symbols)
            //{
            //    var balance = await api.GetBalanceSheetAsync(symbol);
            //    var income = await api.GetIncomeStatementAsync(symbol);

            //    db.SaveJson("BalanceSheets", symbol, balance);
            //    db.SaveJson("IncomeStatements", symbol, income);
            //}
        }
    }
}
