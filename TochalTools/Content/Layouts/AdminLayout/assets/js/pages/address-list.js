function deleteCountry(address, title) {
    swal({
        title: 'آیا از حذف ' + title + ' مطمئن هستید؟',
        text: title + ' حذف خواهد شد و قابل بازگردانی نخواهد بود.',
        icon: 'warning',
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'Post',
                url: '/Address/DeleteCountry',
                data: { id: address },
                success: function (data) {
                    if (data === true) {
                        swal(title + ' با موفقیت حذف شد!', {
                            icon: 'success'
                        });
                        $('#country-' + address).fadeOut(1000);
                    }
                    else {
                        swal(title + ' حذف نشد!');
                    }
                }
            });
        }
    });
}

function deleteState(address, title) {
    swal({
        title: 'آیا از حذف ' + title + ' مطمئن هستید؟',
        text: title + ' حذف خواهد شد و قابل بازگردانی نخواهد بود.',
        icon: 'warning',
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'Post',
                url: '/Address/DeleteState',
                data: { id: address },
                success: function (data) {
                    if (data === true) {
                        swal(title + ' با موفقیت حذف شد!', {
                            icon: 'success'
                        });
                        $('#state-' + address).fadeOut(1000);
                    }
                    else {
                        swal(title + ' حذف نشد!');
                    }
                }
            });
        }
    });
}

function deleteCity(address, title) {
    swal({
        title: 'آیا از حذف ' + title + ' مطمئن هستید؟',
        text: title + ' حذف خواهد شد و قابل بازگردانی نخواهد بود.',
        icon: 'warning',
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'Post',
                url: '/Address/DeleteCity',
                data: { id: address },
                success: function (data) {
                    if (data === true) {
                        swal(title + ' با موفقیت حذف شد!', {
                            icon: 'success'
                        });
                        $('#city-' + address).fadeOut(1000);
                    }
                    else {
                        swal(title + ' حذف نشد!');
                    }
                }
            });
        }
    });
}