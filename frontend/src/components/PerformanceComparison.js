import React, { useEffect } from "react";
import { useState } from 'react';
import StocksController from "../controllers/StocksController";
import './css/Comparisons.css';
import MyChart from "./MyChart";


const SeePerformance = () => {

    const [symbol1, setSymbol1] = useState('');
    const [symbol2, setSymbol2] = useState('');
    const [temp1, setTemp1] = useState('');
    const [temp2, setTemp2] = useState('');
    const [data, setData] = useState([]);
    const [days, setDays] = useState([]);
    const [dataS1, setDataS1] = useState([]);
    const [dataS2, setDataS2] = useState([]);
    const [flag, setFlag] = useState(false);
    const [display, setDisplay] = useState(false);


    const handleSubmit = (e) => {
        e.preventDefault();

        setTemp1(symbol1)
        setTemp2(symbol2)

        const data_symb = {
            symbol1: symbol1,
            symbol2: symbol2
        };

        StocksController.seePerformanceComparison(data_symb.symbol1, data_symb.symbol2).then((response) => {
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


    useEffect(() => {
        
        if (flag === false) {
            setDataS1([]);
            setDataS2([]);
            setDisplay(false);
        } else {
            setDataS1(data.filter(i => i.key === symbol1).map((item) => item.value.key))
            setDataS2(data.filter(i => i.key === symbol2).map((item) => item.value.key))
            setDisplay(true);
        }

        let days_new = Array.from(new Set(data.map((item)=> item.value.value)));
        setDays(days_new);
        console.log(days);  

    }, [data,flag]);


    return (
        <div>
            <div>
            <form >
                <h2>Get performance comparison for the last week of two stocks</h2>
                <label>Symbol 1</label>{' '}
                <input id="input1" type="text" placeholder="Symbol1"
                        onChange={e => setSymbol1(e.target.value)} /> {' '}
                <label>Symbol 2</label>{' '}
                <input id="input2" type="text" placeholder="Symbol2"
                        onChange={e => setSymbol2(e.target.value)} /> {'  '}
                <button type="button" onClick={handleSubmit}>See comparison</button>
            </form>
            </div>
            {  
                 display  && (
                    <MyChart s1={temp1} s2={temp2} dataS1={dataS1} dataS2={dataS2} days={days} />
                )
            }
        </div>
    );
};

export default SeePerformance;
