using Backend.Models;
using Backend.Services;
using AlphaVantage.Net.Common.Intervals;
using AlphaVantage.Net.Common.Size;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Stocks;
using AlphaVantage.Net.Stocks.Client;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly DataPointService _dataPointService;
        private readonly DataPointIntraDayService _dataPointIntraDayService;
        private readonly string apiKey = "BD3RKUOGNZR8LYRZ";
        private readonly AlphaVantageClient client;
        private readonly StocksClient stocksClient;

        public StocksController(DataPointService dataPointService, DataPointIntraDayService dataPointIntraDayService)
        {
            _dataPointService = dataPointService;
            _dataPointIntraDayService = dataPointIntraDayService;
            client = new AlphaVantageClient(apiKey);
            stocksClient = client.Stocks();
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> InsertAsync([FromQuery(Name = "stockSymbol")] string stockSymbol)
        {            
            try
            {
                StockTimeSeries stockTs = await stocksClient.GetTimeSeriesAsync(stockSymbol, Interval.Daily, OutputSize.Compact, isAdjusted: true);
                var dataPoints = stockTs.DataPoints.ToList().GetRange(0,7);

                StockTimeSeries stockTsHourly = await stocksClient.GetTimeSeriesAsync(stockSymbol, Interval.Min60, OutputSize.Compact, isAdjusted: true);
                var dataPointsIntra = stockTsHourly.DataPoints.ToList().GetRange(0, 24);


                foreach (var dataPoint in dataPoints)
                {
                    await _dataPointService.AddDataPoint(
                            new DataPoint(
                            stockSymbol,
                            dataPoint.ClosingPrice,
                            dataPoint.HighestPrice,
                            dataPoint.LowestPrice,
                            dataPoint.OpeningPrice,
                            new DateTime(dataPoint.Time.Year, dataPoint.Time.Month, dataPoint.Time.Day, dataPoint.Time.Hour, dataPoint.Time.Minute, dataPoint.Time.Second),
                            dataPoint.Volume
                        ));
                }

                foreach (var dataPoint in dataPointsIntra)
                {
                    await _dataPointIntraDayService.AddDataPoint(
                            new DataPointIntra(
                            stockSymbol,
                            dataPoint.ClosingPrice,
                            dataPoint.HighestPrice,
                            dataPoint.LowestPrice,
                            dataPoint.OpeningPrice,
                            new DateTime(dataPoint.Time.Year, dataPoint.Time.Month, dataPoint.Time.Day, dataPoint.Time.Hour, dataPoint.Time.Minute, dataPoint.Time.Second),
                            dataPoint.Volume
                        ));
                }

                return StatusCode(201, new Response { Success = true, Message = "The Data Points were successfully added!" });
            }
            catch (Exception exc)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "The Data Points could not be added",
                    Errors = new List<string> { exc.Message }
                });
            }
        }

        [HttpGet]
        [Route("performance-comparison")]
        public async Task<IActionResult> performanceComparison([FromQuery(Name = "stockSymbol1")] string stockSymbol1, [FromQuery(Name = "stockSymbol2")] string stockSymbol2)
        {
            try
            {
                return Ok(await _dataPointService.performanceComparisonOfTwoStocks(stockSymbol1, stockSymbol2));
            }
            catch (Exception exc)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "One or both stock symbols are not in the database!",
                    Errors = new List<string> { exc.Message }
                });
            }
        }

        [HttpGet]
        [Route("self-performance-comparison")]
        public async Task<IActionResult> selfPerformanceComparison([FromQuery(Name = "stockSymbol")] string stockSymbol)
        {
            try
            {
                return Ok(await _dataPointService.selfPerformanceComparison(stockSymbol));
            }
            catch (Exception exc)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "This stock symbol is not in the database!",
                    Errors = new List<string> { exc.Message }
                });
            }
        }

        [HttpGet]
        [Route("self-performance-comparison-intra")]
        public async Task<IActionResult> selfPerformanceComparisonIntra([FromQuery(Name = "stockSymbol")] string stockSymbol)
        {
            try
            {
                return Ok(await _dataPointIntraDayService.selfPerformanceComparisonIntra(stockSymbol));
            }
            catch (Exception exc)
            {
                return BadRequest(new Response
                {
                    Success = false,
                    Message = "This stock symbol is not in the database!",
                    Errors = new List<string> { exc.Message }
                });
            }
        }
    }

 }
