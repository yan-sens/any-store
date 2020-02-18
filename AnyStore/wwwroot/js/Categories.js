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
    context.IsCategoryEdit = false;
    context.EditedCategoryId = null;

    context.init = function () {

        $(".as-nav-link[name=categories]").addClass("k-state-selected");        

        context.DataGrid = $("#dvCategories").kendoGrid({
            dataSource: {
                transport: {
                    read: "/Admin/GetCategories"
                },
                pageSize: 20,
                requestEnd: function () {
                    $('.k-grid-content tr').on("dblclick", function (e) {
                        console.log(e);
                    });
                }
            },
            height: 700,
            sortable: true,
            filterable: true,
            selectable: true,
            groupable: true,
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
                field: "parentCategoryName",
                title: "Родительская категория"
            }, {
                template: function (dataItem) {
                    return "<a class='k-button k-button-icontext' onclick='CategoryContext.openEditCategoryWindow(\"" + dataItem.id + "\")'><span class='k-icon k-i-edit'></span></a>";
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

        context.CategoryWindow = $("#add_window").kendoWindow({ width: 500 }).data("kendoWindow");
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
                context.IsCategoryEdit = false;
                context.EditedCategoryId = null;
                context.clearCategoryForm();
                context.CategoryChildSwitch.enable(true);
                context.CategoryWindow.title("Добавить категорию");
                context.CategoryWindow.center().open();
            }
        });

        $("#save_category_btn").kendoButton({
            click: function (e) {
                var data = {};
                var url = context.IsCategoryEdit ? "/Admin/UpdateCategory" : "/Admin/AddCategory";
                data["Id"] = context.EditedCategoryId;
                data["Name"] = context.CategoryNameInput.value();
                data["Title"] = context.CategoryTitleInput.value();
                data["Description"] = context.CategoryDescriptionTextArea.value();
                data["HasChildren"] = context.CategoryChildSwitch.value();
                data["ParentCategoryId"] = context.CategoriesDropPown.value();
                $.post(url, data, function (data) {
                    context.CategoryWindow.close();
                    context.DataGrid.dataSource.read();
                    context.CategoriesDropPown.dataSource.read();
                });
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
                    read: "/Admin/GetCategoriesWithChild"
                },
                pageSize: 25
            },
            dataTextField: "name",
            dataValueField: "id"
        }).data("kendoComboBox");

    };    

    context.removeCategory = function (id) {
        $.post("/Admin/RemoveCategory", {id: id});
    };

    context.openEditCategoryWindow = function (id) {
        var item = context.DataGrid.dataSource.data().find(x => x.id === id);
        context.IsCategoryEdit = true;
        context.EditedCategoryId = id;
        context.clearCategoryForm();
        context.CategoryNameInput.value(item.name);
        context.CategoryTitleInput.value(item.title);
        context.CategoryDescriptionTextArea.value(item.description);
        context.CategoriesDropPown.value(item.parentCategoryId);
        context.CategoryChildSwitch.value(item.hasChildren);
        context.CategoryChildSwitch.enable(false);
        context.CategoriesDropPown.dataSource.filter({ field: "id", operator: "neq", value: id });
        context.CategoryWindow.title("Редактировать категорию");
        context.CategoryWindow.center();
        context.CategoryWindow.open();
    };

    context.clearCategoryForm = function () {
        context.CategoryNameInput.value(null);
        context.CategoryTitleInput.value(null);
        context.CategoryDescriptionTextArea.value(null);
        context.CategoriesDropPown.value(null);
        context.CategoryChildSwitch.value(null);
    };

})(CategoryContext);
