$(document).ready(function () {
    // Payroll Table
    $('#overtimeSchedule').DataTable({
        ajax: {
            url: "https://localhost:7166/api/Overtime/manager-guid/" + guidManager,
            dataSrc: "data",
            dataType: "JSON"
        },
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, ':visible'],
                },
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':visible',
                },
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 5],
                },
            },
            'colvis',
        ],
        columns: [
            { "data": "fullName" },
            { "data": "email" },
            {
                "data": "dateRequest",
                render: function (data, type, row) {
                    return `${formatDate(row.dateRequest)}`;
                }
            },
            {
                "data": "duration",
                render: function (data, type, row) {
                    return `${row.duration} hours`;
                }
            },
            { "data": "status" },
            {
                "data": "typeOfDay",
                render: function (data, type, row) {
                    return `${formatTypeOfDay(row.typeOfDay)}`;
                }
            }
        ]
    });
});

// Convert format date
function formatDate(inputDate) {
    const options = { year: 'numeric', month: 'short', day: 'numeric' };
    const date = new Date(inputDate);
    return date.toLocaleDateString('en-GB', options);
}

// Convert Type of Day String
function formatTypeOfDay(typeOfDay) {
    if (typeOfDay === "WeekDay") {
        return "Week Day";
    } else if (typeOfDay === "OffDay") {
        return "Off Day";
    } else {
        return typeOfDay;
    }
}