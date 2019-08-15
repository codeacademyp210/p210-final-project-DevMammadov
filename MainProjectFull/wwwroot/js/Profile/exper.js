$(function () {

    $(document).on('input', '.search-area input', SearchCompany);
    $(document).on('click', '.about-work-edit', editAboutText);
    $(document).on('click', '.about-work-save', saveAboutText)
    $(document).on('click', '.s-date-edit', editStartDate);
    $(document).on('click', '.s-date-save', saveStartDate);
    $(document).on('click', '.e-date-edit', editEndDate);
    $(document).on('click', '.e-date-save', saveEndDate);
    $(document).on('click', '.experience-remove', removeExperience);
    $(".register-company").on('change', registerCompany);
    $(".exper-form input[type='submit']").on("click", saveCompany);


    // ========================== Saving <=================================


    // Cheking current time and disabling end datetime for end date of work
    $("#present").on("change", function () {
        if ($(this).prop("checked")) {
            $(".end-date").attr("disabled", "disabled");
        }
        else {
            $(".end-date").removeAttr("disabled");
        }
    });

    // Search experience input && Selecting and adding to input
    // incoming companies from Database
    let companyObj = function (vname, vimg, vid) {
        this.name = vname,
            this.img = vimg,
            this.id = vid
    };

    let experObj = function (id, sdate, edate, about, compid) {
        this.id = id,
            this.sdate = sdate,
            this.edate = edate,
            this.about = about,
            this.compid = compid
    };

    // Define companyList array
    let companyList = [];
    // Define Experience list array
    let experList = [];

    $.ajax({
        type: "POST",
        url: $('.exper-get-url').attr('data-url'),
        contentType: "JSON",
        processData: false,
        contentType: false,
        dataType: "json",
        success: function (response, status) {

            for(let company in response.result){
                // Getting companyes from server
                companyList.push(new companyObj(vname = response.result[company].name, vimg = response.result[company].icon, vid = response.result[company].id));
            } 
        }
    });

    function SearchCompany() {

        let text = $(this).val();
        $(this).parent().siblings("ul").html("");
        let imgPath = $(this).parent().siblings('ul').attr('data-src');

        for (let elm in companyList) {

            if (companyList[elm].name.toLocaleLowerCase().indexOf(text.toLocaleLowerCase()) > -1 && text.length > 0) {

                $(this).parent().siblings("ul").append("<li> <a href='#' id='" + companyList[elm].id + "' class=''> <img src='" + imgPath + companyList[elm].img + "'> <span> " + companyList[elm].name + " </span> </a></li>");
            }
        }
        // Function select experience from list
        $(this).parent().siblings("ul").find("a").click(selectCompany);
    };

    // Selecting company from list
    function selectCompany(e) {
        e.preventDefault();

        let imgPath = $(this).find("img").attr("src").split("/");
        let imgName = imgPath[imgPath.length - 1];

        let path = $(this).find("img").attr("src")
        $(".search-company-logo").attr('src', $(this).find("img").attr("src"));
        $(".search-company-logo").show();

        //setting company name
        $(".search-area input").val($(this).find("span").html());
        // setting name/id to hidden input
        $(".search-area .c-name").val($(this).find("span").html().trim());
        $(".search-area .c-id").val($(this).attr("id"));
        //making area readonly and clear all
        $(".search-area input").attr("readonly", "true");
        $(".search-area input").css("background", "#fff");
        $(".exper-form .search-area .cancel").show();
        $(this).parents('ul').html("");
        // disabling company register checkbox
        $('.register-company').attr("disabled", "disabled")

        $(".exper-form .search-area .cancel").click(cancelSelection);
    }

    // Cancel selection (press x button)
    function cancelSelection(e) {
        e.preventDefault();

        $(".search-area input").val('');
        $(".search-area .c-name").val('');
        $(".search-area .c-id").val('');
        $(".search-area input").removeAttr("readonly");

        $(".search-company-logo").hide();
        $(".search-company-logo").attr('src', '');
        // enabling company register checkbox
        $('.register-company').removeAttr("disabled")

        $(this).hide();
    }


    // Checkbox : Register new company
    function registerCompany() {
        if ($(this).prop('checked')) {
            if ($(".search-area input").val() != '') {
                $('.c-name-error').hide();
                // giving name to hidden input id will be null
                $(".search-area .c-name").val($(".search-area input").val().trim());
            }
            else {
                $('.c-name-error').css({ 'display': 'inline' });
            }
        }
        else {
            $(".search-area .c-name").val('');
            $('.c-name-error').hide();
        }
    }


    // Save Experience
    function saveCompany() {

        let cName =  $('.search-area .c-name').val();
        let cID =    $('.search-area .c-id').val();
        let startD = $(".start-date").val();
        let endD =   $(".end-date").val();
        let about =  $(".exper-about").val();

        if (checkEmpty(cName, $('.c-name-error')) &&
            checkEmpty(startD, $('.s-date-error')) &&
            checkEmpty(endD, $('.e-date-error'), $('#present').prop('checked'), null) &&
            checkEmpty(about, $('.about-work-error'), null, 100)
        ) {
            // sending form to database
            let dataToSend = new FormData();
            dataToSend.append("CompanyName", cName);
            dataToSend.append("CompanyId", cID);
            dataToSend.append("startDate", startD);
            dataToSend.append("about", about);
            dataToSend.append("endDate", endD);

            $.ajax({
                type: "POST",
                url: $('.exper-save-url').attr('data-url'),
                data: dataToSend,
                contentType: "JSON",
                processData: false,
                contentType: false,
                dataType: "json",
                success: function (result, status) {
                    
                    experList.push(new experObj(result.experId, startD, endD, about, cID));
                    if (result.companyId != null) {
                        companyList.push(new companyObj(cName, 'none.png', result.companyId));
                    }

                    loadExperience(experList.length - 1);
                }
            });

        }
    }

    // Add new experience to body automaticly
    function loadExperience(experIndex) {

        let src = $('.company-logo-src').attr('data-src');
        let exp = experList[experIndex];
        let company, cimg, cname;

        for (let c in companyList) {
            if (companyList[c].id == experList[experIndex].compid) {
                company = companyList[c];
            }
        }

        if (company == null) {
            cimg = 'none.png';
            cname = $('.search-area .c-name').val();
        }
        else {
            cimg = company.img;
            cname = company.name;
        }

        let newExperience = '<div class="exper" id="' + exp.id + '">' +
            '<div class="exper-aside  main-back">' +
            '<img src="' + src + cimg + '" alt="">' +
            '<h5>' + cname + '</h5>' +
            '<div class="mt-4 start-date">' +
            '<b>Başlama:</b>' +
            '<span>' + exp.sdate + '</span>' +
            '<input type="hidden" value="' + exp.sdate + '" class="s-date"> ' +
            '<div class="s-date-edit-controls">' +
            '<input type="date" class="borderless">' +
            '<small class="text-danger s-date-error">Lütfən başlanğıc tarixi yazın </small>' +
            '</div>' +
            '<a href="#" class="s-date-save control-icon">' +
            '<i class="fas fa-check"></i>' +
            '</a> ' +
            '<a href="#" class="s-date-edit control-icon"> ' +
            '<i class="fas fa-pen"></i>' +
            '</a> ' +
            '</div>' +

            '<div class="end-date">' +
            ' <b>Bitmə:</b>' +
            '<span>' + exp.edate + '</span>' +
            '<input type="hidden" value="' + exp.edate + '" class="e-date"> ' +
            '<div class="e-date-edit-controls">' +
            '<input type="date" class="borderless">' +
            '<small class="text-danger e-date-error">Lütfən bitmə tarixi yazın </small>' +
            '</div>' +
            '<a href="#" class="e-date-save control-icon"> ' +
            '<i class="fas fa-check"></i>' +
            '</a> ' +
            '<a href="#" class="e-date-edit control-icon">' +
            '<i class="fas fa-pen"></i>' +
            '</a>' +
            '</div>' +
            '</div>' +
            '<div class="exper-body">' +
            '<h6 class="d-flex justify-content-between">' +
            '<span>Öhdəliklər</span>' +
            '<div class="">' +
            '<a href="#" class="about-work-save control-icon main-back-color">' +
            '<i class="fas fa-check"></i>' +
            '</a>' +
            '<a href="#" class="about-work-edit control-icon ml-2 main-back-color">' +
            '<i class="fas fa-pen"></i>' +
            '</a>' +
            '<a href="#" class="experience-remove control-icon ml-2 main-back-color">' +
            '<i class="fas fa-trash-alt mr-2"></i>' +
            '</a>' +
            '</div>' +
            '</h6>' +
            '<span class="work-about">' +
            '<div class="about-work-edit-controls">' +
            '<textarea name="" maxlength="350" id="" rows="3" class="form-control"></textarea>' +
            '<small class="text-danger edit-about-work-error">İşdəki öhdəlikləriniz barədə danışın (ən azı 100 hərf)</small>' +
            '</div>' +

            '<span>' + exp.about + '</span>' +
            '</span>' +
            '</div>' +
            '</div>';

        //adding to body
        $('.experiences').prepend(newExperience);

        //clear inputs
        $('#search-input').val('');
        $('.c-name').val('');
        $('.c-id').val('');
        $('.start-date').val(null);
        $('.end-date').val(null);
        $('.exper-about').val('');
        $(".register-company").prop('checked', false);

        $(".search-area input").removeAttr("readonly");
        $(".search-company-logo").hide();
        $(".search-company-logo").attr('src', '');
        // enabling company register checkbox
        $('.register-company').removeAttr("disabled")

        $('.cancel').hide();
    }


    // ====================================== Editing <=====================

    // Edit About work
    function editAboutText(e) {
        e.preventDefault()

        $(this).parents('.exper').find('.work-about textarea').val($(this).parents('.exper').find('.work-about span').html().trim());
        $(this).parents('.exper-body').toggleClass('editing');
    }

    // Save changing in about
    function saveAboutText(e) {
        e.preventDefault();

        let about = $(this).parents('.exper').find('.work-about textarea').val();

        let experId = $(this).parents(".exper").attr('id');
        let url = $('.exper-edit-url').attr('data-url');

        if (checkEmpty(about, $(this).parents('.exper').find('.edit-about-work-error'), null, 100)) {

            let dataToSend = new FormData();
            dataToSend.append('about', about);
            dataToSend.append('id', experId);

            sendAjax(url,dataToSend);
            $(this).parents('.exper').find('.work-about span').html(about.trim());
            $(this).parents('.exper-body').removeClass('editing');
        }

    }


    // Editing start date
    function editStartDate(e) {
        e.preventDefault();

        let date = new Date($(this).parents('.exper').find('.s-date').val());

        let days = (date.getDate() > 9) ? date.getDate() : '0' + date.getDate();
        let month = (date.getMonth() + 1 > 9) ? date.getMonth() + 1 : '0' + (date.getMonth() + 1);

        $(this).parents('.exper').find('.s-date-edit-controls input').val(date.getFullYear() + '-' + month + '-' + days);

        $(this).parents('.start-date').addClass('editing');
        
    }


    //Save start date
    function saveStartDate(e) {
        e.preventDefault();

        let startDate = $('.s-date-edit-controls input').val();

        let experId = $(this).parents('.exper').attr('id');
        let url = $('.exper-edit-url').attr('data-url');


        if (checkEmpty(startDate, $(this).parents('.exper').find('.s-date-error'))) {
            let dataToSend = new FormData();
            dataToSend.append("startDate", startDate);
            dataToSend.append("id", experId);

            sendAjax(url, dataToSend);

            let startDateFormat = new Date(startDate);
            let days = (startDateFormat.getDate() > 9) ? startDateFormat.getDate() : '0' + startDateFormat.getDate();
            let month = (startDateFormat.getMonth() + 1 > 9) ? startDateFormat.getMonth() + 1 : '0' + (startDateFormat.getMonth() + 1);

            $(this).parents('.start-date').removeClass('editing');
            $(this).parents('.exper').find('.s-date').val(startDate.toString("yyyy-MM-dd"));
            $(this).parents('.exper').find('.start-date span').html(days + '-' + month + '-' + startDateFormat.getFullYear());
        }
    }



    // Editing end date
    function editEndDate(e) {
        e.preventDefault();

        let endDate = $(this).parents('.exper').find('.e-date').val();
        let date;

        date = new Date(endDate);
  

        let days = (date.getDate() > 10) ? date.getDate() : '0' + date.getDate();
        let month = (date.getMonth() + 1 > 10) ? date.getMonth() + 1 : '0' + (date.getMonth() + 1);
        $(this).parents('.exper').find('.e-date-edit-controls input').val(date.getFullYear() + '-' + month + '-' + days);

        $(this).parents('.end-date').addClass('editing');
    }



    //Save end date
    function saveEndDate(e) {
        e.preventDefault();

        let endDate = $(this).parents('.exper').find('.e-date-edit-controls input').val();
        let experId = $(this).parents('.exper').attr('id');
        let url = $('.exper-edit-url').attr('data-url');


        if (checkEmpty(endDate, $(this).parents('.exper').find('.e-date-error'))) {
            let dataToSend = new FormData();
            dataToSend.append("EndDate", endDate);
            dataToSend.append("id", experId);

            sendAjax(url, dataToSend);

            let endDateFormat = new Date(endDate);
            let days = (endDateFormat.getDate() > 9) ? endDateFormat.getDate() : '0' + endDateFormat.getDate();
            let month = (endDateFormat.getMonth() + 1 > 9) ? endDateFormat.getMonth() + 1 : '0' + (endDateFormat.getMonth() + 1);

            $(this).parents('.end-date').removeClass('editing');
            $(this).parents('.exper').find('.e-date').val(endDate.toString());
            $(this).parents('.exper').find('.end-date span').html(days + '-' + month + '-' + endDateFormat.getFullYear() );
        }
    }

    // Remove Experience
    function removeExperience(e) {
        e.preventDefault();

        if (confirm('Bu iş təcrübənizi silmək istəyirsiniz?')) {

            let id = $(this).parents('.exper').attr('id');
            let url = $('.exper-remove-url').attr('data-url');
            let dataToSend = new FormData();
            dataToSend.append('id', id);

            sendAjax(url, dataToSend);

            $(this).parents('.exper').remove();
        }
    }







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

                if (result.response != null) {

                    alert(result.response);
                }
            }
        });
    }




    // Function checking empty inputs
    function checkEmpty(element, error, checkboxState, length) {
        if (checkboxState == null && length == null) {
            if (element == '') {
                error.show();
                return false;
            }
            else {
                error.hide();
                return true;
            }
        }
        else if (checkboxState != null) {

            if (element == '' && checkboxState == false) {
                error.show();
                return false;
            }
            else {
                error.hide();
                return true;
            }
        }
        else if (length != null) {
            if (element.trim().length < length) {
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