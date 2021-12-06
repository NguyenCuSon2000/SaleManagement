/// <reference path="../scripts/angular.min.js" />

var app = angular.module('app', []);
app.controller("MenuController", function ($scope, $http) {
    $http.get('/Home/GetCategory').then(function (d) {
        $scope.Categories = d.data;
    },
        function (error) { alert("Failed"); });
});
app.controller("NewProductCtrl", function ($scope, $http) {
    $http.get('/Home/GetNewProduct').then(function (d) {
        $scope.NewProducts = d.data;
    },
        function (error) { alert("Failed"); });
});
app.controller("ProductByCategory", function ($rootScope, $http) {
    $http.get('/Product/GetAll_product').then(function (d) {
        $rootScope.Products = d.data;
    }, function (error) { alert("Failed"); });

});
app.controller("AllProduct", function ($scope, $http) {
    $scope.getAll = function () {
        $http.get('/Product/GetAll').then(function (d) {
            $scope.Products = d.data;
        }, function (error) { alert("Failed"); });
    }
});

app.controller("NewsCtrl", function ($scope, $http) {
    $scope.getNewNews = function () {
        $http.get('/News/GetNews').then(function (d) {
            $scope.Newses = d.data;
        },
            function (error) { toastr.error("Failed"); });
    }
    $scope.getAllNews = function () {
        $http.get('/News/GetAllNews').then(function (d) {
            $scope.AllNewses = d.data;
        },
            function (error) { toastr.error("Failed"); });
    }
});
// Login/logout
app.controller("LoginController", function ($scope, $http) {
    // login
    $scope.login = function () {

        $http.post("/Login/userLogin?tentk=" + $scope.uid + "&mk=" + $scope.pwd).then(function (d) {
            if (d.data == '"1"') {
                if ($scope.uid == "annie") {
                    window.location.href = '/Admin/Manager/Index';
                }
                else {
                    window.location.href = '/Home/Index';
                }
            }
            else {
                alert(d.data);
            }
        }, function (error) {
            alert("failed");
        });
    }
    // sign up
    $scope.register = function () {
        $scope.addAcc = {};
        $scope.addAcc.Name = $scope.name;
        $scope.addAcc.Email = $scope.mail;
        $scope.addAcc.UserName = $scope.uid;
        $scope.addAcc.PassWord = $scope.pwd;
        $scope.addAcc.Phone = $scope.phone;
        $http({
            method: 'POST',
            url: '/Login/AddAcc',
            data: JSON.stringify($scope.addAcc)
        }).then(function (result) {
            alert(result.data);
        });
    }
    // update
    $scope.update = function () {
        $scope.addAcc = {};
        $scope.addAcc.Name = $scope.name;
        $scope.addAcc.Email = $scope.mail;
        $scope.addAcc.UserName = $scope.uid;
        $scope.addAcc.PassWord = $scope.pwd;
        $scope.addAcc.Phone = $scope.phone;
        $http({
            method: 'POST',
            url: '/Login/AddAcc',
            data: JSON.stringify($scope.addAcc)
        }).then(function (result) {
            alert(result.data);
        });
    }
    // logout
    $scope.LogOut = function () {
        $http.post("/Login/Logout").then(function (d) {
            window.location.href = '/Login/Index';
        }, function (error) {
            alert("failed");
        });
    };
    $scope.hi = false;
    $scope.Get_UserLogged = function () {
        $http.post("/Login/Get_UserLogged").then(function (d) {
            if (d.data != "") {
                $scope.hi = true;
                $scope.User = d.data;
            }
        }, function (error) {
            alert("failed");
        });
    };

});
// Product
app.controller("ProductDetail", function ($scope, $http) {
    $scope.getProductDetail = function () {
        $http.get('/Product/GetSingle').then(function (d) {
            $scope.Product = d.data;
        }, function (error) { alert("Failed"); });
    };

    $scope.addCart = function (product) {
        var checkSize = document.getElementsByName("size");
        for (var i = 0; i < checkSize.length; i++) {
            if (checkSize[i].checked === true) {
                $scope.ProSize= checkSize[i].value;
            }
        };
        $scope.model = {};
        $scope.model.Id = product.Id;
        $scope.model.Size = $scope.ProSize;
        $scope.model.Quantity = $scope.ProSoLuong;
        $scope.model.Color = product.Color;
        $scope.model.State = product.State;
        $scope.model.Category = product.Category;
        $scope.model.AvatarImage = product.AvatarImage;
        $scope.model.Price = product.Price;
        $http({
            method: "post",
            url: "/Cart/Buy",
            datatype: "json",
            data: JSON.stringify($scope.model)
        }).then(function (d) {
            $scope.dsGiohang = d.data;
            $scope.soLuong = $scope.dsGiohang.length;
            window.location.href = "/Cart/Index";
        }, function (error) {
            alert("failed");
        });
    };
});
// Shopping cart
app.controller("CartController", function ($scope, $http) {
    $scope.dsGH = [];
    $scope.hide = true;
    $scope.getCart = function () {
        $http.get("/Cart/getCart").then(function (d) {
            $scope.dsGH = d.data.dsGH;
            if ($scope.dsGH.length == 0) {
                $scope.hide = false;
            }
            $scope.soluong = d.data.soluong;
            $scope.thanhtien = d.data.Thanhtien;
        }, function (e) {
            alert('failed');
        })
    };

    $scope.removeCartItem = function (pro) {
        $http({
            method: "post",
            url: "/Cart/Remove",
            datatype: "json",
            data: JSON.stringify(pro)
        }).then(function (d) {
            $scope.getCart();
            toastr.success(d.data);
        }, function (error) {
            toastr.error("Xoa san pham khong thanh cong!");
        });
    };

    $scope.order = function (user) {
        $http({
            method: "post",
            url: "/Cart/Order",
            datatype: "json",
            data: JSON.stringify(user)
        }).then(function (respone) {
            window.location.href = '/Cart/History';
        }, function (e) {
                toastr.error("Mua hang ko thanh cong!");
        })
    };
    $scope.hid = true;
    $scope.getSPdaMua = function () {
        $http.get("/Cart/SpBuy").then(function (d) {
            $scope.dsCT = d.data;
            if ($scope.dsCT.length == 0) {
                $scope.hid = false;
            }
        }, function (e) {
            alert('failed');
        })
    };
});