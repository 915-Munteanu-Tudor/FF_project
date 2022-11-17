import React from "react";
import './css/Home.css';



const Home = () => {

    return (
        <div className="main">
            <div>
                <h2 className="menu">MENU</h2>
                <a href="/save">
                    <button className="btn" variant="info">Get Stock Info</button>
                    <br/>
                </a>
            </div>
            <div>
                <a href="/performance-comparison">
                    <button className="btn" variant="info">Compare Two Stocks</button>
                    <br/>
                </a>
            </div>
            <div>
                <a href="/self-performance-comparison">
                    <button className="btn" variant="info">Stocks Performance Daily</button>
                    <br/><br/>
                </a>
            </div>
            <div>
                <a href="/self-performance-comparison-intra">
                    <button className="btn" variant="info">Stocks Performance Intraday</button>
                    <br/><br/>
                </a>
            </div>
        </div>


    );

};


export default Home;