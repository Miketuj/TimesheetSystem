(function ($) {


    submitTimeForm();

    downloadCSV();

    $('.ui.dropdown').dropdown();
    $("#datepicker").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy",
        maxDate: 0
    });
    clearForm()
}(jQuery));

function submitTimeForm() {
    $("#submit").off('click').on("click", function () {

        $('.form').addClass('loading');
        checkFormFields()

        if ($('.error').length == 0) {
            var entry = {
                UserID: $('.dropdown').find('.menu').find('.item.selected').attr('id'),
                Date: $("#datepicker").val(),
                Project: $('#project').val(),
                HoursWorked: $('#hoursworked').val(),
                Description: $('#description').val(),
            }

            $.ajax({
                url: '/entry/add',
                type: 'POST',
                data: entry,
                success: function (response) {
                    $('[data-formfeedback]').html(response.statusMessage)
                    $('.form').form('clear')
                    removeLoading()
                    
                },
                error: function (xhr, status, error) {
                    $('[data-formfeedback]').html("There was an error adding your entry")
                    removeLoading()
                }
            });
        } else {
            removeLoading()
        }

    });
}
function clearForm() {
    $("#clear").off('click').on("click", function () {
        $('.form').form('clear')
    });
}
function removeLoading() {
    $('.form').removeClass('loading');
}
function addFieldErrors(field, message) {
    field.addClass('error')
    field.find('span').html(message)
}
function removeFieldErrors(field) {
    field.removeClass('error')
    field.find('span').html('')
}
function checkFormFields() {
    var inputs = $('[data-check]');

    inputs.each(function () {
        var field = $(this).closest('.field');
        if ($(this).val() == undefined || $(this).val() == "") {
            addFieldErrors(field, 'Field required')
        } else {
            removeFieldErrors(field)
        }

    })
    var hoursField = $('#hoursworked').closest('.field');
    var hoursWorked = parseInt($('#hoursworked').val())
    if (!$.isNumeric(hoursWorked)) {
        addFieldErrors(hoursField, 'Must be a number')
    } else {
        removeFieldErrors(hoursField)
    }

}
function downloadCSV() {
    $("#download").off('click').on("click", function (e) {
        e.preventDefault();
        var url = '/download/csv';

        var link = document.createElement('a');
        link.href = url;
        link.download = 'timesheets.csv';
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    });
}