import React from "react";
import './Home.css';



const Home = () => {

    return (
        <div className="main">
            <div>
                <h2 className="menu">MENU</h2>
                <a href="/save">
                    <button className="btn" variant="info">Add a Stock</button>
                    <br/>
                </a>
            </div>
            <div>
                <a href="/performance-comparison">
                    <button className="btn" variant="info">Compare Two Stocks</button>
                    <br/><br/>
                </a>
            </div>
            <div>
                <a href="/all-performance-comparison">
                    <button className="btn" variant="info">Compare Stocks Daily</button>
                    <br/><br/>
                </a>
            </div>
            <div>
                <a href="/all-performance-comparison-intra">
                    <button className="btn" variant="info">Compare Stocks Intraday</button>
                    <br/><br/>
                </a>
            </div>
        </div>


    );

};


export default Home;