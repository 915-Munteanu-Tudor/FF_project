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
            //console.error("error-insert",  error.response.data.message);
            alert(error.response.data.message);
        }
    }

    async seePerformanceComparison(stockSymbol1, stockSymbol2) {

        const user = {
            str1: stockSymbol1,
            str2: stockSymbol2
        };

        const response = await axios.post('/performance-comparison', user, {headers: {'Content-Type': 'application/json'}});
        //console.log(response.data);
        if (response.data === "") {
            alert("Comparison failed!");
            return "fail";

        }
        else {
            return response.data;
        }

    }

    async getPerformancesDaily() {
        try {
            //console.log("new", stockSymbol);
            //const usersName = JSON.stringify({"str": "SPY"});

            const response = await axios.post('/all-performance-comparison');
            console.log(response.data)
            //alert("Successfully got info about a new stock!");
            return response.data;
        } catch (error) {
            console.error("error-comparison", error);
            alert("Comparison failed", error);
        }
    }

    async getPerformancesIntra() {
        try {
            //console.log("new", stockSymbol);
            //const usersName = JSON.stringify({"str": "SPY"});

            const response = await axios.post('/all-performance-comparison-intra');
            console.log(response.data)
            //alert("Successfully got info about a new stock!");
            return response.data;
        } catch (error) {
            console.error("error-comparison", error);
            alert("Comparison failed", error);
        }
    }
}

export default new StocksController();