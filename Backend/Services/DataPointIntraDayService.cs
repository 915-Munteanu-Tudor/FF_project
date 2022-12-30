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
            var result = await _dataPointIntraDayRepo.Insert(dataPointIntra);
            if (result != null)
            {
                await _dataPointIntraDayRepo.SaveChanges();
            }

            return result!.Id;
        }

        public async Task<List<KeyValuePair<string, KeyValuePair<decimal, DateTime>>>> selfPerformanceComparisonIntra(string stock1)
        {

            try
            {
                var performanceList = await _dataPointIntraDayRepo.GetPerformanceBySymbol(stock1);
                return performanceList;
            }
            catch (Exception exc)
            {
                throw new ArgumentException(exc.Message);
            }

        }
    }
}
