import axios from "axios";

class StocksController {
  async insertStockInfo(stockSymbol) {
    try {
      const resp = await axios.post(`/save?stockSymbol=${stockSymbol}`);

      alert(resp.data.message);
    } catch (error) {
      console.error(error.response.data.message);
      alert(error.response.data.message);
    }
  }

  async seePerformanceComparison(stockSymbol1, stockSymbol2) {
    try {
      const response = await axios.get(
        `/performance-comparison?stockSymbol1=${stockSymbol1}&stockSymbol2=${stockSymbol2}`
      );

      return response.data;
    } catch (error) {
      alert(error.response.data.message);
      return "fail";
    }
  }

  async seeSelfPerformance(stockSymbol) {
    try {
      const response = await axios.get(
        `/self-performance-comparison?stockSymbol=${stockSymbol}`
      );

      return response.data;
    } catch (error) {
      alert(error.response.data.message);
      return "fail";
    }
  }

  async seeSelfPerformanceIntra(stockSymbol) {
    try {
      const response = await axios.get(
        `/self-performance-comparison-intra?stockSymbol=${stockSymbol}`
      );

      return response.data;
    } catch (error) {
      alert(error.response.data.message);
      return "fail";
    }
  }
}

export default new StocksController();
