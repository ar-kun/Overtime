$(document).ready(function () {
    // Payroll Table
    $('#myOvertimeTable').DataTable({
        ajax: {
            url: "https://localhost:7166/api/Overtime/employee-guid/" + guidEmployee,
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
            },
            { "data": "remarks" },
            {
                "data": null,
                render: function (data, type, row) {
                    return `<button type="button" class="deleteOvertime focus:outline-none text-white bg-red-700 hover:bg-red-800 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-900">
                             <i class="fa-solid fa-trash"></i>
                            </button>`;
                }
            }
        ]
    });

    $('#myOvertimeTable').on('click', '.deleteOvertime', function () {
        // Ambil ID yang tersimpan dalam atribut data-id
        var data = $('#myOvertimeTable').DataTable().row($(this).parents('tr')).data();
        var guid = data.guid;
        var status = String(data.status);

        // Can't delete data
        if (status != "Requested") {
            Swal.fire(
                'Oops',
                "Only 'Requested' overtime can be deleted",
                'error'
            )
        } else {
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    // send DELETE req with 'guid' to API
                    $.ajax({
                        url: `https://localhost:7166/api/Overtime/${guid}`,
                        type: 'DELETE'
                    }).done((result) => {

                        // Refresh table
                        $('#myOvertimeTable').DataTable().ajax.reload();

                        // Success Alert
                        Swal.fire({
                            icon: 'success',
                            title: result.message,
                            showConfirmButton: false,
                            timer: 1300
                        });
                    }).fail((jqXHR, textStatus, errorThrown) => {
                        // Fail Alert
                        Swal.fire(
                            'Failed!',
                            jqXHR.responseJSON.message,
                            'error'
                        );
                    });
                }
            })
        }
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
        return typeOfDay; // Mengembalikan nilainya tanpa perubahan
    }
}