$(function () {


    $(document).on('click', '.search-port .search-menu ul.sort-menu li a', searchPort)
    $(document).on('click', '.person .left-sort-menu ul li a', searchPerson)


    // Sort Portfolio index page
    function searchPort(e) {
        e.preventDefault();

        let sort = "";
        sort = $(this).attr('data-search');

        $.ajax({
            url: $(this).parents('.search-menu').attr('data-url'),
            type: "GET",
            data: { "sort" : sort},
            success: function (response, status) {
                
                $("#partials").html(response);
            }
        });
    };

    // Sort Persons
    function searchPerson(e) {
        e.preventDefault();
        
        let sort = "";
        sort = $(this).attr('data-search');

        $.ajax({
            url: $(this).parents('.search-menu').attr('data-url'),
            type: "GET",
            data: { "sort": sort },
            success: function (response, status) {

                $("#partials").html(response);
            }
        });

    }





})