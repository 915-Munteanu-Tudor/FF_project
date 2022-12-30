using Backend.Config;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository
{
    public class DataPointIntraDayRepo
    {
        private readonly ApplicationDbContext _dataContext;

        public DataPointIntraDayRepo(ApplicationDbContext applicationDbContext)
        {
            _dataContext = applicationDbContext;
        }

        public async Task<int> SaveChanges()
        {
            var result = await _dataContext.SaveChangesAsync();
            return result;
        }


        public async Task<DataPointIntra> Insert(DataPointIntra dataPointIntra)
        {
            var dataPoints = await GetByName(dataPointIntra.Name);
            if (dataPoints.Count == 24)
            {
                _dataContext.DataPointsIntraDay.RemoveRange(dataPoints);
            }
            return _dataContext.DataPointsIntraDay.Add(dataPointIntra).Entity;
        }

        public async Task<List<DataPointIntra>> GetByName(string name)
        {
            return await _dataContext.DataPointsIntraDay.Where(i => i.Name == name).ToListAsync();
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

                performanceLIst.Add(new KeyValuePair<string, KeyValuePair<decimal, DateTime>>(name, perfDayPair));
            }

            return performanceLIst;
        }
    }
}
