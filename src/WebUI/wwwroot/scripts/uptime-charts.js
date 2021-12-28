var options = {
    stroke: {
        curve: 'straight',
        width: 2, //ustawia odstwpy miedzy kwadratami
      },
    chart: {
        animations: {
            enabled: true,
            easing: 'easeinout',
            speed: 400,
            animateGradually: {
                enabled: true,
                delay: 150
            },
            dynamicAnimation: {
                enabled: true,
                speed: 350
            }
        },
        toolbar: {
            show: false
        },
        height: '100',
        type: "heatmap"
    },
    colors: ["#0057fc"],
    dataLabels: {
        enabled: false
    },
    plotOptions: {
        heatmap: {
            enableShades: false,
          
          colorScale: {
            ranges: [{
                from: 0,
                to: 0,
                name: 'low',
                color: '#ff0000'
              },
              {
                from: 1,
                to: 1,
                name: 'medium',
                color: '#0057fc'
              },
              
            ]
          }
        }
      },
    
    series: [
        {
            name: "test",
            data: [
                1,1,1,0
            ]
        }

    ],
    fill: {
        type: "gradient",
        gradient: {
            shadeIntensity: 1,
            opacityFrom: 0.6,
            opacityTo: 0.0,
            stops: [0, 90, 100]
        }
    },
    grid: {
        show: false
    },
    yaxis: {
        show: false
    },
    tooltip: {
        show: false,
    },
    xaxis:
    {
        axisBorder: {
            show: false
        },
        axisTicks: {
            show: false,
        },
        labels: {
            show: false
        },
        tooltip: {
            enabled: false
        }

    },

    legend: {
        show: false
    },
    //tooltip: {
    //    enabled: false
    //}
};
  
  var chart1 = new ApexCharts(document.querySelector("#chart-1"), options);
  var chart2 = new ApexCharts(document.querySelector("#chart-2"), options);
  
  chart1.render();
  chart2.render();