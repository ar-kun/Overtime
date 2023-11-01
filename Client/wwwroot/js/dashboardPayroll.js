console.log("Ini dashboard payroll js");

// Get total count status payment of all request
$.ajax({
    type: 'GET',
    contentType: "application/json",
    url: 'https://localhost:7166/api/PaymentDetail',
    dataType: 'json',
    success: function (data) {
        var allCount = data.data.length; // get the length of the data array
        console.log('Count of All Payment Details:', allCount); // log the count to the console
        var paidCount = 0;
        var unpaidCount = 0;
        $.each(data.data, function (index, value) {
            if (value.paymentStatus === 0) { // check if the status is unpaid
                unpaidCount++;
            }
            else if (value.paymentStatus === 1) { // check if the status is paid
                paidCount++;
            }
        });
        console.log('Count of Paid Status :', paidCount);
        console.log('Count of Unpaid Status :', unpaidCount);

        $("#monthlyRequested").html(`<div class="text-xs font-weight-bold text-primary text-uppercase mb-1">All Payments</div>
        <div class="h5 mb-0 font-weight-bold text-gray-800">${allCount}</div>`);
        $("#monthlyApproved").html(`<div class="text-xs font-weight-bold text-success text-uppercase mb-1">Paid Payment</div>
        <div class="h5 mb-0 font-weight-bold text-gray-800">${paidCount}</div>`);
        $("#monthlyRejected").html(`<div class="text-xs font-weight-bold text-warning text-uppercase mb-1">Unpaid Payment</div>
        <div class="h5 mb-0 font-weight-bold text-gray-800">${unpaidCount}</div>`);
    },
    error: function () {
        console.log('Error occurred while fetching data from API');
    }
});

// Chart for status payment
$.ajax({
    type: 'GET',
    contentType: "application/json",
    url: 'https://localhost:7166/api/PaymentDetail',
    dataType: 'json',
    success: function (data) {
        var paymentCount = {
            'Paid': 0,
            'Unpaid': 0
        };
        $.each(data.data, function (index, value) {
            if (value.paymentStatus === 0) { // check if the status is unpaid
                paymentCount['Unpaid']++; // increment the count
            }
            else if (value.paymentStatus === 1) { // check if the status is paid
                paymentCount['Paid']++; // increment the count
            }
        });
        var ctx = document.getElementById('myPieChart').getContext('2d');
        var myPieChart = new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: ['Paid', 'Unpaid'],
                datasets: [{
                    data: [paymentCount['Paid'], paymentCount['Unpaid']],
                    backgroundColor: [
                        '#4e73df',
                        '#1cc88a',
                        '#36b9cc'
                    ],
                    hoverBackgroundColor: [
                        '#2e59d9',
                        '#17a673',
                        '#2c9faf'
                    ],
                    hoverBorderColor: "rgba(234, 236, 244, 1)",
                }],
            },
            options: {
                maintainAspectRatio: false,
                tooltips: {
                    backgroundColor: "rgb(255,255,255)",
                    bodyFontColor: "#858796",
                    borderColor: '#dddfeb',
                    borderWidth: 1,
                    xPadding: 15,
                    yPadding: 15,
                    displayColors: false,
                    caretPadding: 10,
                },
                legend: {
                    display: false
                },
                cutoutPercentage: 80,
            },
        });
    },
    error: function () {
        console.log('Error occurred while fetching data from API');
    }
});

// Chart for payments overview
$.ajax({
    type: 'GET',
    contentType: "application/json",
    url: 'https://localhost:7166/api/PaymentDetail',
    dataType: 'json',
    success: function (data) {
        var currentYear = new Date().getFullYear(); // get the current year
        var monthlyTotalPayment = new Array(12).fill(0); // initialize an array of size 12 with 0s
        $.each(data.data, function (index, value) {
            var dateRequest = new Date(value.dateRequest);
            console.log(dateRequest);
            console.log(dateRequest.getFullYear());
            if (dateRequest.getFullYear() === currentYear) { // check if the year of dateRequest is equal to the current year
                monthlyTotalPayment[dateRequest.getMonth()]++; // add the totalPay to the corresponding month
            }
            console.log(monthlyTotalPayment);
        });
        var ctx = document.getElementById('myAreaChart').getContext('2d');
        var myAreaChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                datasets: [{
                    label: 'Total Payment',
                    data: monthlyTotalPayment,
                    lineTension: 0.3,
                    backgroundColor: [
                        'rgba(78, 115, 223, 0.05)'
                    ],
                    borderColor: [
                        /*'rgba(78, 115, 223, 1)'*/
                        '#163270'
                    ],
                    borderWidth: 3,
                    pointRadius: 3,
                    pointBackgroundColor: [
                        /*'rgba(78, 115, 223, 1)'*/
                        '#163270'
                    ],
                    pointBorderColor: [
                        /*'rgba(78, 115, 223, 1)'*/
                        '#163270'
                    ],
                    pointHoverRadius: 4,
                    pointHoverBackgroundColor: [
                        /*'rgba(78, 115, 223, 1)'*/
                        '#163270'
                    ],
                    pointHoverBorderColor: [
                        /*'rgba(78, 115, 223, 1)'*/
                        '#163270'
                    ],
                    pointHitRadius: 10,
                    pointBorderWidth: 2,
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
                                return value.toLocaleString();
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
                            return datasetLabel + tooltipItem.yLabel.toLocaleString();
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