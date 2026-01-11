using System;
using System.Data;
using VeggieShop.DAL;

namespace VeggieShop.BUS
{
    public class RevenueBUS
    {
        private readonly RevenueDAL _dal = new RevenueDAL();

        public DataTable ByDay(DateTime from, DateTime to) => _dal.GetRevenueByDay(from, to);
        public DataTable ByMonth(DateTime from, DateTime to) => _dal.GetRevenueByMonth(from, to);
        public decimal Total(DateTime from, DateTime to) => _dal.GetTotalRevenue(from, to);
    }
}
