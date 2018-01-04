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
var core_1 = require("@angular/core");
var DataService_1 = require("../Shared/DataService");
var router_1 = require("@angular/router");
var CartComponent = (function () {
    function CartComponent(data, router) {
        this.data = data;
        this.router = router;
        this.authe = this.data.authenthicated;
    }
    CartComponent.prototype.OnCheckout = function () {
        if (this.data.LoginRequired && !this.authe) {
            this.router.navigate(["Login"]); //para navegar entre los diversos component especificando el path definido en el app module
        }
        else {
            this.router.navigate(["Checkout"]);
        }
    };
    return CartComponent;
}());
CartComponent = __decorate([
    core_1.Component({
        selector: 'the-cart',
        templateUrl: './App.Cart.html',
        styles: ["CartStyles.css"]
    }),
    __metadata("design:paramtypes", [DataService_1.DataService, router_1.Router])
], CartComponent);
exports.CartComponent = CartComponent;
//# sourceMappingURL=Cart.Component.js.map