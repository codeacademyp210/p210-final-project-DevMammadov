$(function(){

    /* ------------ Functions ------------- */

    let sortBar = $('.sort-menu');

    $(document).on('click','.search-tag-close',closeTag);


    /* open tag menu on Cv view main page */
    $(".filter-tags-opener").click(function(eve){

        $(this).next().slideToggle(200);
    
            if($(this).find("i").hasClass("fa-sliders-h")){
                $(this).find("i").removeClass("fa-sliders-h").addClass("fa-times");
            }
            else{
                $(this).find("i").removeClass("fa-times").addClass("fa-sliders-h");
            }
    
      });

    // =====================  Search Cv

    // declaring checked checkbox list
    let checkedChk = [];
    // ======= Search button click
    $('.left-sort-menu .btn-search').on('click',function(){

        sortBar.html('');// clear sorting bar ul for adding new tags
        $('.left-sort-menu .custom-chk input[type="checkbox"]').each(function(i,v){

            if($(v).prop('checked')){

                let iconName = $(v).parent().find('img').attr('title');

                if(!checkedChk.includes(iconName)){
                       checkedChk.push(iconName);
                    }

                $('.sort-menu').append("<li><div class='search-tag'><b>" + iconName + "</b><span class='search-tag-close'>x</span></div></li>");
            }
        });

        localStorage.clear();
        localStorage.setItem('checked', JSON.stringify(checkedChk));
        setUrlSendAjax();
    });

    // ============== Close one tag in tag line
    function closeTag(e){
     
        let $this = $(this);
        $this.parents('li').remove();
        
        // uncheck checkbox when close tag
        $('.left-sort-menu .custom-chk input[type="checkbox"]').each(function(i,v){
            let iconName = $(v).parent().find('img').attr('title');
            if(iconName == $this.parent().find('b').html()){

                $(v).prop('checked',false);
                checkedChk.splice(checkedChk.indexOf(iconName),1);
            }

        });

        localStorage.clear();
        localStorage.setItem('checked', JSON.stringify(checkedChk));
       // setUrlSendAjax();
    }



        //taking from local storage checked items
        // set checked to checked items after refresh
        let checkedItems = JSON.parse( localStorage.getItem('checked'));
        if(checkedItems != null){
            for (let i = 0; i < checkedItems.length; i++) {
                
                sortBar.html('');
                $('.left-sort-menu .custom-chk input[type="checkbox"]').each(function(i,v){

                    let iconName = $(v).parent().find('img').attr('title');
                    if( checkedItems.includes(iconName) ){
                        
                        $(v).prop('checked',true);
                        $('.sort-menu').append("<li><div class='search-tag'><b>"+ iconName +"</b><span class='search-tag-close'>x</span></div></li>")

                        $(v).parents('.filter-tags').slideDown(200);
                    }
        
                });       
            }
            //console.log( checkedItems )
        }





    // Set selected items to address bar
    function setUrlSendAjax() {

        let icons = "";
        let queryString;
        for (let item in checkedChk) {

            let icon = checkedChk[item].replace(" ", "-").toLowerCase();
            icons += icon + ',';
        }

        let myQuery = icons.substring(0, icons.length - 1, 1);

        if (history.pushState) {

            window.history.replaceState({ path: queryString }, "", "/Search/Cv/Tags/" + myQuery);
        } else {
            document.location.href = "Search/Cv/Tags/" + myQuery;
        }

        // Sort Persons
        $.ajax({
            url: $('.btn-search').attr('data-url'),
            type: "GET",
            data: { "tags": myQuery },
            success: function (response, status) {

               $("#partials").html(response);
            }
        });
    }




});