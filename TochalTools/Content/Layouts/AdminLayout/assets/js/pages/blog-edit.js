$(document).ready(function () {
    getFromInput('#Categories', '#mCategories');
    getFromInput('#Tags', '#mTags');
    $("form").validate({
        ignore: [],
        rules: {
            Categories: {
                required: true
            },
            Title: {
                required: true
            },
            Language: {
                required: true
            },
            ShortContent: {
                required: function (textarea) {
                    CKEDITOR.instances[textarea.id].updateElement();
                    var editorcontent = textarea.value.replace(/<[^>]*>/gi, '');
                    return editorcontent.length === 0;
                }
            },
            FullContent: {
                required: function (textarea) {
                    CKEDITOR.instances[textarea.id].updateElement();
                    var editorcontent = textarea.value.replace(/<[^>]*>/gi, '');
                    return editorcontent.length === 0;
                }
            }
        }, messages: {
            Categories: {
                required: "لطفا دسته بندی را انتخاب نمایید."
            },
            Title: {
                required: "لطفا عنوان مطلب را وارد نمایید."
            },
            Language: {
                required: "لطفا زبان را انتخاب نمایید."
            },
            ShortContent: {
                required: "لطفا متن کوتاه را وارد نمایید."
            },
            FullContent: {
                required: "لطفا متن کامل را وارد نمایید."
            }
        }
    });
});
function beforeSubmit() {
    addToInput('#Categories', '#mCategories');
    addToInput('#Tags', '#mTags');
}