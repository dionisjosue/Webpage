"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var http_1 = require("@angular/common/http"); //to can use the module httpclient
var core_1 = require("@angular/core"); //to can use injectable components in the class directly without specific at providers in the app.module
var Orders_1 = require("./Orders");
require("rxjs/add/operator/map"); //to can use map y no pasar la data directamente a cualquiera que llame al metodo
var DataService = (function () {
    function DataService(http) {
        this.http = http;
        this.Token = "";
        this.order = new Orders_1.Orders();
        this.authenthicated = false;
    }
    Object.defineProperty(DataService.prototype, "LoginRequired", {
        get: function () {
            return this.Token.length == 0 || this.tokenvalidation > new Date();
        },
        enumerable: true,
        configurable: true
    });
    DataService.prototype.Login = function (creds) {
        var _this = this;
        return this.http.post("/Account/CreateToken", creds)
            .map(function (data) {
            _this.Token = data.token;
            _this.tokenvalidation = data.expiration;
            return true;
        });
    };
    DataService.prototype.LoadProducts = function () {
        return this.http.get('/Api/Product')
            .map(function (data) {
            return data;
        });
    };
    DataService.prototype.AddOrder = function (producto) {
        var item = this.order.items.find(function (i) { return i.productId == producto.productId; });
        if (item) {
            item.quantity++;
        }
        else {
            item = new Orders_1.OrderItem();
            item.productId = producto.productId;
            item.productSize = producto.size;
            item.productTitle = producto.title;
            item.productArtId = producto.artId;
            item.productCategory = producto.category;
            item.quantity = 1;
            item.unitPrice = producto.price;
            this.order.items.push(item);
        }
    };
    //headers is a params that proof that we are authenthicated or not in that way we can access to ordercontroller
    DataService.prototype.Checkout = function () {
        var _this = this;
        if (!this.order.orderNumber) {
            this.order.orderNumber = this.order.orderDate.getFullYear().toString() + this.order.orderDate.getTime().toString();
        }
        return this.http.post('/api/Order', this.order, { headers: new http_1.HttpHeaders().set("Authorization", "Bearer " + this.Token) })
            .map(function (response) {
            _this.order = new Orders_1.Orders();
            return true;
        });
    };
    DataService.prototype.IsAuthenthicated = function () {
        var _this = this;
        return this.http.get('/Account/IsAuthenthicated')
            .map(function (data) {
            _this.authenthicated = data.auth;
            return true;
        });
    };
    return DataService;
}());
DataService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.HttpClient])
], DataService);
exports.DataService = DataService;
//# sourceMappingURL=DataService.js.map