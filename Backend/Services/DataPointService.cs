﻿using Backend.Models;
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

        public async Task<List<KeyValuePair<String, KeyValuePair<decimal,DateTime>>>> performanceComparisonOfTwoStocks(String stock1, String stock2)
        {
            try
            {
                var performanceList = _dataPointRepository.GetPerformanceBySymbol(stock1);
                performanceList.AddRange(_dataPointRepository.GetPerformanceBySymbol(stock2));
                return performanceList;
            } 
            catch(Exception exc)
            {
                throw new ArgumentException(exc.Message);
            }
            
        }

        public async Task<List<KeyValuePair<String, KeyValuePair<decimal, DateTime>>>> selfPerformanceComparison(String stock1)
        {
            try
            {
                var performanceList = _dataPointRepository.GetPerformanceBySymbol(stock1);
                return performanceList;
            }
            catch (Exception exc)
            {
                throw new ArgumentException(exc.Message);
            }

        }

        public async Task<int> AddDataPoint(DataPoint dataPoint)
        {
            try
            {
                if (dataPoint != null)
                {

                    var result = _dataPointRepository.Insert(dataPoint);
                    if (result != null)
                    {
                        _dataPointRepository.SaveChanges();
                    }

                    return result.Id;
                }
                else
                {
                    throw new ArgumentException("Could not save the Data Point!");
                }
            }
            catch(Exception exc)
            {
                throw new ArgumentException(exc.Message);
            }

        }

    }
}
