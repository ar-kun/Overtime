// Get monthly earnings
$.ajax({
    type: 'GET',
    contentType: "application/json",
    url: 'https://localhost:7166/api/PaymentDetail/employee-guid/0c6e30db-461e-4d91-a517-08dbd53b227c',
    dataType: 'json',
    success: function (data) {
        var totalPayMonthly = 0;
        var currentMonth = new Date().getMonth() + 1; // get the current month
        $.each(data.data, function (index, value) {
            var dateRequest = new Date(value.dateRequest);
            if (dateRequest.getMonth() + 1 === currentMonth) { // check if the month of dateRequest is equal to the current month
                totalPayMonthly += value.totalPay;
            }
        });
        console.log('Total Pay:', totalPayMonthly); // log the total pay to the console
        $("#monthlyEarnings").html(`<div class="text-xs font-weight-bold text-primary text-uppercase mb-1">Earnings (Monthly)</div>
        <div class="h5 mb-0 font-weight-bold text-gray-800">Rp ${totalPayMonthly}</div>`);
    },
    error: function () {
        console.log('Error occurred while fetching data from API');
    }
});

// Get annual earnings
$.ajax({
    type: 'GET',
    contentType: "application/json",
    url: 'https://localhost:7166/api/PaymentDetail/employee-guid/0c6e30db-461e-4d91-a517-08dbd53b227c',
    dataType: 'json',
    success: function (data) {
        var totalPayAnnual = 0;
        var currentYear = new Date().getFullYear(); // get the current year
        $.each(data.data, function (index, value) {
            var dateRequest = new Date(value.dateRequest);
            if (dateRequest.getFullYear() === currentYear) { // check if the year of dateRequest is equal to the current year
                totalPayAnnual += value.totalPay;
            }
        });
        console.log('Total Pay:', totalPayAnnual); // log the total pay to the console
        $("#annualEarnings").html(`<div class="text-xs font-weight-bold text-success text-uppercase mb-1">Earnings (Annual)</div>
        <div class="h5 mb-0 font-weight-bold text-gray-800">Rp ${totalPayAnnual}</div>`);
    },
    error: function () {
        console.log('Error occurred while fetching data from API');
    }
});


// Chart
$.ajax({
    type: 'GET',
    contentType: "application/json",
    url: 'https://localhost:7166/api/PaymentDetail/employee-guid/0c6e30db-461e-4d91-a517-08dbd53b227c',
    dataType: 'json',
    success: function (data) {
        var currentYear = new Date().getFullYear(); // get the current year
        var monthlyTotalPay = new Array(12).fill(0); // initialize an array of size 12 with 0s
        $.each(data.data, function (index, value) {
            var dateRequest = new Date(value.dateRequest);
            if (dateRequest.getFullYear() === currentYear) { // check if the year of dateRequest is equal to the current year
                monthlyTotalPay[dateRequest.getMonth()] += value.totalPay; // add the totalPay to the corresponding month
            }
        });
        console.log(monthlyTotalPay);
        var ctx = document.getElementById('myAreaChart').getContext('2d');
        var myAreaChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                datasets: [{
                    label: 'Total Pay',
                    data: monthlyTotalPay,
                    backgroundColor: [
                        'rgba(78, 115, 223, 0.05)'
                    ],
                    borderColor: [
                        'rgba(78, 115, 223, 1)'
                    ],
                    borderWidth: 2,
                    pointRadius: 3,
                    pointBackgroundColor: [
                        'rgba(78, 115, 223, 1)'
                    ],
                    pointBorderColor: [
                        'rgba(78, 115, 223, 1)'
                    ],
                    pointHoverRadius: 3,
                    pointHoverBackgroundColor: [
                        'rgba(78, 115, 223, 1)'
                    ],
                    pointHoverBorderColor: [
                        'rgba(78, 115, 223, 1)'
                    ]
                }]
            },
            options: {
                maintainAspectRatio: false,
                layout: {
                    padding: {
                        left: 10,
                        right: 25,
                        top: 25,
                        bottom: 0
                    }
                },
                scales: {
                    xAxes: [{
                        time: {
                            unit: 'month'
                        },
                        gridLines: {
                            display: false,
                            drawBorder: false
                        },
                        ticks: {
                            maxTicksLimit: 12
                        }
                    }],
                    yAxes: [{
                        ticks: {
                            maxTicksLimit: 5,
                            padding: 10,
                            // Include a dollar sign in the ticks
                            callback: function (value, index, values) {
                                return '$' + value.toLocaleString();
                            }
                        },
                        gridLines: {
                            color: "rgb(234, 236, 244)",
                            zeroLineColor: "rgb(234, 236, 244)",
                            drawBorder: false,
                            borderDash: [2],
                            zeroLineBorderDash: [2]
                        }
                    }]
                },
                legend: {
                    display: false
                },
                tooltips: {
                    backgroundColor: "rgb(255,255,255)",
                    bodyFontColor: "#858796",
                    titleMarginBottom: 10,
                    titleFontColor: '#6e707e',
                    titleFontSize: 14,
                    borderColor: '#dddfeb',
                    borderWidth: 1,
                    xPadding: 15,
                    yPadding: 15,
                    displayColors: false,
                    intersect: false,
                    mode: 'index',
                    caretPadding: 10,
                    callbacks: {
                        label: function (tooltipItem, chart) {
                            var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                            return datasetLabel + ': $' + tooltipItem.yLabel.toLocaleString();
                        }
                    }
                }
            }
        });
    },
    error: function () {
        console.log('Error occurred while fetching data from API');
    }
});
