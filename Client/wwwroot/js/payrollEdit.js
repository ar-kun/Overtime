$(document).ready(function () {
    // Ketika click submit edit
    $('#editSubmitPayroll').off('click').on('click', function () {
        Swal.fire({
            title: 'Do you want to save the changes?',
            showDenyButton: true,
            confirmButtonText: 'Save',
            denyButtonText: `Don't save`,
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                let editedData = {
                    guid: $('#editPaymentGuid').val(),
                    totalPay: parseInt($('#editOvertimePay').val()),
                    paymentStatus: parseInt($('#editPaymentStatus').val())
                };

                // Send request ke API
                $.ajax({
                    url: 'https://localhost:7166/api/PaymentDetail',
                    method: 'PUT',
                    contentType: 'application/json',
                    data: JSON.stringify(editedData)
                }).done((result) => {
                    // Save the success message in localStorage
                    localStorage.setItem("successMessage", "Data Updated Successfully");

                    // Redirect to Index page
                    window.location.href = '/payroll/all';
                }).fail((jqXHR, textStatus, errorThrown) => {
                    let alertMsg = jqXHR.responseJSON.message;

                    // Fail Alert
                    Swal.fire('Payroll data failed to update', alertMsg, 'warning');
                });
            } else if (result.isDenied) {
                Swal.fire('Changes are not saved', '', 'info')
            }
        })

        return false;
    });
});