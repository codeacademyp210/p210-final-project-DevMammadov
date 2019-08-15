// ================================= Skills section ===================================
$(document).ready(function () {
    // Skills section clicks 

    $(document).on('click', '.skills-name-edit', EditTopicName);
    $(document).on('click', '.skills-name-add', EnableAddingControls);
    $(document).on('click', '.skills-name-save', SaveTopicName);
    $(document).on('click', '.skills-topic-remove', RemoveTopic);
    $(document).on('click', '.skills-topics .skills a', RemoveSkill);
    $(document).on('input', '.add-controls input[type="text"]', SelectTopicSkill)
    $(".search-skills").on("input", SearchSkills);
    $(".send-new-skill").click(SendCreatedSkill);
    // Languages
    $(".search-lang").on('input', SearchLang);
    $(document).on('click', '.lang-list .skills a', RemoveLang);

    // Urls to database
    let getSkillsUrl = $('.get-skills').attr('data-url');
    let getMySkillsUrl = $('.get-headers').attr('data-url');
    let getSkillsHeaders = $('.get-headers-skills').attr('data-url');
    let addSkillsUrl = $('.add-skills').attr('data-url');
    let removeSkillsUrl = $('.remove-skills').attr('data-url');
    let removeTopicUrl = $('.remove-topic').attr('data-url');
    let addTopicUrl = $('.add-topic').attr('data-url');
    let editTopicUrl = $('.edit-topic').attr('data-url');
    let skillSrc = $('.skills-img').attr('data-url');

    // open skills logo uploader
    $(".skills-logo-upload").click(function () {

        $(this).siblings("input[type='file']").trigger("click");
    });

    // show skills logo when selecting img
    $(".skills-add input[type='file']").change(function () {

        readURL(this, $("#collapse-add-skills img"))
    });


    // Search skills input && Selecting and adding to selected skills
    // incoming skillsObject from Database
    let skillsObj = function (vname, vimg, vid) {
        this.Name = vname,
            this.Icon = vimg,
            this.id = vid
    };

    // Skill header object
    let skillHeaderObj = function (vname, pId, vid) {

        this.Name = vname,
            this.ProfileId = pId,
            this.id = vid
    };

    // Skills and headers object
    let skillsHeadersObj = function (id, pId, hid) {

        this.id = id,
            this.PositionId = pId,
            this.SkillsHeaderId = hid
    };

    //Get user profile id
    let profileId = $("#partials").attr("data-profile");

    // Declare skills array
    let skills = [];
    // Declare all skill headers
    let skillHeaders = [];
    // Declaring all headers-skills pears
    let headersSkills = [];

    // Declare all this users skills
    let allMySkills = [];

    let selectedSkills = [];
    let createdSkills = 0;


    // Getting skills from db
    $.ajax({
        type: "POST",
        url: getSkillsUrl,
        processData: false,
        contentType: false,
        dataType: "JSON",
        success: function (response, status) {

            for (let company in response.skills) {

                skills.push(new skillsObj(vname = response.skills[company].name, vimg = response.skills[company].icon, vid = response.skills[company].id));
            }
        }
    });

    // Getting Headers from db
    $.ajax({
        type: "POST",
        url: getMySkillsUrl,
        processData: false,
        contentType: false,
        dataType: "JSON",
        success: function (response, status) {
            for (let item in response.skillHeaders) {
                skillHeaders.push(new skillHeaderObj(vname = response.skillHeaders[item].name, response.skillHeaders[item].profileId, response.skillHeaders[item].id));
            }
            if (response.error != null) {

                alert(response.error);
            }
        }
    });

    // Getting Headers-Skills from db
    $.ajax({
        type: "POST",
        url: getSkillsHeaders,
        processData: false,
        contentType: false,
        dataType: "JSON",
        success: function (response, status) {
            for (let item in response.skillsHeaders) {
                headersSkills.push(new skillsHeadersObj(response.skillsHeaders[item].id, response.skillsHeaders[item].positionId, response.skillsHeaders[item].skillsHeaderId));
            }
            if (response.error != null) {

                alert(response.error);
            }
        }
    });

    // calculating individual users skills

    //console.log(skillHeaders)

    //for (var i = 0; i < skills.length; i++) {

    //    console.log(skillHeaders[0])

    //    if (skillHeaders[i].id == profileId) {


    //        for (var y = 0; y < headersSkills.length; y++) {

    //            if (headersSkills[y].SkillsHeaderId == skillHeaders[i].id) {

    //                for (var x = 0; x < skills.length; x++) {

    //                    if (skills[x].id == headersSkills[y].PositionId) {

    //                        allMySkills.push(skills[x])

    //                    }
    //                }
    //            }

    //        }
    //    }
    //}


    function SearchSkills() {

        let text = $(this).val();
        $(this).siblings("ul").html("");

        for (let elm in skills) {


            if (skills[elm].Name.toLocaleLowerCase().indexOf(text.toLocaleLowerCase()) > -1 && text.length > 2) {
                // if this skill not selected
                if (selectedSkills.indexOf(skills[elm]) < 0 && allMySkills.indexOf(skills[elm]) < 0) {

                    $(this).siblings("ul").append("<li> <a href='#' id='" + skills[elm].id + "' class='select-skills'> <img src='" + skillSrc + skills[elm].Icon + "'> <span> " + skills[elm].Name + " </span> </a></li>");
                }
            }
        }
        // Function select skills from list add to bottom
        $(this).siblings("ul").find("a").click(selectskills)

    };



    // Function select skills from list add to bottom
    function selectskills(e) {
        e.preventDefault();

        for (let c in skills) {
            if (skills[c].id == $(this).attr("id")) {
                currentSkill = skills[c];
            }
        }

        $(".added-skills").append("<div class='skills bg-white'> <a href='#' class='main-back-color' id='" + currentSkill.id + "'> <i class='fas fa-times main-back-color'></i></a> <img src='" + skillSrc + currentSkill.Icon + "'> <span>" + currentSkill.Name + "</span></div>");

        $(this).parent().parent().html("");
        $(".search-skills").val("");

        selectedSkills.push(currentSkill);
        //allMySkills.push(currentSkill);

        // Function remove selected skills from bottom and list
        $(".added-skills").find("a").click(RemoveSkillFromList);
    }




    // Function remove selected skills from bottom and list
    function RemoveSkillFromList(e) {

        e.preventDefault();
        $(this).parent().remove();

        for (let skill in selectedSkills) {

            if (selectedSkills[skill].id == $(this).attr("id")) {
                selectedSkills.splice(skill, 1);
            }
        }
    }



    // Send new created skill
    function SendCreatedSkill(e) {
        e.preventDefault();

        if ($(".search-skills").val() != "") {
            $(".text-danger.skill-name").hide();

            if ($(this).siblings("input[type='file']")[0].files.length > 0) {
                $(".text-danger.skill-logo").hide();
                let dataToSend = new FormData();
                dataToSend.append("name", $(".search-skills").val());
                dataToSend.append("type", $(".skill-type").val());
                dataToSend.append("file", $(this).siblings("input[type='file']")[0].files[0]);

                sendAjax(addSkillsUrl, dataToSend);

                // to success
                $("#collapse-add-skills .text-success").show().delay(4000).slideUp();
                $(this).siblings("input[type='file']").val(null);
                $(this).siblings("img").attr("src", "");
                $(".search-skills").val("");
                createdSkills++;
            }
            else {
                $(".text-danger.skill-logo").show();
            }
        }
        else {
            $(".text-danger.skill-name").show();
        }
    };


    // Save skills

    $(".skills-form input[type='submit']").click(function () {

        if ($(".topic-header").val() != '') {
            $(".text-danger.select-skill").hide();

            if (createdSkills > 0 || selectedSkills.length > 0) {

                allMySkills.concat(selectedSkills); // getting all user skills
                $(".text-danger.select-skill").hide();

                let skillArr = JSON.stringify(selectedSkills);
                let topicHeader = $(".topic-header").val();
                let dataToSend = new FormData();

                dataToSend.append("skills", skillArr);
                dataToSend.append("header", topicHeader);

                $.ajax({
                    type: "POST",
                    url: addTopicUrl,
                    data: dataToSend,
                    processData: false,
                    contentType: false,
                    dataType: "json",
                    success: function (result) {

                        //  console.log(result.success)
                        loadTopic(result.success);
                    }
                });


            }
            else {
                $(".text-danger.select-skill").show();
            }
        }
        else {
            $(".text-danger.select-skill-header").show();
        }
    });


    // Load skills to page after saving 
    function loadTopic(topicID) {
        let skillDiv = '<div id="' + topicID + '" class="skills-topic">' +
            '<h6 class="main-back">' +
            '<div class="position-relative">' +
            '<span class="skills-header"> ' + $(".topic-header").val() + ' </span>' +
            '<div class="skills-edit-controls">' +
            '<input type="text" class="skills-name" name="">' +
            '<button data-url="" class="butt butt-gray skills-name-save">Yadda saxla</button>' +
            '</div>' +
            '</div>' +
            '<div class="add-controls search-area">' +
            '<input type="text" class="">' +
            '<ul></ul>' +
            '</div>' +
            '<div>' +
            '<a href="#" data-id="3" class="skills-name-add text-white control-icon">' +
            ' <i class="fas fa-plus"></i> ' +
            '</a>' +
            '<a href="#" data-url="" class="skills-name-edit ml-2 text-white  control-icon">' +
            ' <i class="fas fa-pen"></i> ' +
            '</a>' +
            '<a href="#" class="skills-topic-remove ml-2 text-white  control-icon" data-url="">' +
            ' <i class="fas fa-trash-alt mr-2"></i> ' +
            '</a> ' +
            '</div>' +
            '</h6>' +
            '<div class="skills-list">';

        for (c in selectedSkills) {
            skillDiv += '<div class="skills bg-white">' +
                '<a href="#" class="control-icon" data-url="" data-id="' + selectedSkills[c].id + '"> <i class="fas fa-times"></i></a>' +
                '<img src="' + skillSrc + selectedSkills[c].Icon + '" alt="">' +
                selectedSkills[c].Name +
                '</div>';
        }

        skillDiv += '</div></div>';
        $(".skills-topics").prepend(skillDiv);

        //clear all after saving
        $(".added-skills").html("");
        $(".topic-header").val("");
        $(".text-danger").hide();
        selectedSkills.length = 0;
        createdSkills = 0;
    }


    // =============================== Editing <===================================


    // Skills whole topic delete
    function RemoveTopic() {
        if (confirm("Bütün movzunu biliklərlə birlikdə silmək istəyirsiniz?")) {

            let dataToSend = new FormData();
            dataToSend.append('id', $(this).parents('.skills-topic').attr('id'));

            sendAjax(removeTopicUrl, dataToSend);

            $(this).parents('.skills-topic').remove();
        }
    }


    // Save topic name function 
    function SaveTopicName() {

        if ($('.skills-name').val() != '') {
            $('.skills-name').css({ 'border': '1px solid rgb(214, 214, 214)' })

            let dataToSend = new FormData();
            dataToSend.append('id', $(this).parents('.skills-topic').attr('id'));
            dataToSend.append('name', $(this).siblings().val());

            sendAjax(editTopicUrl, dataToSend);

            $(this).parent().siblings().html($(this).siblings().val());
            $(this).parents('.skills-topic').removeClass("skills-editing");
        }
        else {
            $('.skills-name').css({ 'border': '1px solid red' })
        }
    }

    // Function editing topic name
    function EditTopicName(e) {
        e.preventDefault();
        $(this).parents('.skills-topic').find("input").val("");
        $(this).parents(".skills-topic").removeClass("skills-adding");
        $(this).parents('.skills-topic').find(".skills-edit-controls input").val($(this).parents('.skills-topic').find("span").html());
        $(this).parents('.skills-topic').toggleClass("skills-editing");
    }


    // enable adding mode
    function EnableAddingControls(e) {
        e.preventDefault();
        $(this).parents(".skills-topic").removeClass("skills-editing");
        $(this).parents(".skills-topic").toggleClass("skills-adding");
    }

    // selecting skills from controls
    function SelectTopicSkill() {

        let text = $(this).val();
        $(this).siblings("ul").html("");

        for (let elm in skills) {

            if (skills[elm].Name.toLocaleLowerCase().indexOf(text.toLocaleLowerCase()) > -1 && text.length > 2) {
                // if user skills not contain this skill
                if (allMySkills.indexOf(skills[elm]) < 0) {
                    $(this).siblings("ul").append("<li> <a href='#' id='" + skills[elm].id + "' class='select-skills'> <img src='" + skillSrc + skills[elm].Icon + "'> <span> " + skills[elm].Name + " </span> </a></li>");
                }
            }
        }
        // Function select skills from list add to topic
        $(this).siblings("ul").find("a").click(addToTopic)
    }


    // Add to topic selected skill
    function addToTopic(e) {
        e.preventDefault();

        for (let c in skills) {
            if (skills[c].id == $(this).attr("id")) {
                currentSkill = skills[c];
            }
        }

        let dataToSend = new FormData();
        dataToSend.append('skillId', currentSkill.id);
        dataToSend.append('id', $(this).parents('.skills-topic').attr('id'));

        sendAjax(editTopicUrl, dataToSend);

        $(this).parents('.skills-topic').find('.skills-list').append("<div class='skills bg-white'> <a href='#' data-id='" + currentSkill.id + "'> <i class='fas fa-times'></i></a> <img src='" + skillSrc + currentSkill.Icon + "'> <span>" + currentSkill.Name + "</span></div>");

        $(this).parent().parent().html("");
        $(".add-controls input").val("");

        allMySkills.push(currentSkill);
    }


    // Remove Skill
    function RemoveSkill(e) {
        e.preventDefault();
        if (confirm("Bu bacarığı silmək istəyirsiniz?")) {
            let dataToSend = new FormData();
            dataToSend.append('skillId', $(this).attr('data-id'));
            dataToSend.append('topicId', $(this).parents('.skills-topic').attr('id'));

            sendAjax(removeSkillsUrl, dataToSend);
            //on success
            $(this).parent().remove();

            for (let c in allMySkills) {
                if (allMySkills[c].id == $(this).attr('data-id')) {
                    allMySkills.splice(allMySkills.indexOf(allMySkills[c], 1));
                }
            }
        }
    }



    // ====================================  Language =============================================== //

    let LanguageObj = function (id, name, icon) {
        this.id = id,
            this.Name = name,
            this.Icon = icon
    };

    let languages = [];

    $.ajax({
        type: "POST",
        url: $('.get-langs').attr('data-url'),
        contentType: "JSON",
        processData: false,
        contentType: false,
        dataType: "json",
        success: function (response, status) {
            for (let lang in response.langs) {
                // Getting companyes from server
                languages.push(new LanguageObj(response.langs[lang].id, response.langs[lang].name, response.langs[lang].icon));
            }
        }
    });


    function SearchLang() {

        let text = $(this).val();
        $(this).parent().siblings("ul").html("");
        let imgPath = $(this).parent().siblings('ul').attr('data-src');

        for (let elm in languages) {

            if (languages[elm].Name.toLocaleLowerCase().indexOf(text.toLocaleLowerCase()) > -1 && text.length > 1) {

                $(this).parent().siblings("ul").append("<li> <a href='#' id='" + languages[elm].id + "' class=''> <img src='" + imgPath + languages[elm].Icon + "'> <span> " + languages[elm].Name + " </span> </a></li>");
            }
        }

        // Function select language from list
        $(this).parent().siblings("ul").find("a").click(selectLang);
    };

    function selectLang(e) {
        e.preventDefault();

        let imgPath = $(this).find("img").attr("src").split("/");
        let imgName = imgPath[imgPath.length - 1];
        let path = $(this).find("img").attr("src")

        $(".search-company-logo").attr('src', $(this).find("img").attr("src"));
        $(".search-company-logo").show();

        //setting lang name
        $(".search-area input").val($(this).find("span").html());
        // setting lang id to hidden input
        $(".search-area .l-id").val($(this).attr("id"));
        //making area readonly and clear all
        $(".search-area input").attr("readonly", "true");
        $(".search-area input").css("background", "#fff");
        $(".exper-form .search-area .cancel").show();
        $(this).parents('ul').html("");

        $(".exper-form .search-area .cancel").click(cancelSelection);
    }


    // Cancel selection (press x button)
    function cancelSelection(e) {
        e.preventDefault();

        $(".search-area input").val('');
        $(".search-area .l-id").val('');
        $(".search-area input").removeAttr("readonly");

        $(".search-company-logo").hide();
        $(".search-company-logo").attr('src', '');
        $(this).hide();
    }


    // Save language
    $('.lang-form input[type="submit"]').click(function () {

        let langID = $('.l-id').val();
        let langLevel = $('.lang-level').val();
        let error = $('.lang-error');
        
        let url = $('.save-lang').attr('data-url');
        let dataToSend = new FormData();

        dataToSend.append('id', langID);
        dataToSend.append('level', langLevel);

        if (langID != '') {
            error.hide();
            sendAjax(url, dataToSend);
            loadLangToBody(langID, langLevel);

        }
        else {
            error.show();
        }

    });



    function loadLangToBody(id, langLevel) {
        let currentLang;
        let iconSrc = $('.lang-list').attr('data-src');

        for (l in languages) {
            if (languages[l].id == id) {
                currentLang = languages[l];
            }
        }
        

        let lang = '<div class="skills bg-white">' +

            '<a href="#" class="control-icon" data-url="" data-id="'+id+'"> <i class="fas fa-times"></i></a>' +
            '<img src="' + iconSrc + currentLang.Icon + '" alt="">' +
            currentLang.Name + ' - ' + langLevel + '</div>';

            $('.lang-list').prepend(lang);
    }



    // Remove Lang
    function RemoveLang(e) {
        e.preventDefault();

        let url = $('.remove-lang').attr('data-url');

        if (confirm("Bu dili silmək istəyirsiniz?")) {
            let dataToSend = new FormData();
            dataToSend.append('id', $(this).attr('data-id'));

            sendAjax(url, dataToSend);
            //on success
            $(this).parent().remove();

        }
    }




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



}); //main