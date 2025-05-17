namespace FilterStockTools.Utils
{
    public static class Config
    {
        public static string ListOrganizationUrl = "https://fiin-core.ssi.com.vn/Master/GetListOrganization?language=vi";
        public static string BalanceSheetUrl = "https://your-api-domain/GetBalanceSheet?symbol={0}";
        public static string IncomeStatementUrl = "https://your-api-domain/GetIncomeStatement?symbol={0}";
    }
}
