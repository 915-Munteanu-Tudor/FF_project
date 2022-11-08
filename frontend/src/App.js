import { BrowserRouter as Router,Route, Routes } from 'react-router-dom';
import './App.css';
import GetStockInfo from './components/GetStockInfo';
import PerformanceComparison from './components/PerformanceComparison'
import AllPerformance from './components/AllPerformance'
import AllPerformanceIntra from './components/AllPerformanceIntra'
import Home from './components/Home';

function App() {
  return (
    <div className="App">
       <Router>
          <Routes>
            <Route exact path="/" element={<Home />} />
            <Route exact path="/save" element={<GetStockInfo />} />
            <Route exact path="/performance-comparison" element={<PerformanceComparison />}  />
            <Route exact path="/all-performance-comparison" element={<AllPerformance />} />
            <Route exact path="/all-performance-comparison-intra" element={<AllPerformanceIntra />} />
          </Routes>
        </Router>
    </div>
  );
}

export default App;
