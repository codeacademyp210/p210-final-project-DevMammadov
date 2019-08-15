$(function(){


    $('.settings-info .edit').on('click',editUserInfo)
    $('.settings-info .save').on('click',saveUserInfo)
    $('.settings-security .edit').on('click',editSecurity)
    $('.settings-security .save').on('click',saveSecurity)
    $('.settings-connect .edit').on('click', editConnect)
    $('.settings-connect .save').on('click', saveConnect)


    // Edit user connections
    function editConnect(e) {
        e.preventDefault()

        let spanNum = $(this).parents('li').find('.span-number');
        let inputNum = $(this).parents('li').find('.user-phone');

        inputNum.val(spanNum.html().trim());

        $(this).parents('li').toggleClass('editing');
    }

    // Save user Connection
    function saveConnect(e){
        e.preventDefault();

        let url = $('.edit-user-info-url').attr('data-url');
        let dataToSend = new FormData();

        dataToSend.append('phone', $('.user-phone').val());
        dataToSend.append('address', $('.user-address').val());
        dataToSend.append('lin', $('.user-linkedin').val());
        dataToSend.append('be', $('.user-behance').val());
        dataToSend.append('fb', $('.user-facebook').val());
        dataToSend.append('gb', $('.user-github').val());

       sendAjax(url, dataToSend);
       $('.span-number').text($('.user-phone').val());
        $('.span-address').text($('.user-address').val());

        $.each($('.settings-content').find('input'), (i, v) => {

            if ($(v).val() != '') {

                $(v).parents('span').siblings('.key').find('.hidden-check').css({'display': 'inline-block'});
            }
            else {
                $(v).parents('span').siblings('.key').find('.hidden-check').hide();
            }
        });
        $(this).parents('li').removeClass('editing');
    }


    // Edit user info
   function editUserInfo(e){
        e.preventDefault()

        let spanNAme = $(this).parents('li').find('.span-name');
        let spanSurname = $(this).parents('li').find('.span-surname');
        let inputName = $(this).parents('li').find('.user-name');
        let inputSurname = $(this).parents('li').find('.user-surname');

        inputName.val(spanNAme.html().trim());
        inputSurname.val(spanSurname.html().trim());

        $(this).parents('li').toggleClass('editing');

    };
  

    // Save user info
    function saveUserInfo(e){
        e.preventDefault();
        
        let spanNAme = $(this).parents('li').find('.span-name');
        let spanSurname = $(this).parents('li').find('.span-surname');
        let inputName = $(this).parents('li').find('.user-name');
        let inputSurname = $(this).parents('li').find('.user-surname');

        let url = $('.edit-user-info-url').attr('data-url');
        let dataToSent = new FormData();

        if(checkEmpty(inputName,60) && checkEmpty(inputSurname,60)){
            
            dataToSent.append('name',inputName.val());
            dataToSent.append('surname', inputSurname.val());

            sendAjax(url,dataToSent);

            spanNAme.text(inputName.val().trim());
            spanSurname.text(inputSurname.val().trim());
            $(this).parents('li').removeClass('editing');
        }
    }



    //////////////////// Security

    function editSecurity(e) {
        e.preventDefault();

        $(this).parents('li').toggleClass('editing');
    }

    function saveSecurity(e){
        e.preventDefault();

        let securityUrl = $(".edit-security-url").attr("data-url");
        let oldpass = $(this).parents('li').find('.old-password');
        let newpass = $(this).parents('li').find('.new-password');
        let dataToSent = new FormData();
        dataToSent.append('oldpass', oldpass);
        dataToSent.append('newpass', newpass);

        if(checkEmpty(oldpass) && checkEmpty(newpass)){

            $.ajax({
                type: "POST",
                url: securityUrl,
                data: dataToSent,
                dataType: "json",
                processData: false,
                contentType: false,
                success: function (response,success) {

                    console.log(response);

                }
            });
             //   $('.password-error').show();
            
        }

    }

     // ---------- Settings profile

    // Profile switch button
    $('.butt-msg').on('click',function(){

        let url = $('.edit-profile-url').attr('data-url');
        let dataToSent = new FormData();

        if( !$(this).siblings('input').prop('checked') ){
            
            dataToSent.append('button',1);
        }
        else{
            dataToSent.append('button',0);
        }

         sendAjax(url,dataToSent);

    });

    //profile color opener
    $('.settings .current-color').on('click',function(){

        $(this).siblings('.other-color').toggleClass('opened');
    });

    // selecting color
    $('.other-color div').on('click',function(){

        let color = $(this).css('background-color');
        let url = $('.edit-profile-url').attr('data-url');

        let dataToSent = new FormData();
        dataToSent.append('color',color);

        sendAjax(url,dataToSent);
        $('.current-color').css({'background':color});
        $(this).parent().removeClass('opened');
    });


    // Settings blocked list
    $('.settings-block-list .blocked-user a').on('click',function(e){
        e.preventDefault();

        let id = $(this).attr('id');
        let url = $('.edit-blocklist-url').attr('data-url');
        let dataToSent = new FormData();
        
        dataToSent.append('id',id);

        if(confirm("Bu istifadəçini blokdan çıxarmaq istəyirsinizmi?")){

            sendAjax(url,dataToSent);
            $(this).parent().remove();
        }

    });

});



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

        }
    });
}


    // Function checking empty inputs
 function checkEmpty(element,length){
    if(length == null){
        if(element.val() == ''){
            element.css({'border':'1px solid #CD5527'})
            return false;
        }
        else{
            element.css({'border':'0'})
            return true;
        }
    }
    else{
        if(element.val().length > length || element == ''){
            element.css({'border':'1px solid #CD5527'})
            return false;
        }
        else{
            element.css({'border':'0'})
            return true;
        }
    }
}
