import React from 'react';
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';
import { Line } from 'react-chartjs-2';


const MyChart = (s1,s2,dataS1,dataS2,days) => {
  ChartJS.register(
      CategoryScale,
      LinearScale,
      PointElement,
      LineElement,
      Title,
      Tooltip,
      Legend
  );
      
  const options = {
    responsive: true,
    plugins: {
        legend: {
        position: 'top',
        },
      title: {
      display: true,
      text: '',
      },
    },


  };

  console.log(days);

  const dataOpt = {
    labels: days,
    datasets: [
        {
        label: s1 ,
        data: dataS1,
        borderColor: 'rgb(255, 99, 132)',
        backgroundColor: 'rgba(255, 99, 132, 0.5)',
        },
        {
        label: s2,
        data: dataS2,
        borderColor: 'rgb(53, 162, 235)',
        backgroundColor: 'rgba(53, 162, 235, 0.5)',
        },
    ],
  };


  if (s2 === '') {
    dataOpt.datasets.pop();
  }
            
  return (
    <div className='line'>
      <Line options={options} data={dataOpt} />
    </div>
  )
}

export default MyChart;
