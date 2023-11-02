$(document).ready(function () {
    var tableRequest = $('#requestTable').DataTable({
        ajax: {
            url: "https://localhost:7166/api/Overtime/manager-guid/" + managerGuidClaim,
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
            {
                "data": "dateRequest",
                "render": function (data, type, row) {
                    var date = new Date(data);
                    var options = { day: 'numeric', month: 'short', year: 'numeric' };
                    return date.toLocaleDateString('en-US', options);
                }
            },
            { "data": "duration" },
            { "data": "typeOfDay" },
            {
                "data": "status",
                "visible": false
            },
            {
                "data": null,
                render: function (data, type, row) {
                    return `<button class="acceptBtn text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" type="button">
                              <i class="fa-solid fa-check-circle"></i>
                            </button>
                            <button class="rejectBtn text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" type="button">
                              <i class="fa-solid fa-circle-xmark"></i>
                            </button>
                            `;
                }
            }
        ]
    });
    tableRequest.columns(4).search('Requested').draw();

    $('#requestTable').on('click', '.acceptBtn', function () {
        let obj = new Object();
        obj.guid = $('#requestTable').DataTable().row($(this).parents('tr')).data().guid;
        obj.approvalStatus = 1;
        obj.approvedBy = managerName;
        obj.remarks = "";
        console.log(obj);
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "https://localhost:7166/api/Approval",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (result) {
                console.log(result);
                Swal.fire(
                    'Yeay!',
                    'You Accept the Request!',
                    'success'
                )
                tableRequest.ajax.reload();
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
                alert("Error while inserting data!");
            }
        });
    });

    $('#requestTable').on('click', '.rejectBtn', function () {
        let obj = new Object();
        obj.guid = $('#requestTable').DataTable().row($(this).parents('tr')).data().guid;
        obj.approvalStatus = 0;
        obj.approvedBy = managerName;
        obj.remarks = "";
        console.log(obj);
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "https://localhost:7166/api/Approval",
            data: JSON.stringify(obj),
            dataType: "json",
            success: function (result) {
                console.log(result);
                Swal.fire(
                    'Yeay!',
                    'Success to Reject the Request',
                    'success'
                )
                tableRequest.ajax.reload();
            },
            error: function (xhr, status, error) {
                console.log(xhr.responseText);
                alert("Error while inserting data!");
            }
        });
    });
});