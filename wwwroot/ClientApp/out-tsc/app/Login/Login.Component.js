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
var router_1 = require("@angular/router");
var DataService_1 = require("../Shared/DataService");
var Login = (function () {
    function Login(data, router) {
        this.data = data;
        this.router = router;
        this.errormessage = "";
        this.creds = {
            username: "",
            password: ""
        };
    }
    Login.prototype.OnLogin = function () {
        var _this = this;
        this.data.Login(this.creds)
            .subscribe(function (issucess) {
            if (issucess) {
                if (_this.data.order.items.length > 0) {
                    _this.router.navigate(["Checkout"]);
                }
                else {
                    _this.router.navigate([""]);
                }
            }
        }, function (errors) { return _this.errormessage = "failed to login"; });
    };
    return Login;
}());
Login = __decorate([
    core_1.Component({
        selector: "login",
        templateUrl: "Login.Component.html"
    }),
    __metadata("design:paramtypes", [DataService_1.DataService, router_1.Router])
], Login);
exports.Login = Login;
//# sourceMappingURL=Login.Component.js.map