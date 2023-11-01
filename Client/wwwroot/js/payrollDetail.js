$(document).ready(function () {
    // Check if a success message exists in localStorage
    const successMessage = localStorage.getItem("successMessage");

    if (successMessage) {
        // Display the success message
        $("#successAlert").removeClass("hidden");
        // Clear the success message from localStorage
        localStorage.removeItem("successMessage");
    }

    // Payroll Table
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
            { "data": "overtimeDate" },
            { "data": "duration" },
            { "data": "typeOfDay" },
            { "data": "totalPay" },
            { "data": "paymentStatus" },
            {
                "data": null,
                render: function (data, type, row) {
                    return `<button class="editBtn text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" type="button">
                              <i class="fa-solid fa-pen-to-square"></i>
                            </button>
                            <button class="detailBtn text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" type="button">
                              <i class="fa-solid fa-circle-info"></i>
                            </button>`;
                }
            }
        ]
    });

    $('#payrollTable').on('click', '.editBtn', function () {
        // Ambil ID yang tersimpan dalam atribut data-id
        var guid = $('#payrollTable').DataTable().row($(this).parents('tr')).data().guid;

        // Redirect ke halaman Details dengan ID sebagai parameter
        window.location.href = '/Payroll/Edit?guid=' + guid;
    });

    $('#payrollTable').on('click', '.detailBtn', function () {
        // Ambil ID yang tersimpan dalam atribut data-id
        var guid = $('#payrollTable').DataTable().row($(this).parents('tr')).data().guid;

        // Redirect ke halaman Details dengan ID sebagai parameter
        window.location.href = '/Payroll/Details?guid=' + guid;
    });
});

    