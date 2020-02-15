console.log($("#categories_treeview li"));
$("#categories_treeview li").hover(function () {
    $(this).addClass("hover");
}, function () {
    //if (!$(this).closest("li").hasClass("hover")) {
        $(this).removeClass("hover");
    //}    
});