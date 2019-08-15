$(function(){

    // taking url and src adresses
    let cvImgUpload = $('.cv-img-url').attr('data-url');
    let editCv = $('.cv-edit-src').attr('data-url');

    // open cv photo uploader
    $('.cv-img-uploader').on('click',function(e){
        e.preventDefault();

        $(this).siblings().trigger('click');
    });



    // saving image
    $('.upload-cv-img input').change(function (e) { 
        e.preventDefault();
        
        let img = $(this)[0].files[0];
        let dataToSend = new FormData();
        dataToSend.append('file', img);

        sendAjax(cvImgUpload, dataToSend);
            
        readURL(this,$('.cv-img img'));
        
    });



    // Changing cv color
    $('.cv-colors div').on('click',function(){

        let cvColor = $(this).attr('id');
        let allColors = $('.cv-colors div');
        let dataToSend = new FormData();

        $.each(allColors, function (indexInArray, valueOfElement) { 

             $('.cv-container .cv').removeClass(valueOfElement.id)
        });
        
        $('.cv-container .cv').addClass(cvColor);
        dataToSend.append('color', cvColor);
        sendAjax(editCv, dataToSend);
    });




    // changing cv forms
    $('.edit-cv .cv-forms >div').on('click',function(){

        let cvForm = $(this).attr('id');
        let allForms = $('.edit-cv div');
        let dataToSend = new FormData();

        $.each(allForms, function (indexInArray, valueOfElement) { 

             $('.cv-container .cv').removeClass(valueOfElement.id)
        });
        
        $('.cv-container .cv').addClass(cvForm);
        dataToSend.append('type', cvForm);
        sendAjax(editCv, dataToSend);
    })










    // Function loads image from input to img src
function readURL(input,img) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            img.attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
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
            }
        });
    }



});