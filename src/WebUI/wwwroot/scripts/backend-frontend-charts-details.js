var options_3 = {
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
        type: 'bar',
        height: 150,
        stacked: true,
        stackType: '100%'
    },
    colors: ["#0057fc", "#5d95ff"],
    dataLabels: {
        enabled: false
    },


    series: [{
        name: 'Czas ładowania backend',
        data: [44, 55, 41, 67, 22, 43, 21, 49, 44, 55, 41, 67, 22, 43, 21, 49, 13, 23, 20, 8, 13, 27, 33, 12, 44, 55, 41, 67, 22, 43, 21, 49]
    }, {
        name: 'Czas ładowania frontend',
        data: [13, 23, 20, 8, 13, 27, 33, 12, 44, 55, 41, 67, 22, 43, 21, 49, 13, 23, 20, 8, 13, 27, 33, 12, 44, 55, 41, 67, 22, 43, 21, 49]
    }],

    grid: {
        show: false
    },
    yaxis: {
        show: false
    },
    tooltip: {
        show: false,
    },
    xaxis: {
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



var chart3 = new ApexCharts(document.querySelector("#chart-3"), options_3);



chart3.render();