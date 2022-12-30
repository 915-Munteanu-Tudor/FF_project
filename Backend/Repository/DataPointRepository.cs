using Backend.Config;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> SaveChanges()
        {
            var result = await _dataContext.SaveChangesAsync();
            return result;
        }


        public async Task<DataPoint> Insert(DataPoint dataPoint)
        {
            var dataPoints = await GetByName(dataPoint.Name);
            if (dataPoints.Count == 7)
            {
                _dataContext.DataPointsDaily.RemoveRange(dataPoints);
            }

            var inserted = await _dataContext.DataPointsDaily.AddAsync(dataPoint);
            return inserted.Entity;
        }

        public async Task<List<DataPoint>> GetByName(string name)
        {
            return await _dataContext.DataPointsDaily.Where(i => i.Name == name).ToListAsync();
        }

        public async Task<List<KeyValuePair<string, KeyValuePair<decimal, DateTime>>>> GetPerformanceBySymbol(string name)
        {
            var stock = await GetByName(name);

            if (stock.Count == 0)
            {
                throw new ArgumentException("This stock symbol is not in the database");
            }
            var performanceLIst = new List<KeyValuePair<string, KeyValuePair<decimal, DateTime>>>();

            foreach (var item in stock)
            {
                var dayPerformance = (item.ClosingPrice - stock.ElementAt(0).ClosingPrice) / item.ClosingPrice * 100;
                var perfDayPair = new KeyValuePair<decimal, DateTime>(dayPerformance, item.Time);

                performanceLIst.Add((new KeyValuePair<string, KeyValuePair<decimal, DateTime>>(name, perfDayPair)));
            }

            return performanceLIst;
        }
    }
}
