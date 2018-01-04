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
var SearchProduct = (function () {
    function SearchProduct(data) {
        this.data = data;
        this.ProductSearch = new core_1.EventEmitter();
        this.filterBy = "";
    }
    Object.defineProperty(SearchProduct.prototype, "Filter", {
        get: function () {
            return this.filterBy.toLocaleLowerCase();
        },
        set: function (value) {
            this.filterBy = value;
            this.producto = this.Filter ? this.OnSearchClick(this.Filter) : this.filterproduct;
            console.log("allo", this.producto.length.toString());
            console.log("all", this.filterproduct.length.toString());
            this.ProductSearch.emit(this.producto);
            this.producto = this.filterproduct;
        },
        enumerable: true,
        configurable: true
    });
    SearchProduct.prototype.OnSearchClick = function (value) {
        return this.producto.filter(function (items) {
            return items.title.toLocaleLowerCase().indexOf(value) !== -1;
        });
    };
    return SearchProduct;
}());
__decorate([
    core_1.Input(),
    __metadata("design:type", Array)
], SearchProduct.prototype, "producto", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", core_1.EventEmitter)
], SearchProduct.prototype, "ProductSearch", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", Array)
], SearchProduct.prototype, "filterproduct", void 0);
SearchProduct = __decorate([
    core_1.Component({
        selector: 'Search',
        templateUrl: './SearchProductComponent.html',
        styleUrls: []
    }),
    __metadata("design:paramtypes", [DataService_1.DataService])
], SearchProduct);
exports.SearchProduct = SearchProduct;
//# sourceMappingURL=SearchProductComponent.js.map