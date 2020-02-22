if (!SettingsContext)
    var SettingsContext = {};

(function (context) {

    context.currencyGrid = null;
    context.currencyWindow = null;
    context.IsCurrencyEdit = false;
    context.EditedCurrencyId = null;
    context.currencyTitleInput = null;
    context.currencyDisplayInput = null;

    context.init = function () {

        $(".as-nav-link[name=settings]").addClass("k-state-selected");       

        context.currencyGrid = $("#currency_grid").kendoGrid({
            dataSource: {
                transport: {
                    read: "/Settings/GetAllCurrencies"
                },
                pageSize: 20  
            },
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
                field: "display",
                title: "Отображение"
            }, {
                template: function (dataItem) {
                    return "<a class='k-button k-button-icontext' onclick='SettingsContext.openEditCurrencyWindow(\"" + dataItem.id + "\")'><span class='k-icon k-i-edit'></span></a>";
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
                context.removeCurrency(e.model.id);
            }
        }).data("kendoGrid");

        context.currencyWindow = $("#add_currency_window").kendoWindow({ width: 300 }).data("kendoWindow");
        context.currencyNameInput = $("#currency_name_inp").kendoMaskedTextBox().data("kendoMaskedTextBox");
        context.currencyDisplayInput = $("#currency_display_inp").kendoMaskedTextBox().data("kendoMaskedTextBox");

        $("#open_add_currency_window_btn").kendoButton({
            click: function (e) {
                context.IsCurrencyEdit = false;
                context.EditedCurrencyId = null;
                context.clearCurrencyForm();
                context.currencyWindow.title("Добавить валюту");
                context.currencyWindow.center().open();
            }
        });

        $("#save_currency_btn").kendoButton({
            click: function (e) {
                kendo.ui.progress($("#currency_form"), true);
                var data = {};
                var url = context.IsCurrencyEdit ? "/Settings/UpdateCurrency" : "/Settings/CreateCurrency";
                data["Id"] = context.EditedCurrencyId;
                data["Name"] = context.currencyNameInput.value();
                data["Display"] = context.currencyDisplayInput.value();

                $.post(url, data, function () {
                    kendo.ui.progress($("#currency_form"), false);
                    context.currencyGrid.dataSource.read();
                    context.currencyWindow.close();
                });
            }
        });

        $("#cancel_save_currency_btn").kendoButton({
            click: function (e) {
                context.currencyWindow.close();
            }
        });

    };

    context.openEditCurrencyWindow = function (id) {
        var item = context.currencyGrid.dataSource.data().find(x => x.id === id);
        context.IsCurrencyEdit = true;
        context.EditedCurrencyId = id;
        context.clearCurrencyForm();
        context.currencyNameInput.value(item.name);
        context.currencyDisplayInput.value(item.display);
        context.currencyWindow.center().open();
    };

    context.removeCurrency = function (id) {
        $.post("/Settings/RemoveCurrency", { id: id });
    };

    context.clearCurrencyForm = function () {
        context.currencyNameInput.value(null);
        context.currencyDisplayInput.value(null);
    };

})(SettingsContext);