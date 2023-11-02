$(document).ready(function () {
    // Employees Table
    $('#employeesTable').DataTable({
        ajax: {
            url: "https://localhost:7166/api/Employee/manager-guid/" + guidManager,
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
            { "data": "nik" },
            { "data": "gender" },
            { "data": "birthDate" },
            { "data": "hiringDate" },
            { "data": "email" },
            { "data": "phoneNumber" },
            { "data": "salary" }
        ]
    });
});