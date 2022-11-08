import React from "react";
import { useState, useEffect } from 'react';
import StocksController from "../controllers/StocksController";
import './Comparisons.css';

const SeePerformance = () => {
    const [symbol1, setSymbol1] = useState('');
    const [symbol2, setSymbol2] = useState('');
    const [data, setData] = useState([]);
    const [display, setDisplay] = useState(false);

    const handleSubmit = e => {
        e.preventDefault();
        //setData(PerformanceController.seePerformanceComparison(symbol1, symbol2));
        //console.log(data);
        const data_symb = {
            symbol1: symbol1,
            symbol2: symbol2
        };
        StocksController.seePerformanceComparison(data_symb.symbol1, data_symb.symbol2).then((response) => {
            if(response === "fail"){
                setData([]);
                setDisplay(false);
            }
            else {
                setData(response);
                setDisplay(true);
            }

        });

    }

    const renderTableData = () => {
        
        //console.log(data)
        if (data !== [] && data.length > 0) {
            return data.map((item, index) => {
                console.log(item);
                return (
                    <tr key={index}>
                        <td>{item.key}</td>
                        <td>{item.value.value}</td>
                        <td>{item.value.key}</td>
                    </tr>
                )
            });
        }
    }

    useEffect(() => {
        renderTableData();
    }, [data])


    return (
        <div>
            <div>
            <form onSubmit={handleSubmit}>
                <h2>Get performance comparison</h2>
                <label>Symbol 1</label>{' '}
                <input type="text" placeholder="Symbol1"
                            onChange={e => setSymbol1(e.target.value)} /> {' '}
                <label>Symbol 2</label>{' '}
                <input type="text" placeholder="Symbol2"
                            onChange={e => setSymbol2(e.target.value)} /> {'  '}
                <button>See comparison</button>
            </form>
            </div>
            {
                display  && (
                    <div className="tbl">
                        <table>
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Value</th>
                                    <th>Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                {renderTableData()}
                            </tbody>
                        </table>
                    </div>
                )
            }
        </div>
    );
};

export default SeePerformance;