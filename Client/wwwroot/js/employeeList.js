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

    // Create Employee Account
    $("#submitEmployee").click(function () {
        submitForm();
        return false;
    });
});

// Function to handle form add employee
function submitForm() {
    let emp = {};
    emp.firstName = $("#firstName").val();
    emp.lastName = $("#lastName").val();
    emp.gender = parseInt($("#gender").val());
    emp.email = $("#email").val();
    emp.birthDate = $("#birthDate").val();
    emp.hiringDate = $("#hiringDate").val();
    emp.phoneNumber = $("#phoneNumber").val();
    emp.salary = $("#salary").val();
    emp.password = $("#password").val();
    emp.managerNik = $("#managerNik").val();
    let jsonString = JSON.stringify(emp);
    console.log(jsonString);

    $.ajax({
        url: "https://localhost:7290/api/Employee",
        type: "POST",
        cache: false,
        data: jsonString,
        contentType: "application/json"
    }).done((result) => {
        // Hide Modal
        $("#employee-modal").modal('hide');

        // Refresh table
        $('#employee-table').DataTable().ajax.reload();

        // Success Alert
        Swal.fire({
            icon: 'success',
            title: result.message,
            showConfirmButton: false,
            timer: 1300
        });

        //$("#successAlert").text(result.message).show();
    }).fail((jqXHR, textStatus, errorThrown) => {
        let alertMsg = jqXHR.responseJSON.message;

        // Fail Alert
        Swal.fire('Failed to create', alertMsg, 'warning');
        //$("#add-fail").text(alertMsg).show();
    });
}