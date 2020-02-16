if (!ProductsContext)
    var ProductsContext = {};

(function (context) {

    context.init = function () {

        $(".as-nav-link[name=products]").css("color", "#F46A1C");     

        context.DataGrid = $("#dvProducts").kendoGrid({
            dataSource: {
                transport: {
                    read: "/Products/GetProducts"
                },
                pageSize: 20,
                requestEnd: function () {
                    $('.k-grid-content tr').on("dblclick", function (e) {
                        console.log(e);
                    });
                }
            },
            height: 550,
            sortable: true,
            selectable: true,
            editable: "popup",
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            cellClose: function (e) {
                console.log(e);
            },
            columns: [{
                field: "name",
                title: "Название"
            }, {
                field: "title",
                title: "Заголовок"
            }, {
                field: "manufacture",
                title: "Производитель"
            }, {
                field: "purchasePrice",
                title: "Закупочная цена"
            }, {
                field: "sellingPrice",
                title: "Цена продажи"
            }, {
                template: function (dataItem) {
                    return "<a class='k-button k-button-icontext' onclick='ProductsContext.openEditProductWindow(\"" + dataItem.id + "\")'><span class='k-icon k-i-edit'></span></a>";
                },
                width: 56
            }, {
                command: [
                    {
                        name: "destroy",
                        text: ""
                    }],
                width: 56
            }],
            remove: function (e) {
                console.log("Removing...", e.model.name);
                context.removeCategory(e.model.id);
            }
        }).data("kendoGrid");

        context.openEditProductWindow = function () {
            console.log("openEditProductWindow");
        };

        context.removeCategory = function (id) {
            
        };

    };

})(ProductsContext);