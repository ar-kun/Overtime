﻿console.log("Ini dashboard manager js");
console.log(managerGuidClaim);

// Get total count status overtime request by manager guid
$.ajax({
    type: 'GET',
    contentType: "application/json",
    url: 'https://localhost:7166/api/Overtime/manager-guid/' + managerGuidClaim,
    dataType: 'json',
    success: function (data) {
        var requestedCount = 0;
        var approvedCount = 0;
        var rejectedCount = 0;
        var currentDate = new Date();
        var currentMonthStart = new Date(currentDate.getFullYear(), currentDate.getMonth(), 1); // get the start of the current month
        var currentMonthEnd = new Date(currentDate.getFullYear(), currentDate.getMonth() + 1, 0); // get the end of the current month
        $.each(data.data, function (index, value) {
            if (value.status === 'Requested') { // check if the status is requested
                var dateRequest = new Date(value.dateRequest);
                if (dateRequest >= currentMonthStart && dateRequest <= currentMonthEnd) { // check if the date is within the current month
                    requestedCount++; // increment the count
                }
            }
            else if (value.status === 'Approved') { // check if the status is approved
                var dateRequest = new Date(value.dateRequest);
                if (dateRequest >= currentMonthStart && dateRequest <= currentMonthEnd) { // check if the date is within the current month
                    approvedCount++; // increment the count
                }
            }
            else if (value.status === 'Rejected') { // check if the status is rejected
                var dateRequest = new Date(value.dateRequest);
                if (dateRequest >= currentMonthStart && dateRequest <= currentMonthEnd) { // check if the date is within the current month
                    rejectedCount++; // increment the count
                }
            }
        });
        console.log('Count of Requested Overtime Status in This Month:', requestedCount); // log the count to the console
        console.log('Count of Approved Overtime Status in This Month:', approvedCount);
        console.log('Count of Rejected Overtime Status in This Month:', rejectedCount);

        $("#monthlyRequested").html(`<div class="text-xs font-weight-bold text-primary text-uppercase mb-1">Requested (Monthly)</div>
        <div class="h5 mb-0 font-weight-bold text-gray-800">${requestedCount}</div>`);
        $("#monthlyApproved").html(`<div class="text-xs font-weight-bold text-success text-uppercase mb-1">Approved (Monthly)</div>
        <div class="h5 mb-0 font-weight-bold text-gray-800">${approvedCount}</div>`);
        $("#monthlyRejected").html(`<div class="text-xs font-weight-bold text-warning text-uppercase mb-1">Rejected (Monthly)</div>
        <div class="h5 mb-0 font-weight-bold text-gray-800">${rejectedCount}</div>`);
    },
    error: function () {
        console.log('Error occurred while fetching data from API');
    }
});

// Chart for status overtime (monthly)
$.ajax({
    type: 'GET',
    contentType: "application/json",
    url: 'https://localhost:7166/api/Overtime/manager-guid/' + managerGuidClaim,
    dataType: 'json',
    success: function (data) {
        var monthlyCount = {
            'Requested': 0,
            'Approved': 0,
            'Rejected': 0
        };
        var currentDate = new Date();
        var currentMonthStart = new Date(currentDate.getFullYear(), currentDate.getMonth(), 1); // get the start of the current month
        var currentMonthEnd = new Date(currentDate.getFullYear(), currentDate.getMonth() + 1, 0); // get the end of the current month
        $.each(data.data, function (index, value) {
            if (value.status === 'Requested') { // check if the status is requested
                var dateRequest = new Date(value.dateRequest);
                if (dateRequest >= currentMonthStart && dateRequest <= currentMonthEnd) { // check if the date is within the current month
                    monthlyCount['Requested']++; // increment the count
                }
            }
            else if (value.status === 'Approved') { // check if the status is approved
                var dateRequest = new Date(value.dateRequest);
                if (dateRequest >= currentMonthStart && dateRequest <= currentMonthEnd) { // check if the date is within the current month
                    monthlyCount['Approved']++; // increment the count
                }
            }
            else if (value.status === 'Rejected') { // check if the status is rejected
                var dateRequest = new Date(value.dateRequest);
                if (dateRequest >= currentMonthStart && dateRequest <= currentMonthEnd) { // check if the date is within the current month
                    monthlyCount['Rejected']++; // increment the count
                }
            }
        });
        var ctx = document.getElementById('myPieChart').getContext('2d');
        var myPieChart = new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: ['Requested', 'Approved', 'Rejected'],
                datasets: [{
                    data: [monthlyCount['Requested'], monthlyCount['Approved'], monthlyCount['Rejected']],
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

// Chart for overtime request overview
$.ajax({
    type: 'GET',
    contentType: "application/json",
    url: 'https://localhost:7166/api/Overtime/manager-guid/' + managerGuidClaim,
    dataType: 'json',
    success: function (data) {
        var currentYear = new Date().getFullYear(); // get the current year
        var monthlyTotalRequest = new Array(12).fill(0); // initialize an array of size 12 with 0s
        $.each(data.data, function (index, value) {
            var dateRequest = new Date(value.dateRequest);
            if (dateRequest.getFullYear() === currentYear) { // check if the year of dateRequest is equal to the current year
                monthlyTotalRequest[dateRequest.getMonth()]++; // add the totalPay to the corresponding month
            }
        });
        console.log(monthlyTotalRequest);
        var ctx = document.getElementById('myAreaChart').getContext('2d');
        var myAreaChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                datasets: [{
                    label: 'Total Request : ',
                    data: monthlyTotalRequest,
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