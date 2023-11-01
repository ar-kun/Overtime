$(document).ready(function () {
    // Ketika click submit edit
    $('#editSubmitPayroll').off('click').on('click', function () {
        Swal.fire({
            title: 'Do you want to save the changes?',
            showDenyButton: true,
            showCancelButton: true,
            confirmButtonText: 'Save',
            denyButtonText: `Don't save`,
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                let editedData = {
                    firstName: $('#editOvertimePay').val(),
                    lastName: $('#editPaymentStatus').val(),
                    birthDate: $('#editBirthDate').val(),
                    hiringDate: $('#editHiringDate').val(),
                    gender: parseInt($('#editGender').val()),
                    email: $('#editEmail').val(),
                    phoneNumber: $('#editPhoneNumber').val(),
                    guid: data.guid, // GUID employee yang akan diubah
                    nik: $('#editNik').val()
                };

                // Send request ke API
                $.ajax({
                    url: 'https://localhost:7290/api/Employee',
                    method: 'PUT',
                    contentType: 'application/json',
                    data: JSON.stringify(editedData)
                }).done((result) => {
                    // Hide Modal
                    $("#editEmployeeModal").modal('hide');

                    // Refresh table
                    $('#employee-table').DataTable().ajax.reload();

                    // Success Alert
                    Swal.fire({
                        icon: 'success',
                        title: result.message,
                        showConfirmButton: false,
                        timer: 1300
                    });
                    //Swal.fire('Saved!', result.message, 'success');
                    //$("#successAlert").text(result.message).show();
                }).fail((jqXHR, textStatus, errorThrown) => {
                    let alertMsg = jqXHR.responseJSON.message;

                    // Fail Alert
                    Swal.fire('Employee data failed to update', alertMsg, 'warning');
                    //$("#edit-fail").text(alertMsg).show();
                });
            } else if (result.isDenied) {
                Swal.fire('Changes are not saved', '', 'info')
            }
        })

        return false;
    });
});