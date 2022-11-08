import React from "react";
import { useState,useEffect } from 'react';
import StocksController from "../controllers/StocksController";
import './Comparisons.css';

const AllPerformance = () => {
    const [data, setData] = useState([]);
    const [display, setDisplay] = useState(false);

    const handleSubmit = e => {
        e.preventDefault();
        StocksController.getPerformancesDaily().then((response) => {
            console.log(response);
            setDisplay(true);
            setData(response);
            console.log(data);
        })
    };

    
    const renderTableData = () => {
        
        //console.log(data)
        if (data !== [] && data.length > 0) {
            return data.map((item, index) => {
                console.log(item);
                return (
                    <tr key={index}>
                        <td>{item.key}</td>
                        <td>{item.value.key}</td>
                        <td>{item.value.value}</td>
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
                <form onSubmit={handleSubmit}>
                    <h2>See all performance</h2>
                    <button>Get all performance</button>
                </form>
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

export default AllPerformance;