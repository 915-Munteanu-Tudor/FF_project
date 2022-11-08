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
                StockTimeSeries stockTs = await stocksClient.GetTimeSeriesAsync(stockSymbol.Str, Interval.Daily, OutputSize.Compact, isAdjusted: true);
                var dataPoints = stockTs.DataPoints.ToList().GetRange(0,7);

                StockTimeSeries stockTsHourly = await stocksClient.GetTimeSeriesAsync(stockSymbol.Str, Interval.Min60, OutputSize.Full, isAdjusted: true);
                var dataPointsIntra = stockTs.DataPoints.ToList();


                foreach (var dataPoint in dataPoints)
                {
                    try
                    {
                        await _dataPointService.AddDataPoint(
                                new DataPoint(
                                stockSymbol.Str,
                                dataPoint.ClosingPrice,
                                dataPoint.HighestPrice,
                                dataPoint.LowestPrice,
                                dataPoint.OpeningPrice,
                                dataPoint.Time,
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
                                stockSymbol.Str,
                                dataPoint.ClosingPrice,
                                dataPoint.HighestPrice,
                                dataPoint.LowestPrice,
                                dataPoint.OpeningPrice,
                                dataPoint.Time,
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
                var res = _dataPointService.performanceComparisonOfTwoStocks(stockSymbols.Str1, stockSymbols.Str2);
                return await res;
            }
            catch (Exception exc)
            {
                var x = exc.Message;
                return null;
            }
        }

        [HttpPost]
        [Route("all-performance-comparison")]
        public async Task<List<KeyValuePair<String, KeyValuePair<decimal, DateTime>>>> allPerformanceComparison()
        {
            try
            {
                var res = _dataPointService.allPerformanceComparison();
                return await res;
            }
            catch (Exception exc)
            {
                var x = exc.Message;
                return null;
            }
        }

        [HttpPost]
        [Route("all-performance-comparison-intra")]
        public async Task<List<KeyValuePair<String, KeyValuePair<decimal, DateTime>>>> allPerformanceComparisonIntra()
        {
            try
            {
                var res = _dataPointIntraDayService.allPerformanceComparison();
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
