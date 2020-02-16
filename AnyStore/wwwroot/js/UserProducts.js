if (!UserProductsContext)
    var UserProductsContext = {};

(function (context) {

    context.categoryId = null;
    context.productsDataSource = null;
    context.pager = null;

    context.viewModel = new kendo.observable({
        pagerPageSize: 15,
        pagerSort: {
            field: {
                text: "Название",
                value: "name"
            },
            dir: {
                text: "По возрастанию",
                value: "asc"
            }
        }
    });

    context.init = function (categoryId) {

        context.categoryId = categoryId;

        context.productsDataSource = new kendo.data.DataSource({
            transport: {
                read: "/Products/GetProductsByCategoryId?categoryId=" + context.categoryId
            },
            sort: context.getSort(),
            requestEnd: function (data) {
                if (data.response)
                    setTimeout(function () {
                        context.renderProducts(context.productsDataSource.view());
                    });
            },
            pageSize: context.viewModel.pagerPageSize
        });

        context.productsDataSource.read();

        context.pager = $("#products_pager").kendoPager({
            dataSource: context.productsDataSource,
            change: function () {
                context.renderProducts(context.productsDataSource.view());                
                $('html, body').animate({ scrollTop: 400 }, 1000);
            }
        }).data("kendoPager");

        $("#show_ddl").kendoDropDownList({
            dataSource: [15, 30, 45, 60],
            change: function (e) {
                context.productsDataSource.pageSize(context.viewModel.pagerPageSize);
                context.renderProducts(context.productsDataSource.view());                
            }
        });

        $("#sort_ddl").kendoDropDownList({
            dataSource: [{
                text: "Название",
                value: "name"
            }, {
                text: "Производитель",
                value: "manufacture"
            }, {
                text: "Цена",
                value: "sellingPrice"
            }],
            dataTextField: "text",
            dataValueField: "value",
            change: function (e) {
                context.productsDataSource.sort(context.getSort()); 
                context.renderProducts(context.productsDataSource.view());
            }
        }); 
        
        $("#dir_ddl").kendoDropDownList({
            dataSource: [{
                text: "По возрастанию",
                value: "asc"
            }, {
                text: "По убыванию",
                value: "desc"
            }],
            dataTextField: "text",
            dataValueField: "value",
            change: function (e) {
                context.productsDataSource.sort(context.getSort());
                context.renderProducts(context.productsDataSource.view());
            }
        });

        kendo.bind($("#show_ddl"), context.viewModel);
        kendo.bind($("#sort_ddl"), context.viewModel);
        kendo.bind($("#dir_ddl"), context.viewModel);
    };

    context.getSort = function () {
        return { field: context.viewModel.pagerSort.field.value, dir: context.viewModel.pagerSort.dir.value};
    };

    context.renderProducts = function (products) {
        $(".listview").children().remove();
        $.each(products, function (i, d) {
            var productHtml1 = $("#product_template").html();
            var productHtml2 = $("#product_template2").html();
            productHtml1 = productHtml1.replace(/{{productName}}/g, d.title);
            productHtml2 = productHtml2.replace(/{{productName}}/g, d.title);
            productHtml1 = productHtml1.replace(/{{price}}/g, d.sellingPrice);
            productHtml2 = productHtml2.replace(/{{price}}/g, d.sellingPrice);
            productHtml1 = productHtml1.replace(/{{id}}/g, d.id);
            productHtml2 = productHtml2.replace(/{{id}}/g, d.id);
            $("#display-1-1 .listview").append(productHtml1);
            $("#display-1-2 .listview").append(productHtml2);
        });        
    };

})(UserProductsContext);