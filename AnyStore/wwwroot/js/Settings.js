if (!SettingsContext)
    var SettingsContext = {};

(function (context) {

    context.currencyGrid = null;

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

        context.openEditCurrencyWindow = function () {

        };

        context.removeCurrency = function () {

        };

    };

})(SettingsContext);