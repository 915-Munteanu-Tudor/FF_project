import React, { useEffect } from "react";
import { useState } from 'react';
import StocksController from "../controllers/StocksController";
import './css/Comparisons.css';
import MyChart from "./MyChart";


const SelfPerformanceIntra = () => {

    const [symbol1, setSymbol1] = useState('');
    const [temp1, setTemp1] = useState('');
    const [data, setData] = useState([]);
    const [days, setDays] = useState([]);
    const [dataS1, setDataS1] = useState([]);
    const [flag, setFlag] = useState(false);
    const [display, setDisplay] = useState(false);


    const handleClick = (e) => {
        e.preventDefault();

        setTemp1(symbol1)

        const data_symb = {
            symbol1: symbol1,
        };

        StocksController.seeSelfPerformanceIntra(data_symb.symbol1).then((response) => {
            if(response === "fail"){
                setFlag(false);
                setData([]);
            }
            else {
                setFlag(true);
                setData(response);
            }

        });

    }

    const handleSubmit = e => {
        e.preventDefault();
    }

    useEffect(() => {

        if (flag === false) {
            setDataS1([]);
            setDisplay(false);
        } else {
            setDataS1(data.filter(i => i.key === symbol1).map((item) => item.value.key))
            setDisplay(true);
        }

        let days_new = Array.from(new Set(data.map((item)=> item.value.value)));
        setDays(days_new);
        console.log(days);

    }, [data,flag]);


    return (
        <div>
            <div>
            <form onSubmit={handleSubmit}>
                <h2>Self performance comparison intraday for the last week of a stock</h2>
                <label>Symbol 1</label>{' '}
                <input id="input1" type="text" placeholder="Symbol1"
                        onChange={e => setSymbol1(e.target.value)} /> {' '}
                <button type="button" onClick={handleClick}>See comparison</button>
            </form>
            </div>
            {  
                 display  && (
                    <MyChart s1={temp1} s2={''} dataS1={dataS1} dataS2={[]} days={days} />
                )
            }
        </div>
    );
};

export default SelfPerformanceIntra;
