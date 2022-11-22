using Backend.Config;
using Backend.Models;
using System.Xml.Linq;

namespace Backend.Repository
{
    public class DataPointRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public DataPointRepository(ApplicationDbContext applicationDbContext)
        {
            _dataContext = applicationDbContext;
        }

        public int SaveChanges()
        {
            var result = _dataContext.SaveChanges();
            return result;
        }


        public DataPoint? Insert(DataPoint dataPoint)
        {
            if(GetByName(dataPoint.Name).Count == 7)
            {
                _dataContext.DataPointsDaily.RemoveRange(GetByName(dataPoint.Name));
            }
            return _dataContext.DataPointsDaily.Add(dataPoint).Entity;
        }

        public List<DataPoint> GetByName(String name)
        {
            return _dataContext.DataPointsDaily.Where(i => i.Name == name).ToList();
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
