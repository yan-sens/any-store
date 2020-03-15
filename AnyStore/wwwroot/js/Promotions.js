if (!PromotionContext)
    var PromotionContext = {};

(function (context) {

    context.DataGrid = null;
    context.promotionWindow = null;
    context.isPromotionEdit = false;
    context.editedPromotionId = null;
    context.promotionRateInput = null;
    context.productsWindow = null;
    context.promotionProductsGrid = null;
    context.editedProductsPromotionId = null;

    context.init = function () {

        $(".as-nav-link[name=promotions]").addClass("k-state-selected");    

        context.DataGrid = $("#dvPromotions").kendoGrid({
            dataSource: {
                transport: {
                    read: "/Promotion/GetPromotions"
                },
                pageSize: 20
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
                field: "rate",
                title: "Скидка",
                template: function (dataItem) {
                    return "<span>" + dataItem.rate + " %</span>";
                }
            },{
                field: "startDate",
                title: "Начало",
                template: function (dataItem) {
                    return "<span>" + dayjs(dataItem.startDate).format("DD-MM-YYYY") + "</span>";
                }
            }, {
                field: "endDate",
                title: "Конец",
                template: function (dataItem) {
                    return "<span>" + dayjs(dataItem.endDate).format("DD-MM-YYYY") + "</span>";
                }
            }, {
                template: function (dataItem) {
                    return "<a class='k-button k-primary' onclick='PromotionContext.openProductsWindow(\"" + dataItem.id + "\", \"" + dataItem.name +"\")'>Товары</a>";
                },
                width: 96
            }, {
                template: function (dataItem) {
                    return "<a class='k-button k-button-icontext' onclick='PromotionContext.openEditPromotionWindow(\"" + dataItem.id + "\")'><span class='k-icon k-i-edit'></span></a>";
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
                context.removePromotion(e.model.id);
            }
        }).data("kendoGrid");

        context.promotionProductsGrid = $("#dvPromotionProducts").kendoGrid({
            dataSource: {
                transport: {
                    read: "/Products/GetProducts"
                },
                pageSize: 20
            },
            height: 700,
            sortable: true,
            filterable: true,
            selectable: true,
            groupable: true,
            pageable: {
                refresh: true,
                pageSizes: true,
                buttonCount: 5
            },
            columns: [{
                field: "name",
                title: "Название"
            }, {
                field: "categoryName",
                title: "Категория"
            },{
                template: function (dataItem) {
                    return "<input type='checkbox' onchange='PromotionContext.promotionProductChange(\"" + dataItem.id + "\")' class='k-checkbox k-primary prom-map-cb'>";
                },
                width: 56
            }]
        }).data("kendoGrid");

        context.promotionWindow = $("#add_window").kendoWindow({ width: 300 }).data("kendoWindow");
        context.productsWindow = $("#products_window").kendoWindow({ width: 800 }).data("kendoWindow");        
        context.promotionRateInput = $("#promotion_rate_inp").kendoNumericTextBox({ min: 1, max: 100, value: 1, step: 1, format: "custom"}).data("kendoNumericTextBox");
        context.promotionNameInput = $("#promotion_name_inp").kendoMaskedTextBox().data("kendoMaskedTextBox");
        context.promotionStartInput = $("#promotion_start_inp").kendoDatePicker().data("kendoDatePicker");
        context.promotionEndInput = $("#promotion_end_inp").kendoDatePicker().data("kendoDatePicker");

        $("#open_add_promotion_window_btn").kendoButton({
            click: function (e) {
                context.isPromotionEdit = false;
                context.editedPromotionId = null;
                context.clearPromotionForm();
                context.promotionWindow.title("Добавить акцию");
                context.promotionWindow.center().open();
            }
        });

        $("#save_promotion_btn").kendoButton({
            click: function (e) {
                kendo.ui.progress($("#add_window"), true);
                var data = {};
                var url = context.isPromotionEdit ? "/Promotion/UpdatePromotion" : "/Promotion/AddPromotion";
                data["Id"] = context.editedPromotionId;
                data["Name"] = context.promotionNameInput.value();
                data["Rate"] = context.promotionRateInput.value();
                data["StartDate"] = context.promotionStartInput.value() !== null ? context.promotionStartInput.value().toJSON() : (new Date()).toJSON();
                data["EndDate"] = context.promotionEndInput.value() !== null ? context.promotionEndInput.value().toJSON() : (new Date()).toJSON();

                $.post(url, data, function () {
                    kendo.ui.progress($("#add_window"), false);
                    context.DataGrid.dataSource.read();
                    context.promotionWindow.close();
                });
            }
        });

        $("#cancel_save_promotion_btn").kendoButton({
            click: function (e) {
                context.promotionWindow.close();
            }
        });

        $.get("/Products/GetPromotionProducts",null, function (data) {
            console.log(data);
        });
    };

    context.promotionProductChange = function (id){
        kendo.ui.progress($("#products_window"), true);
        var data = {};
        data["ProductId"] = id;
        data["PromotionId"] = context.editedProductsPromotionId;
        data["Active"] = $(event.target)[0].checked;

        $.post("/Promotion/UpdatePromotionProduct", data, function () {
            kendo.ui.progress($("#products_window"), false);
        });
    };

    context.openEditPromotionWindow = function (id) {
        var item = context.DataGrid.dataSource.data().find(x => x.id === id);
        context.clearPromotionForm();

        context.isPromotionEdit = true;
        context.editedPromotionId = id;

        context.promotionNameInput.value(item.name);
        context.promotionRateInput.value(item.rate);
        context.promotionStartInput.value(item.startDate);
        context.promotionEndInput.value(item.endDate);

        context.promotionWindow.title("Редактировать акцию");
        context.promotionWindow.center().open();
    };

    context.openProductsWindow = function (id, name) {
        context.editedProductsPromotionId = id;
        context.productsWindow.title("Товары для акции '" + name + "'");
        context.productsWindow.center().open();
        $.each($(".prom-map-cb"), function (i, d) { d.checked = false; });
        $.get("/Promotion/GetPromotionProducts", { promotionId: id }, function (data) {
            data.forEach(function (x) {
                var dataItem = context.promotionProductsGrid.dataSource.get(x.productId);
                var row = context.promotionProductsGrid.tbody.find("tr[data-uid='" + dataItem.uid + "']");
                if (row.length > 0)
                    $(row).find(".prom-map-cb")[0].checked = true;
            });
        });
    };

    context.removePromotion = function (id) {
        $.post("/Promotion/RemovePromotion", { id: id });
    };

    context.clearPromotionForm = function () {
        context.promotionNameInput.value(null);
        context.promotionRateInput.value(1);
        context.promotionStartInput.value(null);
        context.promotionEndInput.value(null);
    };

})(PromotionContext);