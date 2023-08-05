function getStates(country, state, city) {
    var countryId = $('#' + country).val();
    $('#StateId').attr('disabled', 'disabled');
    $('#CityId').attr('disabled', 'disabled');
    $('#' + state).html('');
    $('#' + city).html('');
    $('#' + state).append('<option value="">انتخاب کنید</option>');
    $('#' + city).append('<option value="">انتخاب کنید</option>');
    if (countryId === null || countryId === '') {
        $('#' + state).removeAttr('disabled');
        $('#' + city).removeAttr('disabled');
    }
    else {
        $.ajax({
            type: 'Post',
            url: '/Address/ReadAllStatesByCountryIdForSelect',
            data: { id: countryId },
            success: function (data) {
                data.forEach(function (item) {
                    $('#' + state).append('<option value="' + item.Value + '">' + item.Text + '</option>');
                });
                $('#' + state).removeAttr('disabled');
                $('#' + city).removeAttr('disabled');
            }
        });
    }
}

function getCities(state, city) {
    var stateId = $('#' + state).val();
    $('#' + city).attr('disabled', 'disabled');
    $('#' + city).html('');
    $('#' + city).append('<option value="">انتخاب کنید</option>');
    if (stateId === null || stateId === '') {
        $('#' + city).removeAttr('disabled');
    }
    else {
        $.ajax({
            type: 'Post',
            url: '/Address/ReadAllCitiesByStateIdForSelect',
            data: { id: stateId },
            success: function (data) {
                data.forEach(function (item) {
                    $('#' + city).append('<option value="' + item.Value + '">' + item.Text + '</option>');
                });
                $('#' + city).removeAttr('disabled');
            }
        });
    }
}