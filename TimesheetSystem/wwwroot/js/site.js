(function ($) {


    submitTimeForm();

    downloadCSV();

    $('.ui.dropdown').dropdown();

}(jQuery));

function submitTimeForm() {
    $("#submit").off('click').on("click", function () {
        checkFormFields() 

        if ($('.error').length == 0) {
            var entry = {
                UserID: $('.dropdown').find('.menu').find('.item.selected').attr('id'),
                Date: Date.now,
                Project: $('#project').val(),
                HoursWorked: $('#hoursworked').val(),
                Description: $('#description').val(),
            }

            $.ajax({
                url: '/entry/add',
                type: 'POST',
                data: entry,
                success: function (response) {
                    console.log(response)
                    //Clear form
                    //Show message to say uploaded successfully
                },
                error: function (xhr, status, error) {
                    alert("An error occurred: " + error);
                }
            });
        } else {
            //do nothing
        }
    });
}
function checkFormFields() {
    var inputs = $('[data-check]');

    inputs.each(function () {
        var field = $(this).closest('.field');
        if ($(this).val() == undefined || $(this).val() == "") {
           
            field.addClass('error')
            field.find('span').html('Field required')
        } else {
            field.removeClass('error')
            field.find('span').html('')
        }
       
    })
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