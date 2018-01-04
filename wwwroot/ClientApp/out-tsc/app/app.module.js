"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var platform_browser_1 = require("@angular/platform-browser");
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http"); //to permit use component involved with http in the components
var router_1 = require("@angular/router");
var forms_1 = require("@angular/forms"); //is necesary to can use ngmodel 
var app_component_1 = require("./app.component");
var Products_Component_1 = require("./Shop/Products.Component");
var SearchProductComponent_1 = require("./SearchProducts/SearchProductComponent");
var DataService_1 = require("./Shared/DataService");
var Cart_Component_1 = require("./Cart/Cart.Component");
var checkout_component_1 = require("./checkout.component");
var Shop_Component_1 = require("./Shop.Component");
var Login_Component_1 = require("./Login/Login.Component");
var route = [
    {
        path: "", component: Shop_Component_1.Shop //root of the SAP that is the reason of the path is empty
    },
    {
        path: "Checkout", component: checkout_component_1.Checkout
    },
    {
        path: "Login", component: Login_Component_1.Login
    }
];
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        declarations: [
            app_component_1.AppComponent,
            Products_Component_1.ProductList,
            SearchProductComponent_1.SearchProduct,
            Cart_Component_1.CartComponent,
            checkout_component_1.Checkout,
            Shop_Component_1.Shop,
            Login_Component_1.Login
        ],
        imports: [
            platform_browser_1.BrowserModule,
            http_1.HttpClientModule,
            forms_1.FormsModule,
            router_1.RouterModule.forRoot(route, {
                useHash: true,
                enableTracing: false
            })
        ],
        providers: [DataService_1.DataService],
        bootstrap: [app_component_1.AppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map