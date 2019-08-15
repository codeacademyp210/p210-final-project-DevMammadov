$(function () {

    // open profile picture uploader
    $(".upload-profile-img a").click(function (e) {
        e.preventDefault();
        $(this).siblings("input").trigger("click");
    });

    // open cover picture uploader
    $(".upload-cover-img a").click(function (e) {
        e.preventDefault();
        $(this).siblings("input").trigger("click");
    });

    // Add form drop down opener [ FOR ALL FORMS ]
    $(".add-form").click(function (e) {
        e.preventDefault();
        $(this).parents(".main-section-header").siblings(".dropdown").toggleClass("opened");
    });

    // ============ Edit cover picture ============= //

    $('.cover-img').change(function () {

        let url = $('.change-cover').attr('data-url');

        var file = $(this)[0].files[0];

        let dataToSend = new FormData();
        dataToSend.append("file", file);

        $.ajax({
            type: "POST",
            url: url,
            data: dataToSend,
            contentType: "JSON",
            processData: false,
            contentType: false,
            dataType: "json",
            success: function (result, status) {
                if (result.error != null) {
                    alert(result.error)
                }
                else {
                    $('.cover').css("background-image", "url('../../Content/images/Covers/" + result.response + "')");
                }
            }
        });
    });

    // ============ Edit profile picture ============= //


    $('.profile-img').change(function () {

        let url = $('.change-photo').attr('data-url');

        var file = $(this)[0].files[0];

        let dataToSend = new FormData();
        dataToSend.append("file", file);

        $.ajax({
            type: "POST",
            url: url,
            data: dataToSend,
            contentType: "JSON",
            processData: false,
            contentType: false,
            dataType: "json",
            success: function (result, status) {
                if (result.error != null) {
                    alert(result.error)
                }
                else {
                    $('.profile-picture img').attr("src", "../../Content/images/Users/" + result.response);
                }
            }
        });
    });


    // ================================ Experience section ==================================


    // Function loads image from input to img src
    function readURL(input, img) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                img.attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }


    // Send ajax 
    // Function for sending ajax requests
    function sendAjax(url, data) {

        $.ajax({
            type: "POST",
            url: url,
            data: data,
            contentType: "JSON",
            processData: false,
            contentType: false,
            dataType: "json",
            success: function (result, status) {

                console.log(result)
            }
        });
    }

});