using Backend.Models;
using Backend.Repository;

namespace Backend.Services
{
    public class DataPointIntraDayService
    {
        private readonly DataPointIntraDayRepo _dataPointIntraDayRepo;

        public DataPointIntraDayService(DataPointIntraDayRepo dataPointIntraDayRepo)
        {
            _dataPointIntraDayRepo = dataPointIntraDayRepo;
        }
        public async Task<int> AddDataPoint(DataPointIntra dataPointIntra)
        {
            try
            {
                if (dataPointIntra != null)
                {

                    var result = _dataPointIntraDayRepo.Insert(dataPointIntra);
                    if (result != null)
                    {
                        _dataPointIntraDayRepo.SaveChanges();
                    }

                    return result.Id;
                }
                else
                {
                    throw new ArgumentException("Could not save the Data Point!");
                }
            }
            catch (Exception exc)
            {
                throw new ArgumentException(exc.Message);
            }

        }
        public async Task<List<KeyValuePair<String, KeyValuePair<decimal, DateTime>>>> allPerformanceComparison()
        {
            try
            {
                var list = _dataPointIntraDayRepo.GetAllStockNames();
                var performanceList = new List<KeyValuePair<String, KeyValuePair<decimal, DateTime>>>();

                foreach (var item in list)
                {
                    performanceList.AddRange(_dataPointIntraDayRepo.GetPerformanceBySymbol(item));
                }

                return performanceList;
            }
            catch (Exception exc)
            {
                return null;
            }

        }
    }
}
