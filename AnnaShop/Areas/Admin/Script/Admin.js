/// <reference path="../../../scripts/angular-1.3.9/angular.min.js" />

var admin = angular.module('admin', []);
//"customFilters",'datatables', 'ui.router'
admin.controller("CategoryController", function ($scope, $http) {
    // Get All Data
    $scope.GetAllData = function () {
        $http({
            method: "get",
            url: "/Admin/Category/GetAll_Category"
        }).then(function (response) {
            $scope.categories = response.data;
        }, function () {
            alert("Error");
        })
    };
    // Delete
    $scope.Delete = function (Cat) {
        if (confirm("Do you want to dalete this category?")) {
            $http({
                method: "post",
                url: "/Admin/Category/Delete_Category",
                datatype: "json",
                data: JSON.stringify(Cat)
            }).then(function (response) {
                $scope.GetAllData();
                toastr.success(response.data);
            })
        }
    };
    // Update
    $scope.Update = function (Cat) {
        $scope.CatId = Cat.Id;
        $scope.CatName = Cat.Name;
        document.getElementById("btnSave").setAttribute("value", "Update");
        document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
        document.getElementById("spn").innerHTML = "Update Category Information";
    };
    // Insert
    $scope.InsertData = function () {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Add") {
            $scope.Category = {};
            $scope.Category.Name = $scope.CatName;
            $http({
                method: "post",
                url: "/Category/Insert_Category",
                datatype: "json",
                data: JSON.stringify($scope.Category)
            }).then(function (response) {
                $scope.GetAllData();
                toastr.success(response.data);
                $scope.CatName = "";
                $scope.CatId = "";
            })
        }
        else {
            $scope.Category = {};
            $scope.Category.Name = $scope.CatName;
            $scope.Category.Id = $scope.CatId;
            $http({
                method: "post",
                url: "/Category/Update_Category",
                datatype: "json",
                data: JSON.stringify($scope.Category)
            }).then(function (response) {
                $scope.GetAllData();
                toastr.success(response.data);
                $scope.CatName = "";
                $scope.CatId = "";
                document.getElementById("btnSave").setAttribute("value", "Add");
                document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
                document.getElementById("spn").innerHTML = "Add New Categorye";
            })
        }
    }
});
admin.controller("ProductController", function ($scope, $http) {
    // Get All Data
    $scope.GetAllData = function () {
        $http({
            method: "get",
            url: "/Admin/AdminProduct/GetAll_Product"
        }).then(function (response) {
            $scope.products = response.data;
        }, function () {
            alert("Error");
        })
    };
    // Get All Category
    $scope.GetAllCat = function () {
        $http({
            method: "get",
            url: "/Admin/Category/GetAll_Category"
        }).then(function (response) {
            $scope.categories = response.data;
        }, function () {
            alert("Error");
        })
    };
    // Delete
    $scope.Delete = function (Cat) {
        if (confirm("Do you want to dalete this product?")) {
            $http({
                method: "post",
                url: "/Admin/AdminProduct/Delete_product",
                datatype: "json",
                data: JSON.stringify(Cat)
            }).then(function (response) {
                $scope.GetAllData();
                toastr.success(response.data);
            })
        }
    };
    // upload file
    $scope.uploadFile = function (files) {
        var fd = new FormData();
        //Take the first selected file
        fd.append("file", files[0]);

        $http.post('/Admin/AdminProduct/UploadFile', fd, {
            withCredentials: true,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        }).success(function (data, status, headers, config) {
            $scope.pAvartarPath = data.filePath;
        }).error(function (data, status, headers, config) {

        } );
    };
    //Action
    $scope.InsertAction = function () {
        document.getElementById("btnSave").setAttribute("value", "Add");
        $scope.pName = "";
        $scope.pAvt = "";
        $scope.pColor = "";
        $scope.pSize = "";
        $scope.pQuantity = "";
        $scope.pState = "";
        $scope.pPrice = "";
        $scope.pAvartarPath = "";
        $scope.pCategory = "";
        document.getElementById("user_img").setAttribute("src", "");
    }
    // Update
    $scope.Update = function (pro) {
        $scope.pName = pro.Name;
        $scope.pPrice = pro.Price;
        $scope.pAvartarPath = pro.AvatarImage;
        $scope.pColor = pro.Color;
        $scope.pSize = pro.Size;
        $scope.pQuantity = pro.Quantity;
        $scope.pState = pro.State;
        $scope.pCategory = pro.Category;
        document.getElementById("EmpID_").value = pro.Id;
        document.getElementById("btnSave").setAttribute("value", "Update");
        document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
        document.getElementById("spn").innerHTML = "Update Category Information";
    };
    // Insert
    $scope.InsertData = function () {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Add") {
            $scope.Product = {};
            $scope.Product.Name = $scope.pName;
            console.log($scope.pName);
            $scope.Product.Price = $scope.pPrice;
            $scope.Product.Color = $scope.pColor;
            $scope.Product.Size = $scope.pSize;
            $scope.Product.Quantity = $scope.pQuantity;
            $scope.Product.State = $scope.pState;
            $scope.Product.Category = $scope.pCategory;
            $scope.Product.AvatarImage = $scope.pAvartarPath;
            $http({
                method: "post",
                url: "/Admin/AdminProduct/Insert_product",
                datatype: "json",
                data: JSON.stringify($scope.Product)
            }).then(function (response) {
                $scope.GetAllData();
                toastr.success(response.data);
                $scope.pName = "";
                $scope.pAvt = "";
                $scope.pColor = "";
                $scope.pSize = "";
                $scope.pQuantity = "";
                $scope.pState = "";
                $scope.pPrice = "";
            })
        }
        else {
            $scope.Product = {};
            $scope.Product.Name = $scope.pName;
            $scope.Product.Price = $scope.pPrice;
            $scope.Product.AvatarImage = $scope.pAvartarPath;
            $scope.Product.Color = $scope.pColor;
            $scope.Product.Size = $scope.pSize;
            $scope.Product.Quantity = $scope.pQuantity;
            $scope.Product.Category = $scope.pCategory;
            $scope.Product.State = $scope.pState;
            $scope.Product.Id = document.getElementById("EmpID_").value; 
            $http({
                method: "post",
                url: "/Admin/AdminProduct/Update_product",
                datatype: "json",
                data: JSON.stringify($scope.Product)
            }).then(function (response) {
                $scope.GetAllData();
                toastr.success(response.data);
                $scope.pName = "";
                $scope.pAvt = "";
                $scope.pColor = "";
                $scope.pSize = "";
                $scope.pQuantity = "";
                $scope.pState = "";
                $scope.pPrice = "";
                document.getElementById("btnSave").setAttribute("value", "Add");
                document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
                document.getElementById("spn").innerHTML = "Add New Product";
            })
        }
    }
});
admin.controller("SaleController", function ($scope, $rootScope, $http) {

    // Get All Data
    $scope.GetAllOrder = function () {
        $http({
            method: "get",
            url: "/Admin/SaleManager/GetAllOrder"
        }).then(function (response) {
            $scope.orders = response.data;
        }, function () {
            alert("Error");
        });
       
    };
    $http.get('/Admin/OrderDetails/GetDetailOrderByOrder').then(function (d) {
        $rootScope.list_detailOrder = d.data;
    }, function (error) { alert("Failed"); });
});


// chart
admin.controller("ThongKeController", function ($scope, $http) {
    // Set new default font family and font color to mimic Bootstrap's default styling
    Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
    Chart.defaults.global.defaultFontColor = '#292b2c';

    //doanh thu thang trong nam
    $scope.getData = function () {
        $http({
            method: "get",
            url: "/Admin/Manager/getDoanhThuThang"
        }).then(function (response) {
            var today = new Date();
            $scope.year = today.getFullYear();
            var month = [];
            for (i = 1; i < 13; i++) {
                month.push(i + "/" + $scope.year );
            }
            // Bar Chart Example
            var ctx = document.getElementById("myBarChart");
            var myLineChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: month,
                    datasets: [{
                        label: "doanh thu",
                        backgroundColor: "rgba(2,117,216,1)",
                        borderColor: "rgba(2,117,216,1)",
                        data: response.data.Dts
                    }],
                },
                options: {
                    scales: {
                        xAxes: [{
                            time: {
                                unit: 'month'
                            },
                            gridLines: {
                                display: false
                            },
                            ticks: {
                                maxTicksLimit: 12
                            }
                        }],
                        yAxes: [{
                            ticks: {
                                min: 0,
                                max: response.data.dtMax,
                                maxTicksLimit: 7
                            },
                            gridLines: {
                                display: true
                            }
                        }],
                    },
                    legend: {
                        display: false
                    }
                }
            });



        })
    };
    //doanh thu theo loai san pham
    $scope.getChartLoaiSP = function() {
        $http({
            method: "get",
            url: "/Admin/Manager/getDtLoaiSp"
        }).then(function (response) {
            var ctx = document.getElementById("myPieChart");
            var myPieChart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: response.data.dsLSP, //loai sp
                    datasets: [{
                        data: response.data.dsPT, //phan tram
                        backgroundColor: ['#007bff', '#dc3545', '#ffc107', '#28a745', "#8e44ad", "#e74c3c", "#16a085", "#e66767", "#778beb", "#3dc1d3"],
                    }],
                },
            });
        })
    };
    //sp ban chay -- chưa làm :(((
    $scope.getSpBanChay = function () {
        $http({
            method: "get",
            url: "/Admin/Manager/getDoanhThuThang"
        }).then(function (response) {

        })
    };
});
