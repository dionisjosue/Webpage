import { Products } from './products'
import * as _ from 'lodash'

export class Orders {
    id: number;
    orderDate: Date = new Date();
    orderNumber: string;
    items: Array<OrderItem> = new Array<OrderItem>();

    get subtotal(): number {
        return Math.round(_.sum(_.map(this.items, i => i.unitPrice * i.quantity))*100)/100
        
    }

}
export class OrderItem {
    id: number;
    quantity: number;
    unitPrice: number;
    productId: number;
    productCategory: string;
    productSize: string;
    productTitle: string;
    productArtist: string;
    productArtId: string;
    //total: number;
}