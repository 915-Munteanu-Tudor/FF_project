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


//const MyChart = (s1,s2,dataS1,dataS2,days) => {
const MyChart = props => {
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
      tooltip: {
        yAlign: 'bottom',
        callbacks: {
          label(tooltipItems) {
            return `${tooltipItems.formattedValue} %`
          }
        }
      },
      title: {
        display: true,
        text: '',
      },
    },
    scales: {
      x: {
        grid : {
          lineWidth: context => context.tick.value === 0 ? 2 : 0,
          drawBorder: false,
        },
          ticks: {
              callback: function() {
                  return '';
              }
          }
      },
      y: {
        suggestedMin: () => {
          if (props.s2 === '')
            return Math.abs(Math.floor(Math.min(...props.dataS1))) > Math.abs(Math.ceil(Math.max(...props.dataS1))) ? -Math.abs(Math.floor(Math.min(...props.dataS1))) : -Math.abs(Math.ceil(Math.max(...props.dataS1)));
          else 
            return -Math.max(Math.abs(Math.floor(Math.min(...props.dataS1))), Math.abs(Math.ceil(Math.max(...props.dataS1))), Math.abs(Math.floor(Math.min(...props.dataS2))), Math.abs(Math.ceil(Math.max(...props.dataS2))));
        },
        suggestedMax: () => {
          if (props.s2 === '')
            return Math.abs(Math.floor(Math.min(...props.dataS1))) > Math.abs(Math.ceil(Math.max(...props.dataS1))) ? Math.abs(Math.floor(Math.min(...props.dataS1))) : Math.abs(Math.ceil(Math.max(...props.dataS1)));
          else 
            return Math.max(Math.abs(Math.floor(Math.min(...props.dataS1))), Math.abs(Math.ceil(Math.max(...props.dataS1))), Math.abs(Math.floor(Math.min(...props.dataS2))), Math.abs(Math.ceil(Math.max(...props.dataS2))));
        },
        grid : {
          lineWidth: context => context.tick.value === 0 ? 2 : 0,
        },
        ticks: {
            callback: function(value) {
                return value + ' %';
              }
          }
      } 
    } 

  };

  console.log(props.days);

  const dataOpt = {
    labels: props.days,
    datasets: [
        {
        label: props.s1 ,
        data: props.dataS1,
        borderColor: 'rgb(255, 99, 132)',
        backgroundColor: 'rgba(255, 99, 132, 0.5)',
        },
        {
        label: props.s2,
        data: props.dataS2,
        borderColor: 'rgb(53, 162, 235)',
        backgroundColor: 'rgba(53, 162, 235, 0.5)',
        },
    ],
  };


  if (props.s2 === '') {
    dataOpt.datasets.pop();
  }


  return (
    <div className='line'>
      <Line options={options} data={dataOpt} />
    </div>
  )
}

export default MyChart;
