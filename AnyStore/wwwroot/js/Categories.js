if (!CategoryContext)
    var CategoryContext = {};

(function (context) {

    context.DataGrid = null;
    context.CategoryWindow = null;

    context.init = function () {

        $(".as-nav-link[name=categories]").css("color", "#F46A1C");        

        context.DataGrid = $("#dvCategories").kendoGrid({
            dataSource: {
                transport: {
                    read: "/Admin/GetCategories"
                },

                pageSize: 25
            },
            height: 550,
            sortable: true,
            selectable: true,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [{
                field: "name",
                title: "Название"
            }, {
                field: "title",
                title: "Заголовок"
            }, {
                field: "description",
                title: "Описание"
            }]
        }).data("kendoGrid");

        context.CategoryWindow = $("#add_window").kendoWindow().data("kendoWindow");
        $("#add_category_btn").kendoButton({
            click: function (e) {
                context.CategoryWindow.center().open();
            }
        });
    };    

})(CategoryContext);
