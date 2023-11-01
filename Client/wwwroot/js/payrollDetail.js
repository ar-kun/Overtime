$('#payrollTable').DataTable({
    ajax: {
        url: "payroll/GetPayrollList/",
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
        { "data": "nik" },
        { "data": "fullName" },
        {
            "data": "overtimeDate",
            render: function (data, type, row) {
                return `${formatDate(row.overtimeDate)}`;
            }
        },
        { "data": "duration" },
        {
            "data": "typeOfDay",
            render: function (data, type, row) {
                return `${formatDayString(row.typeOfDay)}`;
            }
        },
        {
            "data": "totalPay",
            render: function (data, type, row) {
                return `${formatCurrency(row.totalPay)}`;
            }
        },
        { "data": "paymentStatus" },
        {
            "data": null,
            render: function (data, type, row) {
                return `<button class="btn btn-blue" id="detailBtn">
                          Detail
                        </button>`;
            }
        }
    ]
});

$('#payrollTable').on('click', '#detailBtn', function () {
    // Ambil ID yang tersimpan dalam atribut data-id
    var guid = $('#payrollTable').DataTable().row($(this).parents('tr')).data().guid;

    // Redirect ke halaman Details dengan ID sebagai parameter
    window.location.href = '/Payroll/Details?guid=' + guid;
});

// Convert format date
function formatDate(inputDate) {
    const options = { year: 'numeric', month: 'short', day: 'numeric' };
    const date = new Date(inputDate);
    return date.toLocaleDateString('en-GB', options);
}

// Convert int to IDR Currency
function formatCurrency(value) {
    const formattedNumber = new Intl.NumberFormat('id-ID', {
        style: 'currency',
        currency: 'IDR'
    }).format(value);

    return formattedNumber;
}

// Seperate string
function formatDayString(dayString) {
    return dayString.replace(/([a-z])([A-Z])/g, '$1 $2');
}