var options = {
    stroke: {
        curve: 'smooth',
        width: 2, //ustawia odstwpy miedzy kwadratami
      },
    chart: {
        toolbar: {
            show: false
        },
        height: '100',
        type: "area"
        //type:"line"
    },
    colors: ["#0057FC"],
    dataLabels: {
        enabled: false
    },
   
    
    series: [
        {
            type: "area",
            //type:"line"
            name: "test",
            data: [1980,2020,2600,1632,2832,2932,3716,2631,2736,3920,2382,2678,2837,2516,2736,
                3182,1273,2736,2736,1625,2735,2736,2837,2836,2736,2936,2936,2837,2837,2936
            ]
        },
      
       
    ],
    //jeśli typ jest line to gradient robi gradient po bokach (prawa i lewa)
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
        categories: [
            "01 Jan",
            "02 Jan",
            "03 Jan",
            "04 Jan",
            "05 Jan",
            "06 Jan",
            "07 Jan"
        ],
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