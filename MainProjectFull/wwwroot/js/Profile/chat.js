$(function () {

    if ($(".messages ul")[0] != null) {
        $(".messages ul").animate({ scrollTop: $(".messages ul")[0].scrollHeight }, 0);
    }

    $(document).on('click', '.send', sendMessage);
    $(document).on('click', '.remove-messages', removeChat);
    $(document).on('click', '.messages ul li', showDate);

    // trigger enter
    $('textarea').on("keypress", function (e) {
        if (e.which == 13) {
            $('.send').trigger('click');
        }
    });

    // show date
    function showDate() {

        // $(this).find('small').toggleClass('show');
        $(this).find('small').slideToggle();
    };

    // send message
    function sendMessage() {

        let src = $(this).attr('data-src');
        let text = $(this).siblings('textarea').val().trim();
        let friendId = $(this).siblings('.friend-id').attr('id');
        let dataToSend = new FormData();


        if (text != '') {

            dataToSend.append('text', text);
            dataToSend.append('id', friendId);

            $.ajax({
                type: "POST",
                url: "/Message/Send/",
                data: dataToSend,
                contentType: "JSON",
                processData: false,
                contentType: false,
                dataType: "json",
                success: function (result, status) {

                    if (result.error != null) {
                        alert(result.error);
                    }
                    else {
                        if ($(".messages ul")[0] == null) { $(".messages").append('<ul></ul>') }
                        let msg = '<li class="me"> <img class="user-img" src="' + src + '" /> <div class="main-back" id="' + result.id + '"> ' + text + ' <small> indi </small> </div> </li>';
                        $('.messages ul').append(msg);
                        $('textarea').val('');

                        $(".messages ul").animate({ scrollTop: $(".messages ul")[0].scrollHeight }, 1000);
                    }
                }
            });
        }
    }


    // Recieve message
    (function resieveMessage() {

        let friendId = $('.friend-id').attr('id');
        let src = $('.friend-img').attr('data-src');
        dataToSend = new FormData();
        dataToSend.append('id', friendId);

        let today = new Date();

        setInterval(function () {
            $.ajax({
                type: "POST",
                url: "/Message/Get/",
                data: dataToSend,
                contentType: "JSON",
                processData: false,
                contentType: false,
                dataType: "json",
                success: function (result, status) {


                    if (result.messages.length > 0 && $(".messages ul")[0] == null) { $(".messages").append('<ul></ul>') }

                    for (let message of result.messages) {

                        let msgDate = new Date(message.date);

                        if (msgDate.getFullYear() == today.getFullYear() && msgDate.getMonth() == today.getMonth() && msgDate.getDate() == today.getDate() && msgDate.getHours() == today.getHours() && msgDate.getMinutes() == today.getMinutes()) {

                            msgDate = msgDate.getSeconds() - today.getSeconds() + " san əvvəl";
                        }
                        else if (msgDate.getFullYear() == today.getFullYear() && msgDate.getMonth() == today.getMonth() && msgDate.getDate() == today.getDate() && msgDate.getHours() == today.getHours() && msgDate.getMinutes() != today.getMinutes()) {

                            msgDate = msgDate.getMinutes() - today.getMinutes() + " dəq əvvəl";
                        }
                        else if (msgDate.getFullYear() == today.getFullYear() && msgDate.getMonth() == today.getMonth() && msgDate.getDate() == today.getDate() && msgDate.getHours() != today.getHours()) {

                            msgDate = msgDate.getHours() - today.getHours() + " saat əvvəl";
                        }
                        else if (msgDate.getFullYear() == today.getFullYear() && msgDate.getMonth() == today.getMonth() && msgDate.getDate() != today.getDate()) {

                            msgDate = msgDate.getDate() - today.getDate() + " gün əvvəl";
                        }
                        else {
                            msgDate = msgDate.getFullYear() + '.' + msgDate.getMonth() + '.' + msgDate.getDate();
                        }

                        let msg = '<li> <img class="user-img" src="' + src + '" /> <div class="main-back" id="' + message.id + '"> ' + message.message + ' <small> ' + msgDate + ' </small> </div> </li>';
                        $('.messages ul').append(msg);

                        $(".messages ul").animate({ scrollTop: $(".messages ul")[0].scrollHeight }, 1000);
                    }


                }
            });


        }, 500)
    })();

    // Chat settings
    $('.chat-settings').click(function () {

        $(this).siblings().toggleClass('opened');
    });

    // Delete messages

    function removeChat() {

        let friendId = $(this).attr('id');

        let dataToSend = new FormData();
        dataToSend.append('id', friendId);

        if (confirm("Bütün mesajları silmək istəyirsiniz?")) {
            $.ajax({
                type: "POST",
                url: "/Message/RemoveChat/",
                data: dataToSend,
                contentType: "JSON",
                processData: false,
                contentType: false,
                dataType: "json",
                success: function (result, status) {

                    if (result.success) {
                        $('.messages').empty();
                        $('.messages').append('<div class="no-message"> Mesaj yoxdur </div>')
                    }
                }
            });
        }
    }


})