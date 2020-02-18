if (!ProductsContext)
    var ProductsContext = {};

(function (context) {

    context.DataGrid = null;
    context.ProductWindow = null;
    context.ProductNameInput = null;
    context.ProductTitleInput = null;
    context.ProductDescriptionTextArea = null;
    context.CategoriesDropPown = null;
    context.IsProductEdit = false;
    context.EditedProductId = null;
    context.ProductPriceInput = null;
    context.ProductImageUploader = null;
    context.ProductImagesUploader = null;
    context.ProductValidator = null;

    context.init = function () {

        $(".as-nav-link[name=products]").addClass("k-state-selected");    

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
            height: 700,
            sortable: true,
            selectable: true,
            filterable: true,
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
                field: "manufacture",
                title: "Производитель"
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
                context.removeProduct(e.model.id);
            }
        }).data("kendoGrid");

        context.CategoriesDropPown = $("#categories_ddl").kendoComboBox({
            dataSource: {
                transport: {
                    read: "/Admin/GetCategoriesForProduct"
                },
                pageSize: 25
            },
            dataTextField: "name",
            dataValueField: "id"
        }).data("kendoComboBox");

        context.ProductWindow = $("#add_window").kendoWindow({ width: 500 }).data("kendoWindow");
        context.ProductNameInput = $("#product_name_inp").kendoMaskedTextBox().data("kendoMaskedTextBox");
        context.ProductTitleInput = $("#product_title_inp").kendoMaskedTextBox().data("kendoMaskedTextBox");
        context.ProductPriceInput = $("#product_price_inp").kendoNumericTextBox().data("kendoNumericTextBox");
        context.ProductDescriptionTextArea = $("#product_description_ta").kendoEditor().data("kendoEditor");
        $("#open_add_product_window_btn").kendoButton({
            click: function (e) {
                context.IsProductEdit = false;
                context.EditedProductId = null;
                context.clearProductForm();
                context.ProductWindow.title("Добавить товар");
                context.ProductWindow.center().open();
            }
        });

        context.ProductImageUploader = $("#product_image").kendoUpload({
            localization: {
                select: "Главное фото"
            },
            multiple: false,
            select: context.onImageSelect,
            validation: {
                //maxFileSize: 300000,
                allowedExtensions: [".jpg", ".jpeg", ".png"]                   
            }
        });

        context.ProductImagesUploader = $("#product_images").kendoUpload({
            localization: {
                select: "Остальные фото"
            },
            upload: context.onImageSelect,
            validation: {
                //maxFileSize: 300000,
                allowedExtensions: [".jpg", ".jpeg", ".png"]    
            }
        });

        $("#save_product_btn").kendoButton({
            click: function (e) {
                if (context.ProductValidator.validate()) {
                    var url = "/Products/CreateProduct";
                    $.post(url, $('form').serialize(), function (data) {
                        context.ProductWindow.close();
                        context.DataGrid.dataSource.read();
                    });
                }                
            }
        });

        $("#cancel_save_product_btn").kendoButton({
            click: function (e) {
                context.ProductWindow.close();
            }
        });

        context.ProductValidator = $("#product_form").kendoValidator({
            messages: {
                // overrides the built-in message for the required rule
                required: "Это обязательно"
            }
        }).data("kendoValidator");

    };

    context.onImageSelect = function (e) {
        console.log(e);
    };

    context.openEditProductWindow = function () {
        console.log("openEditProductWindow");
    };

    context.removeProduct = function (id) {

    };

    context.clearProductForm = function () {
        context.ProductNameInput.value(null);
        context.ProductTitleInput.value(null);
        context.ProductDescriptionTextArea.value(null);
        context.CategoriesDropPown.value(null);
    };

})(ProductsContext);