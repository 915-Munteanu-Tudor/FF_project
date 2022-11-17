using Backend.Models;
using Backend.Services;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Size;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Stocks;
using AlphaVantage.Net.Stocks.Client;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly DataPointService _dataPointService;
        private readonly DataPointIntraDayService _dataPointIntraDayService;
        private readonly String apiKey = "BD3RKUOGNZR8LYRZ";
        private AlphaVantageClient client;
        private StocksClient stocksClient;

        public StocksController(DataPointService dataPointService, DataPointIntraDayService dataPointIntraDayService)
        {
            _dataPointService = dataPointService;
            _dataPointIntraDayService = dataPointIntraDayService;
            client = new AlphaVantageClient(apiKey);
            stocksClient = client.Stocks();
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> InsertAsync([System.Web.Http.FromUri] StockName stockSymbol)
        {            
            try
            {
                StockTimeSeries stockTs = await stocksClient.GetTimeSeriesAsync(stockSymbol.Str!, Interval.Daily, OutputSize.Compact, isAdjusted: true);
                var dataPoints = stockTs.DataPoints.ToList().GetRange(0,7);

                StockTimeSeries stockTsHourly = await stocksClient.GetTimeSeriesAsync(stockSymbol.Str!, Interval.Min60, OutputSize.Compact, isAdjusted: true);
                var dataPointsIntra = stockTsHourly.DataPoints.ToList().GetRange(0, 24);


                foreach (var dataPoint in dataPoints)
                {
                    try
                    {
                        await _dataPointService.AddDataPoint(
                                new DataPoint(
                                stockSymbol.Str!,
                                dataPoint.ClosingPrice,
                                dataPoint.HighestPrice,
                                dataPoint.LowestPrice,
                                dataPoint.OpeningPrice,
                                new DateTime(dataPoint.Time.Year, dataPoint.Time.Month, dataPoint.Time.Day, dataPoint.Time.Hour, dataPoint.Time.Minute, dataPoint.Time.Second),
                                dataPoint.Volume
                            ));
                    }
                    catch(Exception exc)
                    {
                        return BadRequest(new Response
                        {
                            Success = false,
                            Message = "The Data Point for this stock symbol is already in database!",
                            Errors = new List<String> { exc.Message }
                        });
                    }
                }

                foreach (var dataPoint in dataPointsIntra)
                {
                    try
                    {
                        await _dataPointIntraDayService.AddDataPoint(
                                new DataPointIntra(
                                stockSymbol.Str!,
                                dataPoint.ClosingPrice,
                                dataPoint.HighestPrice,
                                dataPoint.LowestPrice,
                                dataPoint.OpeningPrice,
                                new DateTime(dataPoint.Time.Year, dataPoint.Time.Month, dataPoint.Time.Day, dataPoint.Time.Hour, dataPoint.Time.Minute, dataPoint.Time.Second),
                                dataPoint.Volume
                            ));
                    }
                    catch (Exception exc)
                    {
                        return BadRequest(new Response
                        {
                            Success = false,
                            Message = "The Data Point for this stock symbol is already in database!",
                            Errors = new List<String> { exc.Message }
                        });
                    }
                }

                return Ok(new Response { Success = true, Message = "Inserted all the Data Points successfully!" });
            }
            catch (Exception exc)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "The Data Points could not be added",
                    Errors = new List<String> { exc.Message }
                });
            }
            

        }

        [HttpPost]
        [Route("performance-comparison")]
        public async Task<List<KeyValuePair<String, KeyValuePair<decimal, DateTime>>>> performanceComparison([FromBody] StockPair stockSymbols)
        {
            try
            {
                var res = _dataPointService.performanceComparisonOfTwoStocks(stockSymbols.Str1!, stockSymbols.Str2!);
                return await res;
            }
            catch (Exception exc)
            {
                var x = exc.Message;
                return null;
            }
        }

        [HttpPost]
        [Route("self-performance-comparison")]
        public async Task<List<KeyValuePair<String, KeyValuePair<decimal, DateTime>>>> selfPerformanceComparison(StockName name)
        {
            try
            {
                var res = _dataPointService.selfPerformanceComparison(name.Str!);
                return await res;
            }
            catch (Exception exc)
            {
                var x = exc.Message;
                return null;
            }
        }

        [HttpPost]
        [Route("self-performance-comparison-intra")]
        public async Task<List<KeyValuePair<String, KeyValuePair<decimal, DateTime>>>> selfPerformanceComparisonIntra(StockName name)
        {
            try
            {
                var res = _dataPointIntraDayService.selfPerformanceComparisonIntra(name.Str!);
                return await res;
            }
            catch (Exception exc)
            {
                var x = exc.Message;
                return null;
            }
        }
    }

 }
