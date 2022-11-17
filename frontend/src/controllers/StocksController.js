import axios from "axios";

class StocksController {
    async insertStockInfo(stockSymbol) {
        try {
            const user = {
                str: stockSymbol
            }
            await axios.post('/save', user, {headers: {'Content-Type': 'application/json'}});

            alert("Successfully got info about a new stock!");
        } catch (error) {
            console.error("error-insert",  error.response.data.message);
            alert(error.response.data.message);
        }
    }

    async seePerformanceComparison(stockSymbol1, stockSymbol2) {

        const user = {
            str1: stockSymbol1,
            str2: stockSymbol2
        };

        const response = await axios.post('/performance-comparison', user, {headers: {'Content-Type': 'application/json'}});
        if (response.data === "") {
            alert("Comparison failed!");
            return "fail";

        }
        else {
            return response.data;
        }

    }

    async seeSelfPerformance(stockSymbol) {
        const user = {
            str: stockSymbol,
        };

        const response = await axios.post('/self-performance-comparison', user, {headers: {'Content-Type': 'application/json'}});
        if (response.data === "") {
            alert("Comparison failed!");
            return "fail";

        }
        else {
            return response.data;
        }
    }

    async seeSelfPerformanceIntra(stockSymbol) {
        const user = {
            str: stockSymbol,
        };

        const response = await axios.post('/self-performance-comparison-intra', user, {headers: {'Content-Type': 'application/json'}});
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