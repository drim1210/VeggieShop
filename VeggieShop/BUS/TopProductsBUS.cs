using System;
using System.Data;
using VeggieShop.DAL;

namespace VeggieShop.BUS
{
    public class TopProductsBUS
    {
        private readonly TopProductsDAL _dal = new TopProductsDAL();

        public DataTable GetTop(DateTime from, DateTime to, int topN, bool sortByRevenue)
        {
            return _dal.GetTopProducts(from, to, topN, sortByRevenue ? "revenue" : "qty");
        }
    }
}
