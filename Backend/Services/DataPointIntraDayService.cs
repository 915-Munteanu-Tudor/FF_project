﻿using Backend.Models;
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
        public async Task<List<KeyValuePair<String, KeyValuePair<decimal, DateTime>>>> selfPerformanceComparisonIntra(String stock1)
        {

            try
            {
                var performanceList = _dataPointIntraDayRepo.GetPerformanceBySymbol(stock1);
                return performanceList;
            }
            catch (Exception exc)
            {
                throw new ArgumentException(exc.Message);
            }

        }
    }
}