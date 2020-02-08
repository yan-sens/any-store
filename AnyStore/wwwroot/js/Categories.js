if (!CategoryContext)
    var CategoryContext = {};

(function (context) {

    $("#dvCategories").kendoGrid({
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
            title: "Описание",
            width: 350
        }]
    }).data("kendoGrid");

})(CategoryContext);
