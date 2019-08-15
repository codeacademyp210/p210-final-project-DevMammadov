$(function () {

    $(document).on('click', '.port-window .cancel a', closePortfolio);
    $(document).on('click', '.port-list .portfolio', openPortfolio);
    $(document).on('click', '.profile-menu ul li.port', openPortfolio);
    $(document).on('click', '.search-results .portfolio-item img', openPortfolio);
    $(document).on('click', '.remove-port-img', removePortImg);
    $(document).on('click', '.remove-port', removePort);
    $(document).on('click', '.port-img-add', addPortImg);
    $(document).on('change', '.port-img-select', selectImage);
    $(document).on('click', '.send-comment', sendComment);
    $(document).on('click', '.btn-folow', folowUser);
    $(document).on('click', '.block-user', blockUser);
    $(document).on('click', '.block-this-user', blockUser);

    // open cv
    $(document).on('click', '.cv-forms.index .portfolio-img img', openCv);
    // main search
    $(document).on('click', '.main-cover .main-search', mainSearch);

    /* getting current user information */
    
    $.ajax({
        type: "POST",
        url: $('.profile-link').attr('data-url'),
        processData: false,
        contentType: false,
        dataType: "json",
        success: function (result) {
            if (result.error == null) {

                let src = $('.profile-link img').attr('data-src');
                $('.profile-link img').attr('src', src + result.photo);
                $('.u-name').text(result.name + ' ' + result.surname);

                takeNotify(result.id);
               // takeMessages(result.id);
            }
        }
    });
    
    // Chekging for notifications
    function takeNotify(id) {

        let notifyBadge = $('.notificon.notify span');
        let notifyList = $('.notification-list');
        let src = notifyList.parents('.profile-menu').attr('data-src');
        let folder = "";

        let dataToSend = new FormData();
        dataToSend.append("id", id);

      setInterval(function () {
            $.ajax({
                type: "POST",
                url: $('.profile-menu .notification-list').attr('data-url'),
                data: dataToSend,
                processData: false,
                contentType: false,
                dataType: "json",
                success: function (result) {
                    if (result.unreadCount > 0) {
                        notifyBadge.html(result.unreadCount);
                        notifyBadge.show();
                    }
                    notifyList.empty();
                    for (let item of result.notifies) {
                        let Nclass = '';
                        let Nport = '';
                        let Nid = '';
                        let text = '';
                        let href = '#';

                        if (item.status == false) { Nclass = 'unread' }
                        if (item.type == "port") { Nid = item.portfolioId; Nport = 'port'; folder = "Users/"; text = item.text.substr(0, 50) + '...' }
                        if (item.type == "system") { folder = "System/"; text = item.text; }
                        if (item.type == "portImg") { folder = "PortImages/"; text = item.text; Nid = item.portfolioId; Nport = 'port' }
                        if (item.type == "exper") { folder = "Users/"; text = item.text; Nport = 'exper'; href = "/Profile/Experience/"+item.senderProfileId; }


                        let notify = '<li id="' + Nid + '" class="' + Nclass + ' ' + Nport + '"> <a href = "'+href+'" > ' +
                            '<img src="' + src + folder + item.photo + '" alt=""><span>' +
                            '<b>' + item.senderName + item.senderSurname + '</b><br>' +
                            text +
                            '<small>' + item.date.substr(0, 10) + '</small >' +
                            '</span></a></li >';

                        notifyList.prepend(notify);
                    }
                }
            });
        }, 3000);
    }


// open notification menu
$('.notify').on('click', function () {

    $('.menu').find('.spliter').removeClass('opened');
    $('.menu-list').removeClass('opened');
    $('.message-list').removeClass('opened');
    $('.message').find('.spliter').removeClass('opened');
    $(this).siblings('.notification-list').toggleClass('opened');
    $(this).find('.spliter').toggleClass('opened');

    let email = $(this).parents('header').attr('data-user');
    let dataToSend = new FormData();
    dataToSend.append("email", email);

    $.ajax({
        type: "POST",
        url: $(this).attr('data-url'),
        processData: false,
        contentType: false,
        data: dataToSend,
        success: function (result,success) {
            if (result.success == true) {

                $('.notify').find('.badge').html('0').hide();

            }
        }
    });

    });



    // Chekging for notifications
    //function takeMessages(id) {

    //    let notifyBadge = $('.notificon.message span');
    //    let notifyList = $('.message-list');
    //    let src = notifyList.parents('.profile-menu').attr('data-src');
    //    let folder = "Users/";

    //    let dataToSend = new FormData();
    //    dataToSend.append("id", id);

    // //   setInterval(function () {
    //        $.ajax({
    //            type: "POST",
    //            url: "/GetMessages/Home",
    //            data: dataToSend,
    //            processData: false,
    //            contentType: false,
    //            dataType: "json",
    //            success: function (result) {

    //                console.log(result)
    //            }
    //        });
    //  //  }, 3000);
    //}





    // Setting user info to header dripdown menu
    
    /* Main header menu  */

    // open mobile version menu
    $(".profile-menu .menu-opener").on('click', function () {

        $(this).siblings('.ul-menu').toggleClass('opened');
    });

    // open messages menu
    $('.message').on('click', function () {

        $('.menu').find('.spliter').removeClass('opened');
        $('.menu-list').removeClass('opened');
        $('.notification-list').removeClass('opened');
        $('.notify').find('.spliter').removeClass('opened');
        $(this).siblings('.message-list').toggleClass('opened');
        $(this).find('.spliter').toggleClass('opened');
    });

    // open menu
    $('.menu').on('click', function () {

        $('.message-list').removeClass('opened');
        $('.notification-list').removeClass('opened');
        $('.message').find('.spliter').removeClass('opened');
        $('.notify').find('.spliter').removeClass('opened');
        $(this).siblings('.menu-list').toggleClass('opened');
        $(this).find('.spliter').toggleClass('opened');
    });



    /* Fill selectbox year mounth day on register page */

    var date = new Date();

    // Add year
    for (let i = date.getFullYear(); i > date.getFullYear() - 50; i--) {

        let option = "<option value='" + i + "'> " + i + " </option>";
        $("#year").append(option);
    }

    let days = ["Yanvar", "Fevral", "Mart", "Aprel", "May", "Iyun", "Iyul", "Avqust", "Sentyabr", "Oktyabr", "Noyabr", "Dekabr"];


    // Add month
    for (let i = 0; i < 12; i++) {

        let val = 0;
        if (i < 9) {
            val = "0" + (i + 1);
        }
        else {
            val = i + 1;
        }
        let option = "<option value='" + val + "'> " + days[i] + " </option>";
        $("#month").append(option);
    }

    // Add days
    for (let i = 1; i < 32; i++) {

        let val = 0;
        if (i < 10) {
            val = "0" + i;
        }
        else {
            val = i;
        }
        let option = "<option value='" + val + "'> " + val + " </option>";
        $("#day").append(option);
    }

    // Set data to datetimer

    let day = $('#day').val();
    let year = $('#year').val();
    let month = $('#month').val();

    $('.birth-date').val(year + '-' + month + '-' + day);

    // set date onchange
    $('#day').on('change', function () {
        day = $(this).val();
        $('.birth-date').val(year + '-' + month + '-' + day);
    });

    $('#year').on('change', function () {
        year = $(this).val();
        $('.birth-date').val(year + '-' + month + '-' + day);
    });

    $('#month').on('change', function () {
        month = $(this).val();
        $('.birth-date').val(year + '-' + month + '-' + day);
    });






    // open portfolio
    function openPortfolio(e) {
        e.preventDefault();

        let id = $(this).attr('id');
        let dataToSend = new FormData();
        dataToSend.append('id', id);

        $.ajax({
            url: $('.port-window').attr('data-port'),
            type: "GET",
            data: {
                "id": id
            },
            success: function (response, status) {

                if (response.error != null) {
                    alert(response.error)
                }
                else {
                    $('.port-window').html(response);
                    $('body').css({ 'overflow': 'hidden' });
                    $('.port-window').show();
                }
            }
        });
    }


    function closePortfolio(e) {
        e.preventDefault();
        $('.port-window').hide();
        $('body').css({ 'overflow': 'auto' });
    };


    function removePortImg(e) {
        e.preventDefault();
        $this = $(this);
        let imgId = $(this).siblings('img').attr('id');
        let portId = $(this).parents('.window-body').attr('data-port');
        let dataToSend = new FormData();
        dataToSend.append('imgId', imgId);
        dataToSend.append('portId', portId);

        if (confirm("Bu şəkli fortfolionuzdan silmək istəyirsinizmi?")) {
            $.ajax({
                type: "POST",
                url: $(this).parents('.window-body').attr('data-remove'),
                processData: false,
                contentType: false,
                dataType: "json",
                data: dataToSend,
                success: function (result) {
                    if (result.error != null) {
                        alert(result.error)
                    }
                    else {
                        $this.parent().remove();
                        console.log($('.profile-link').attr('data-url') + ' ' + imgId + ' ' + portId)
                    }
                }
            });
        }
    }

    // Remove all portfolio
    function removePort(e) {
        e.preventDefault();

        let id = $(this).attr('data-port');
        let dataToSend = new FormData();
        dataToSend.append('id', id);

        if (confirm("Bu portfolionu tamamilə silmək istəyirsinizmi?")) {
            $.ajax({
                type: "POST",
                url: "/Port/Remove/",
                processData: false,
                contentType: false,
                dataType: "json",
                data: dataToSend,
                success: function (result) {
                    if (result.error != null) {
                        alert(result.error);
                    }
                    if (result.success) {
                        $('.window-main').hide();
                        window.location.reload();
                    }
                }
            });
        }

    }


    function addPortImg(e) {
        e.preventDefault();

        $('.port-img-select').trigger('click');
    }

    function selectImage() {
        var file = $(this)[0].files[0];
        let dataToSend = new FormData();
        let portId = $(this).attr('data-port');
        let src = $(this).attr('data-src');
        dataToSend.append('file', file);
        dataToSend.append('id', portId);

        $.ajax({
            type: "POST",
            url: $(this).attr('data-url'),
            processData: false,
            contentType: false,
            dataType: "json",
            data: dataToSend,
            success: function (result) {

                let portImg = $('.window-body');
                let newImg = '<div class="port-img-div">' +
                    '<a href="#" class="remove-port-img">' +
                    '<i class="fas fa-trash-alt"></i></a>' +
                    '<img id="' + result.id + '" src="' + src + result.uniqFileName + '" /></div>';




                portImg.prepend(newImg);
            }
        });

    };

    // add comment to portfolio

    function sendComment() {

        let text = $(this).siblings('textarea').val();
        let portId = $(this).parents('.window-footer').siblings('.window-body').attr('data-port');
        let url = $(this).parents('.comment-form').attr('data-url');
        let src = $(this).parents('.comment-form').attr('data-src')

        let dataToSend = new FormData();
        dataToSend.append('id', portId);
        dataToSend.append('text', text);

        if (text != '') {

            $.ajax({
                type: "POST",
                url: url,
                processData: false,
                contentType: false,
                dataType: "json",
                data: dataToSend,
                success: function (result) {

                    if (result.error != null) {
                        alert(result.error);
                    }
                    else {
                        
                        let comment = '<div class="comment" id="' + result.id + '"> <div class="image">' +
                                        '<img src="' + src + result.img + '" alt=""></div>' +
                                            '<div class="text">' +
                                                '<div class="user-name main-back-color">' + result.name + ' ' + result.surname + '</div>' +
                                                     text +
                                                '</div></div >';

                        $('.window-footer .comments').prepend(comment);

                        $('.window-footer textarea').val('')
                    }
                }
            });
        }

    }

    // Open Cv
    function openCv() {

        let id = $(this).attr('id'); // cv id
        let dataToSend = new FormData();
        dataToSend.append('id', id);

        $.ajax({
            url: $('.port-window').attr('data-cv'),
            type: "GET",
            data: {
                "id": id
            },
            success: function (response, status) {

                $('.port-window').html(response);
                $('body').css({ 'overflow': 'hidden' });
                $('.port-window').show();

                // change background of selected items
                let selectedSkills = [];
                $.each($('.render-partial.cv .left-sort-menu .icon-chk'), (i, v) => {

                    if ($(v).prop('checked')) {

                        let skill = $(v).siblings('label').find('img').attr('title');
                        selectedSkills.push(skill);
                    }
                });

                $.each($('.port-window .cv-skills ul li'), (i, v) => {

                    if (selectedSkills.includes($(v).html().trim())) {
                        $(v).css({
                            'background': 'rgba(238, 255, 0, 0.171)',
                            'padding-left': '5px'
                        });
                    }
                })
            }
        });

    }


    // ===================== Folow user ========================= //

    function folowUser() {

        let $this = $(this);
        let id = $this.attr('data-id');
        let url = $this.attr('data-url');
        let dataToSend = new FormData();
        dataToSend.append('id', id);

        $.ajax({
            type: "POST",
            url: url,
            processData: false,
            contentType: false,
            dataType: "json",
            data: dataToSend,
            success: function (result) {
                if (result.error != null) {
                    alert(result.error)
                }
                else {
                    $this.html('<i class="fas fa-user-check main-back-color"></i><span> İzləyirsiz</span>');
                }
            }
        });

    }

    // open unfollow menu
    $('.user-info .open-menu').click(function () {

        $('.user-info').find('ul').toggleClass('opened');
    });

    // unfollow user
    $('.user-info .unfolow').click(function () {
        let $this = $(this);
        let id = $this.attr('data-id');
        let url = $this.attr('data-url');
        let dataToSend = new FormData();
        dataToSend.append('id', id);

        $.ajax({
            type: "POST",
            url: url,
            processData: false,
            contentType: false,
            dataType: "json",
            data: dataToSend,
            success: function (result) {
                if (result.error != null) {
                    alert(result.error)
                }
                else {
                    $('.btn-folow').html(' <i class="fas fa-user "></i> <span> İzlə </span> ');
                    $this.parents('li').hide();
                    $this.parents('ul').toggleClass('opened');
                }
            }
        });
    });


    // Main search in main page
    function mainSearch() {

        let dataToSend = new FormData();
        let text = $(this).siblings('input[type="text"]').val().trim();
        let url = $(this).attr('data-url');
        let type = "";
        
        if ($('#partials > article').hasClass('cv-search')) {
            type = "cv";
        }
        else if ($('#partials > article').hasClass('person')) {
            type = "person";
        }
        else if ($('#partials > article').hasClass('search-port')) {
            type = "port"
        }
        if (text != "") {

            $.ajax({
                type: "POST",
                url: url,
                data: { "text": text, "type" : type },
                success: function (result) {
                    
                    $("#partials").html(result);

                }
            });
        }
    }

    // Block user
    function blockUser(e) {
        e.preventDefault();

        let userId = $(this).attr('id');
        let dataToSend = new FormData();
        dataToSend.append('id', userId);

        $.ajax({
            type: "POST",
            url: "/Home/Block/",
            data: dataToSend,
            contentType: "JSON",
            processData: false,
            contentType: false,
            dataType: "json",
            success: function (result, status) {

                if (result.success) {
                    location.reload();
                }
            }
        });
    }





    
});