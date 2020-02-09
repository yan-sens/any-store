if (!CategoryContext)
    var CategoryContext = {};

(function (context) {

    context.DataGrid = null;
    context.CategoryWindow = null;
    context.CategoryNameInput = null;
    context.CategoryTitleInput = null; 
    context.CategoryDescriptionTextArea = null;
    context.CategoriesDropPown = null;
    context.CategoryChildSwitch = null;

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
            editable: "incell",
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
                field: "description",
                title: "Описание"
            }, {
                command: [
                {
                    name: "destroy",
                    text: ""
                }],
                width: 56
            }],
            remove: function (e) {
                console.log("Removing", e.model.name);
            }
        }).data("kendoGrid");

        context.CategoryWindow = $("#add_window").kendoWindow({
            width: 500,
            title: "Добавить категорию"
        }).data("kendoWindow");
        context.CategoryNameInput = $("#category_name_inp").kendoMaskedTextBox().data("kendoMaskedTextBox");
        context.CategoryTitleInput = $("#category_title_inp").kendoMaskedTextBox().data("kendoMaskedTextBox");
        context.CategoryDescriptionTextArea = $("#category_description_ta").kendoEditor().data("kendoEditor");        
        context.CategoryChildSwitch = $("#child_switch_cb").kendoSwitch({
            change: function (e) {
                console.log(e.checked);
            }
        }).data("kendoSwitch");
        $("#open_add_category_window_btn").kendoButton({
            click: function (e) {
                context.CategoryWindow.center().open();
            }
        });

        $("#save_category_btn").kendoButton({
            click: function (e) {
                console.log("save category...");
            }
        });

        $("#cancel_save_category_btn").kendoButton({
            click: function (e) {
                context.CategoryWindow.close();
            }
        });

        context.CategoriesDropPown = $("#categories_ddl").kendoComboBox({
            dataSource: {
                transport: {
                    read: "/Admin/GetCategories"
                },
                pageSize: 25
            },
            dataTextField: "name",
            dataValueField: "id"
        }).data("kendoComboBox");
        
    };    

})(CategoryContext);
