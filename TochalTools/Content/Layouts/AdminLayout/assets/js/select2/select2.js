function addToInput(orgIntutId, fakeInputId) {
    var Values = '';
    var items = $(fakeInputId).val();
    items.forEach(function (item) {
        Values = Values + item + ',';
    });
    if (Values !== '') {
        Values = Values.slice(0, -1);
    }
    $(orgIntutId).val(Values);
}

function getFromInput(orgIntutId, fakeInputId) {
    var Values = $(orgIntutId).val();
    if (Values !== null && Values !== '') {
        try {
            var items = Values.split(',');
            $(fakeInputId).val(items);
        }
        catch (e) {
            $(fakeInputId).val(Values);
        }
    }
}