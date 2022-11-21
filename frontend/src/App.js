import { BrowserRouter as Router,Navigate,Route, Routes } from 'react-router-dom';
import './App.css';
import AddStock from './components/GetStockInfo';
import SeePerformance from './components/PerformanceComparison'
import SelfPerformance from './components/SelfPerformanceComparison'
import SelfPerformanceIntra from './components/SelfPerformanceIntra'
import Home from './components/Home';

function App() {
  return (
    <div className="App">
       <Router>
          <Routes>
            <Route exact path="/" element={<Home />} />
            <Route exact path="/save" element={<AddStock />} />
            <Route exact path="/performance-comparison" element={<SeePerformance />}  />
            <Route exact path="/self-performance-comparison" element={<SelfPerformance />} />
            <Route exact path="/self-performance-comparison-intra" element={<SelfPerformanceIntra />} />
            <Route
              path="*"
              element={<Navigate to="/" />}
            />
          </Routes>
        </Router>
    </div>
  );
}

export default App;
