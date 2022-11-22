using Backend.Config;
using Backend.Models;

namespace Backend.Repository
{
    public class DataPointIntraDayRepo
    {
        private readonly ApplicationDbContext _dataContext;

        public DataPointIntraDayRepo(ApplicationDbContext applicationDbContext)
        {
            _dataContext = applicationDbContext;
        }

        public int SaveChanges()
        {
            var result = _dataContext.SaveChanges();
            return result;
        }


        public DataPointIntra? Insert(DataPointIntra dataPointIntra)
        {
            if (GetByName(dataPointIntra.Name).Count == 24)
            {
                _dataContext.DataPointsIntraDay.RemoveRange(GetByName(dataPointIntra.Name));
            }
            return _dataContext.DataPointsIntraDay.Add(dataPointIntra).Entity;
        }

        public List<DataPointIntra> GetByName(String name)
        {
            return _dataContext.DataPointsIntraDay.Where(i => i.Name == name).ToList();
        }

        public List<KeyValuePair<String, KeyValuePair<decimal, DateTime>>> GetPerformanceBySymbol(String name)
        {
            var stock = GetByName(name);

            if (stock.Count() == 0)
            {
                throw new ArgumentException("This stock symbol is not in the database");
            }
            var performanceLIst = new List<KeyValuePair<String, KeyValuePair<decimal, DateTime>>>();

            foreach (var item in stock)
            {
                var dayPerformance = (item.ClosingPrice - stock.ElementAt(0).ClosingPrice) / item.ClosingPrice * 100;
                var perfDayPair = new KeyValuePair<decimal, DateTime>(dayPerformance, item.Time);

                performanceLIst.Add(new KeyValuePair<String, KeyValuePair<decimal, DateTime>>(name, perfDayPair));
            }

            return performanceLIst;
        }
    }
}
