$(function () {
    var current = location.pathname;
    $('ul.navbar-nav > li a').each(function () {
        var $this = $(this);
        if ($this.attr('href') === current) {
            try {
                if ($this.parent().parent().parent().parent().parent()[0].nodeName.toLowerCase() === 'li') {
                    $this.parent().parent().parent().parent().parent().addClass('active');
                }
            }
            catch (e) {/*nothing*/}
            try {
                if ($this.parent().parent().parent()[0].nodeName.toLowerCase() === 'li') {
                    $this.parent().parent().parent().addClass('active');
                }
            }
            catch (e) {/*nothing*/}
            try {
                if ($this.parent()[0].nodeName.toLowerCase() === 'li') {
                    $this.parent().addClass('active');
                }
            }
            catch (e) {/*nothing*/}
        }
    });
});

function openLink(link) {
    var w = window.innerWidth;
    var h = window.innerHeight;
    if (w > 480)
        window.location.href = link;
}