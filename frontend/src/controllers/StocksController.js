import axios from "axios";

class StocksController {
    async insertStockInfo(stockSymbol) {
        try {
            await axios.post(`/save?stockSymbol=${stockSymbol}`);

            alert("Successfully got info about a new stock!");
        } catch (error) {
            console.error("error-insert",  error.response.data.message);
            alert(error.response.data.message);
        }
    }

    async seePerformanceComparison(stockSymbol1, stockSymbol2) {

        const response = await axios.get(`/performance-comparison?stockSymbol1=${stockSymbol1}&stockSymbol2=${stockSymbol2}`);
        if (response.data === "") {
            alert("Comparison failed!");
            return "fail";
        }
        else {
            return response.data;
        }

    }

    async seeSelfPerformance(stockSymbol) {

        const response = await axios.get(`/self-performance-comparison?stockSymbol=${stockSymbol}`);
        if (response.data === "") {
            alert("Comparison failed!");
            return "fail";

        }
        else {
            return response.data;
        }
    }

    async seeSelfPerformanceIntra(stockSymbol) {

        const response = await axios.get(`/self-performance-comparison-intra?stockSymbol=${stockSymbol}`);
        if (response.data === "") {
            alert("Comparison failed!");
            return "fail";

        }
        else {
            console.log(response.data);
            return response.data;
        }
    }
}

export default new StocksController();