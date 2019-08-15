$(function () {

    $(document).on('click', '.about-section .user-position .edit', EditPosition);
    $(document).on('click', '.about-section .user-position .save', SavePosition);
    $(document).on('click', '.about-section .body-header .edit', EditAbout);
    $(document).on('click', '.about-section .body-header .save', SaveAbout);
    $(document).on('click', '.social-acts .body-header .remove', RemoveAct);
    $(document).on('click', '.social-acts .about-aside > div > div:first-child .edit', EditCompanyPosition);
    $(document).on('click', '.social-acts .about-aside > div > div:first-child .save', SaveCompanyPosition);
    $(document).on('click', '.social-acts .about-aside > div > div + div .edit', EditCompanyPosition);
    $(document).on('click', '.social-acts .about-aside > div > div + div .save', SaveCompanyPosition);
    $(document).on('click', '.univers .about-aside .edit', EditProfession);
    $(document).on('click', '.univers .about-aside .save', SaveProfession);
    $(document).on('click', '.univers .about-body .remove', RemoveProfession);

    // ========================> Adding new University

    // checkbox
    $('.student').on('click', function () {

        if ($(this).prop('checked')) {
            $('.end-date').attr('disabled', 'true')
        }
        else {
            $('.end-date').removeAttr('disabled')
        }
    });

    // adding
    $('.univer-add button').on('click', function () {

        let univerName = $('.univer-name').val();
        let profession = $('.prof-name').val();
        let startDate = $('.start-date').val();
        let endDate = $('.end-date').val();
        let url = $('.univer-add-url').attr('data-url');
        let UniverId;

        if ($('.student').prop('checked')) {
            endDate = new Date();
        }

        console.log(endDate )

        if (checkEmpty(univerName, $('.uName-error'), 60) &&
            checkEmpty(profession, $('.profName-error'), 60) &&
            checkEmpty(startDate, $('.sdate-error')) &&
            checkEmpty(endDate, $('.edate-error'))
        ) {

            let dataToSend = new FormData();
            dataToSend.append('uName', univerName);
            dataToSend.append('pName', profession);
            dataToSend.append('startD', startDate);
            dataToSend.append('endD', endDate);

            $.ajax({
                type: "POST",
                url: url,
                data: dataToSend,
                processData: false,
                contentType: false,
                dataType: "json",
                success: function (result) {

                    if (result.error != null) {
                        alert(result.error);
                    }

                    UniverId = result.id
                    addUniverToBody(univerName, profession, startDate, endDate, UniverId);

                }
            });

            // clear inputs
            $('.univer-name').val('');
            $('.prof-name').val('');


        }

    })

    function addUniverToBody(univerName, profession, startDate, endDate, UniverId) {

        let univer = '<div class="about-section mb-2" id="' + UniverId + '">' +

                            '<div class="about-aside main-back">'+
                            '<small class="p-name-error">İxtisasınızı qeyd eidn (maximum 60 hərf)</small>'+
                            '<div class="w-100">'+
                            '<div class="aside-content">' +
                            '<span>' + profession + ' </span>' +
                                '<input maxlength="60" type="text" class="profession-name">' +
                                '<i class="fas fa-check control-icon save"></i>'+
                                '<i class="fas fa-pen control-icon edit"></i>'+
                        '</div>'+
                    '</div>' +
                '</div>'+
                        '<div class="about-body" id="University">'+
                            '<div class="body-header">'+

                                    '<a href="#" class="main-back-color control-icon save"> <i class="fas fa-check"></i> </a>'+
                                    '<a href="#" class="main-back-color ml-2 control-icon edit"> <i class="fas fa-pen"></i> </a>'+
                                    '<a href="#" class="main-back-color ml-2 control-icon remove"> <i class="fas fa-trash-alt ml-2"></i> </a>'+
                   
                            '</div>'+
                            '<span>' + univerName+ '</span>' +

            '<textarea type="text" maxlength="120" class="form-control university-name"></textarea>' +
            '<small class="univer-name-error">Müəsisənin adını boş buraxmayın</small>' +
            '<i class="ml-2">' + startDate + ' - ' + endDate + '</i>' +

            ' </div>' +
            '</div>';

        $('.univers').prepend(univer);
    }






















    // =========================== Editing

    // Edit profession name
    function EditProfession(e) {
        e.preventDefault();

        $(this).siblings('input').val($(this).siblings('span').html().trim())
        $(this).parent().toggleClass('editing');
    }



    // save profession name
    function SaveProfession(e) {
        e.preventDefault();

        let profession = $('.profession-name').val();
        let id = $(this).parents('.about-section').attr('id');
        let dataToSend = new FormData();
        let url = $('.univer-edit-url').attr('data-url');

        if (checkEmpty(profession, $('.prof-name-error'))) {

            dataToSend.append('id', id);
            dataToSend.append('profession', profession);

            sendAjax(url, dataToSend);
            console.log(profession + ' ' + id + ' ' + url)
        }

        $(this).parents('.aside-content').find('span').text(profession);
        $(this).parent().removeClass('editing');

    }

    // Remove University
    function RemoveProfession(e) {
        e.preventDefault();

        let url = $('.univer-remove-url').attr('data-url');

        let id = $(this).parents('.about-section').attr('id');
        let dataToSend = new FormData();

        if (confirm('are you sure to delete this academic background?')) {
            dataToSend.append('id', id);
            sendAjax(url, dataToSend);

            $(this).parents('.about-section').remove();
        }

    }

    // ========================> Adding new social activity

    $('.socials-add button').on('click', function () {

        let companyName = $('.company-name').val();
        let positionName = $('.position-name').val();
        let aboutAct = $('.about-act').val();
        let url = $('.activity-add-url').attr('data-url');
        let aciId;

        if (checkEmpty(companyName, $('.cname-error'), 60) &&
            checkEmpty(positionName, $('.pname-error'), 60) &&
            checkEmpty(aboutAct, $('.about-act-error'), 250)
        ) {

            let dataToSend = new FormData();
            dataToSend.append('companyName', companyName);
            dataToSend.append('positionName', positionName);
            dataToSend.append('about', aboutAct);

            $.ajax({
                type: "POST",
                url: url,
                data: dataToSend,
                processData: false,
                contentType: false,
                dataType: "json",
                success: function (result) {

                    if (result.error != null) {
                        alert(result.error);
                    }

                    aciId = result.id
                    addActToBody(companyName, positionName, aboutAct, aciId);

                }
            });

            // clear inputs
            $('.company-name').val('');
            $('.position-name').val('');
            $('.about-act').val('');


        }
    });


    function addActToBody(cName, pName, about, aciId) {

        console.log(aciId)

        let activity = '<div class="about-section mb-4"  id="' + aciId + '">' +
            '<div class="about-aside main-back">' +
            '<small class="c-name-error">Təşkilat adını qeyd eidn (maximum 60 hərf)</small>' +
            '<small class="p-name-error">Vəzifənizi qeyd eidn (maximum 60 hərf)</small>' +
            '<div class="w-100">' +
            '<div class="aside-content">' +
            '<span>' + cName + '</span>' +
            '<input maxlength="60" type="text" id="company-name">' +
            '<i class="fas fa-check control-icon save"></i>' +
            '<i class="fas fa-pen control-icon edit"></i>' +
            '</div>' +

            '<div class="aside-content">' +
            '<span>' + pName + '</span>' +
            '<input maxlength="60" type="text" id="position-name">' +
            '<i class="fas fa-check control-icon save"></i>' +
            '<i class="fas fa-pen control-icon edit"></i>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<div class="about-body" id="about-social">' +
            '<div class="body-header">' +
            '<a href="#" class="main-back-color control-icon save"> <i class="fas fa-check"></i> </a>' +
            '<a href="#" class="main-back-color ml-2 control-icon edit"> <i class="fas fa-pen"></i> </a>' +
            '<a href="#" class="main-back-color ml-2 control-icon remove"> <i class="fas fa-trash-alt ml-2"></i> </a>' +
            ' </div>' +
            '<span>' + about + '</span>' +
            '<textarea  maxlength="250" class="form-control" rows="3"></textarea>' +
            '<small class="c-about-error">Haqqınızdakı məlumatı boş buraxmayın (maximum 250 hərf)</small>' +
            '</div>' +
            '</div>';

        $('.social-acts').prepend(activity);
    }







    // ==========================> Editing ==================

    //Edit position
    function EditPosition(e) {
        e.preventDefault();

        $(this).siblings('input').val($(this).siblings('span').html().trim())
        $(this).parent().toggleClass('editing');
    }

    // Save Position
    function SavePosition(e) {
        e.preventDefault();

        let position = $(this).siblings('input').val();
        let url = $('.edit-user-url').attr('data-url');
        let error = $(this).siblings('small');

        if (checkEmpty(position, error, 60)) {

            let dataToSend = new FormData();
            dataToSend.append('position', position);
            sendAjax(url, dataToSend);

            $('.user-info').find('h5').text(position);
            $(this).siblings('span').text($(this).siblings('input').val().trim());
            $(this).parent().removeClass('editing');
        }
    }



    // edit about text
    function EditAbout(e) {
        e.preventDefault();
        $(this).parents('.about-body').toggleClass('editing');
        $(this).parent().siblings('textarea').text($(this).parent().siblings('span').text().trim());

        $(this).siblings('.save').toggle();
    }

    // Save About text

    function SaveAbout(e) {
        e.preventDefault();

        let url = "";
        let error = "";
        let dataToSend = new FormData();
        dataToSend.append('about', $(this).parent().siblings('textarea').val());

        if ($(this).parents('.about-body').attr('id') == 'about') {

            url = $('.edit-user-url').attr('data-url');
            error = $('.u-about-error');
        }
        else if ($(this).parents('.about-body').attr('id') == 'about-social') {

            url = $('.activity-edit-url').attr('data-url');
            dataToSend.append('id', $(this).parents('.about-section').attr('id'));
            error = $('.c-about-error');
        }
        else {
            url = $('.univer-edit-url').attr('data-url');
            dataToSend.append('id', $(this).parents('.about-section').attr('id'));
            error = $('.univer-name-error');
        }

        if (checkEmpty($(this).parent().siblings('textarea').val(), error, 250)) {

            sendAjax(url, dataToSend);
            $(this).parent().siblings('span').text($(this).parent().siblings('textarea').val())
            $(this).parents('.about-body').removeClass('editing');
            $(this).hide();
        }

    }


    // Edit Company name
    function EditCompanyPosition(e) {
        e.preventDefault();

        $(this).siblings('input').val($(this).siblings('span').html().trim())
        $(this).parent().toggleClass('editing');
    }

    // Save company
    function SaveCompanyPosition(e) {
        e.preventDefault();

        let compPosName = $(this).siblings('input').val();
        let compId = $(this).parents('.about-section').attr('id');
        let url = $('.activity-edit-url').attr('data-url');
        let dataToSend = new FormData();
        let error = "";

        if ($(this).siblings('input').attr('id') == 'company-name') {

            dataToSend.append('companyName', compPosName);
            error = $(this).parents('.about-aside').find('.c-name-error');

        }
        else {
            dataToSend.append('positionName', compPosName);
            error = $(this).parents('.about-aside').find('.p-name-error');
        }

        if (checkEmpty(compPosName, error, 60)) {

            dataToSend.append('id', compId);
            sendAjax(url, dataToSend);

            $(this).siblings('span').text($(this).siblings('input').val().trim());
            $(this).parent().removeClass('editing');
        }
    }




    // Remove Activity
    function RemoveAct(e) {
        e.preventDefault();

        let id = $(this).parents('.about-section').attr('id');
        let url = $('.activity-remove-url').attr('data-url');

        if (confirm('are you sure to remove this social activity ?')) {
            let dataToSend = new FormData();
            dataToSend.append('id', id);

            sendAjax(url, dataToSend);

            $(this).parents('.about-section').remove();
        }
    }





    // Function for sending ajax requests
    function sendAjax(url, data) {
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            processData: false,
            contentType: false,
            dataType: "json",
            success: function (result) {

                if (result.error != null) {
                    alert(result.error);
                }
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