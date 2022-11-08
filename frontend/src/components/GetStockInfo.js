import React from "react";
import { useState } from 'react';
import StocksController from "../controllers/StocksController";


const AddStock = () => {
    const [symbol, setSymbol] = useState('');

    const handleSubmit = e => {
        e.preventDefault();
        StocksController.insertStockInfo(symbol);
    }

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <h2>Get stock info</h2>
                <label>Symbol</label>{' '}
                <input type="text" placeholder="Symbol"
                            onChange={e => setSymbol(e.target.value)} /> {' '}
                <button>Get Info</button>

            </form>
        </div>
    );
};

export default AddStock;