"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var _ = require("lodash");
var Orders = (function () {
    function Orders() {
        this.orderDate = new Date();
        this.items = new Array();
    }
    Object.defineProperty(Orders.prototype, "subtotal", {
        get: function () {
            return Math.round(_.sum(_.map(this.items, function (i) { return i.unitPrice * i.quantity; })) * 100) / 100;
        },
        enumerable: true,
        configurable: true
    });
    return Orders;
}());
exports.Orders = Orders;
var OrderItem = (function () {
    function OrderItem() {
    }
    return OrderItem;
}());
exports.OrderItem = OrderItem;
//# sourceMappingURL=Orders.js.map