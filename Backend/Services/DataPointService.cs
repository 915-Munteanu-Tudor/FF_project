using Backend.Models;
using Backend.Repository;

namespace Backend.Services
{
    public class DataPointService
    {
        private readonly DataPointRepository _dataPointRepository;

        public DataPointService(DataPointRepository dataPointRepository)
        {
            _dataPointRepository = dataPointRepository;
        }

        public async Task<List<KeyValuePair<string, KeyValuePair<decimal,DateTime>>>> performanceComparisonOfTwoStocks(string stock1, string stock2)
        {
            try
            {
                var performanceList = await _dataPointRepository.GetPerformanceBySymbol(stock1);
                performanceList.AddRange(await _dataPointRepository.GetPerformanceBySymbol(stock2));
                return performanceList;
            } 
            catch(Exception exc)
            {
                throw new ArgumentException(exc.Message);
            }
            
        }

        public async Task<List<KeyValuePair<string, KeyValuePair<decimal, DateTime>>>> selfPerformanceComparison(string stock1)
        {
            try
            {
                var performanceList = await _dataPointRepository.GetPerformanceBySymbol(stock1);
                return performanceList;
            }
            catch (Exception exc)
            {
                throw new ArgumentException(exc.Message);
            }

        }

        public async Task<int> AddDataPoint(DataPoint dataPoint)
        {
            var result = await _dataPointRepository.Insert(dataPoint);
            if (result != null)
            {
                await _dataPointRepository.SaveChanges();
            }

            return result!.Id;

        }

    }
}
