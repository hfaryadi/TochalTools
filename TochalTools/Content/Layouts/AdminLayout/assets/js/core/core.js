$(function(){
    var current = location.pathname;
    $('ul.sidebar-menu > li a').each(function(){
        var $this = $(this);
        //if($this.attr('href').indexOf(current) !== -1){
        if ($this.attr('href') === current) {
            try {
                if ($this.parent().parent().parent().parent().parent()[0].nodeName.toLowerCase() === 'li') {
                    $this.parent().parent().parent().parent().parent().addClass('active');
                }
            }
            catch (e) {/*nothing*/}
            try{
                if ($this.parent().parent().parent()[0].nodeName.toLowerCase() === 'li')
                {
                    $this.parent().parent().parent().addClass('active');
                }
            }
            catch (e) {/*nothing*/}
            try{
                if ($this.parent()[0].nodeName.toLowerCase() === 'li')
                {
                    $this.parent().addClass('active');
                }
            }
            catch (e) {/*nothing*/}
            $this.addClass('active');
        }
    });
});

function intFormat(number) {
    var regex = /(\d)((\d{3},?)+)$/;
    number = number.split(',').join('');
    while (regex.test(number)) {
        number = number.replace(regex, '$1,$2');
    }
    return number;
}

function numFormat(number) {
    var pointReg = /([\d,\.]*)\.(\d*)$/, f;
    if (pointReg.test(number)) {
        f = RegExp.$2;
        return intFormat(RegExp.$1) + '.' + f;
    }
    return intFormat(number);
}

function comma() {
    $('input[maxlength=18]').val(function (index, value) {
        return value.replace(/,/g, '');
    });
}