$(function () {


    $('.port .add-port button.add-images').on('click', function () {

        $(this).siblings('input').trigger('click');

    });

    $('.port-images').on('change', function () {

        imagesPreview(this, $('.selected-photos'));
    })


    // Adding portfolio
    $('.send-port').on('click', function () {

        let portname = $('.pname').val();
        let dataToSend = new FormData();
        let url = $('.save-portfolio').attr('data-url');
        let porttype;

        if ($('#veb').prop('checked')) {
            porttype = $('#veb').val();
        }
        else {
            porttype = $('#qrafik').val()
        }

        if (!$('#veb').prop('checked') && !$('#qrafik').prop('checked')) {
            $('.ptype-error').show();
        }
        else {
            $('.ptype-error').hide();

            if (checkEmpty(portname, $('.pname-error'), 60)) {

                dataToSend.append('name', portname);
                dataToSend.append('about', $('.pabout').val());
                dataToSend.append('github', $('.pgithub').val());
                dataToSend.append('behance', $('.pbehance').val());
                dataToSend.append('website', $('.pwebsite').val());
                dataToSend.append('portType', porttype);

                for (var file of $('.port-images')[0].files) {

                    dataToSend.append('files', file);
                }

                $.ajax({
                    type: "POST",
                    url: url,
                    processData: false,
                    contentType: false,
                    data: dataToSend,
                    dataType: "json",
                    success: function (result) {

                        if (result.error != null) {
                            alert(result.error);
                        }

                        addPortToBody(result.id, result.responseImg)
                    }
                });
            }
        }

    });

    function addPortToBody(id,img) {

        let src = $('.port-list').attr('data-src');

        let port = ' <div class="portfolio" id="'+id+'"> ' +
                    ' <img src="'+ src + img +'" alt=""> ' +
                    ' <h5> @item.Name </h5> ' +
                    ' </div> ';
        $('.port-list').prepend(port);

        $('.selected-photos').empty();
        $('.pname').val('');
        $('.pabout').val('');
        $('.pgithub').val('');
        $('.pbehance').val('');
        $('.pwebsite').val('');

    }





















    // Multiple images preview in browser
    var imagesPreview = function(input, placeToInsertImagePreview) {
    
        if (input.files) {
            var filesAmount = input.files.length;
    
            for (i = 0; i < filesAmount; i++) {
                var reader = new FileReader();
    
                reader.onload = function(event) {
                    $($.parseHTML('<img>')).attr('src', event.target.result).appendTo(placeToInsertImagePreview);
                }
    
                reader.readAsDataURL(input.files[i]);
            }
        }
    
    }


    // Function for sending ajax requests
function sendAjax(url,data){
    $.ajax({
        type: "POST",
        url: url,
        processData: false,
        contentType: false,
        data: data,
        dataType: "json",
        success: function(result){
                
                if (result.error != null) {
                    alert(result.error);
            }
            console.log(result)
            }
        });
    }

    // Function checking empty inputs
    function checkEmpty(element, error, length) {
        if (length == null) {
            if (element == '') {
                error.show();
                return false;
            }
            else {
                error.hide();
                return true;
            }
        }
        else {
            if (element.length > length || element == '') {
                error.show();
                return false;
            }
            else {
                error.hide();
                return true;
            }
        }
    }







})