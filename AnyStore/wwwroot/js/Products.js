if (!ProductsContext)
    var ProductsContext = {};

(function (context) {

    context.DataGrid = null;
    context.ProductWindow = null;
    context.ProductNameInput = null;
    context.ProductTitleInput = null;
    context.ProductDescriptionTextArea = null;
    context.ProductDescription2TextArea = null;
    context.CategoriesDropPown = null;
    context.IsProductEdit = false;
    context.EditedProductId = null;
    context.ProductPriceInput = null;
    context.ProductImageUploader = null;
    context.ProductImagesUploader = null;
    context.ProductImageListView = null;
    context.ProductImagesListView = null;
    context.ProductValidator = null;
    context.CurrenciesDropPown = null;

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
            columns: [{
                field: "name",
                title: "Название"
            }, {
                field: "title",
                title: "Заголовок"
            }, {
                field: "currencyName",
                title: "Валюта"
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

        context.CurrenciesDropPown = $("#currencies_ddl").kendoComboBox({
            dataSource: {
                transport: {
                    read: "/Settings/GetAllCurrencies"
                },
                pageSize: 25
            },
            dataTextField: "name",
            dataValueField: "id"
        }).data("kendoComboBox");

        context.ProductWindow = $("#add_window").kendoWindow({ width: 1000 }).data("kendoWindow");
        context.ProductNameInput = $("#product_name_inp").kendoMaskedTextBox().data("kendoMaskedTextBox");
        context.ProductTitleInput = $("#product_title_inp").kendoMaskedTextBox().data("kendoMaskedTextBox");
        context.ProductPriceInput = $("#product_price_inp").kendoNumericTextBox().data("kendoNumericTextBox");
        context.ProductDescriptionTextArea = $("#product_description_ta").kendoEditor({
            tools: CommonHelper.getEditorTools()
        }).data("kendoEditor");
        context.ProductDescription2TextArea = $("#product_description2_ta").kendoEditor({
            tools: CommonHelper.getEditorTools()
        }).data("kendoEditor");
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
            showFileList: false,
            validation: {
                //maxFileSize: 300000,
                allowedExtensions: [".jpg", ".jpeg", ".png"]                   
            }
        }).data("kendoUpload");

        context.ProductImagesUploader = $("#product_images").kendoUpload({
            localization: {
                select: "Остальные фото"
            },
            select: context.onImagesSelect,
            showFileList: false,
            validation: {
                //maxFileSize: 300000,
                allowedExtensions: [".jpg", ".jpeg", ".png"]
            }
        }).data("kendoUpload");

        context.ProductImageListView = $("#product_image_listView").kendoListView({
            template: kendo.template($("#image_template").html()),
            selectable: true,
            dataSource: {
                schema: {
                    model: {
                        id: "id",
                        fields: {
                            id: { type: "string" },
                            src: { type: "string" }
                        }
                    }
                }
            }
        }).data("kendoListView");

        context.ProductImagesListView = $("#product_images_listView").kendoListView({
            template: kendo.template($("#image_template").html()),
            selectable: true,
            dataSource: {
                schema: {
                    model: {
                        id: "id",
                        fields: {
                            id: { type: "string" },
                            src: { type: "string" }
                        }
                    }
                }
            }
        }).data("kendoListView");

        context.getBase64 = function (file) {
            return new Promise((resolve, reject) => {
                const reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = () => {
                    let encoded = reader.result.toString().replace(/^data:(.*,)?/, '');
                    if ((encoded.length % 4) > 0) {
                        encoded += '='.repeat(4 - (encoded.length % 4));
                    }
                    resolve(encoded);
                };
                reader.onerror = error => reject(error);
            });
        };

        $("#save_product_btn").kendoButton({
            click: function (e) {
                if (context.ProductValidator.validate() && context.ProductImageListView.dataSource.data().length > 0) {
                    kendo.ui.progress($("#product_form"), true);
                    var data = {};
                    var url = context.IsProductEdit ? "/Products/UpdateProduct" : "/Products/CreateProduct";
                    data["Id"] = context.EditedProductId;
                    data["Name"] = context.ProductNameInput.value();
                    data["Title"] = context.ProductTitleInput.value();
                    data["Description"] = context.ProductDescriptionTextArea.value();
                    data["AdditionalDescription"] = context.ProductDescription2TextArea.value();
                    data["SellingPrice"] = context.ProductPriceInput.value();
                    data["CategoryId"] = context.CategoriesDropPown.value();
                    data["CurrencyId"] = context.CurrenciesDropPown.value();
                    data["Image"] = context.ProductImageListView.dataSource.data()[0].src;
                    data["Images"] = context.ProductImagesListView.dataSource.data().map(x => x.src);

                    $.post(url, data, function () {
                        kendo.ui.progress($("#product_form"), false);
                        context.DataGrid.dataSource.read();
                        context.ProductWindow.close();
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
                required: "Это обязательно"
            }
        }).data("kendoValidator");

    };

    context.onImageSelect = function (e) {
        context.getBase64(e.files[0].rawFile).then(x => {
            context.ProductImageListView.dataSource.data()
                .forEach(d => context.ProductImageListView.dataSource.remove(d));
            context.ProductImageListView.dataSource.add({
                id: kendo.guid(),
                src: x
            });
        });
    };

    context.onImagesSelect = function (e) {
        context.getBase64(e.files[0].rawFile).then(x => {
            context.ProductImagesListView.dataSource.add({
                id: kendo.guid(),
                src: x
            });
        });
    };

    context.openEditProductWindow = function (id) {
        var item = context.DataGrid.dataSource.data().find(x => x.id === id);
        context.clearProductForm();

        context.IsProductEdit = true;
        context.EditedProductId = id;
        context.ProductNameInput.value(item.name);
        context.ProductTitleInput.value(item.title);
        context.ProductPriceInput.value(item.sellingPrice);
        context.ProductDescriptionTextArea.value(item.description);
        context.CategoriesDropPown.value(item.categoryId);
        context.CurrenciesDropPown.value(item.currencyId);
        kendo.ui.progress($("#product_form"), true);
        context.ProductWindow.title("Редактировать товар");
        context.ProductImageListView.dataSource.add({
            id: kendo.guid(),
            src: item.image
        });
        $.get("/Products/GetProductImagesByProductId?productId=" + id, function (data) {
            $.each(data, function (i, d) {
                context.ProductImagesListView.dataSource.add({
                    id: kendo.guid(),
                    src: d.image
                });
            });

            kendo.ui.progress($("#product_form"), false);
        });
        context.ProductWindow.center().open();
    };

    context.removeProduct = function (id) {
        $.post("/Products/RemoveProduct", { id: id });
    };

    context.clearProductForm = function () {
        context.ProductPriceInput.value(null);
        context.ProductNameInput.value(null);
        context.ProductTitleInput.value(null);
        context.ProductDescriptionTextArea.value(null);
        context.CategoriesDropPown.value(null);
        context.CurrenciesDropPown.value(null);
        context.ProductImageListView.dataSource.data().empty();
        context.ProductImagesListView.dataSource.data().empty();
    };

})(ProductsContext);