import { Component, OnInit } from '@angular/core';
import { DataService } from '../Shared/DataService'; //to can use the class dataservice
import { Products } from '../Shared/products';

@Component({
    selector: 'Products-List',
    templateUrl: './ProductList.html',
    styleUrls: []

})
export class ProductList implements OnInit {//onInit interfaz that is reccomend to use for call method that are involved with calls to the api, you can called in the constructor

    private products: Products[];
    private FiltereProducts: Products[];
    constructor(private data: DataService) {//private to make the var own of the class
    }
    private errmsg: string = " ";
    
    ngOnInit(): void {
        this.data.LoadProducts()
            .subscribe(data => {
                this.products = data
                this.FiltereProducts = this.products
            },
        error=> this.errmsg ="Failed To get Products" );
    }
    
    public Addproduct(product: Products):void {
        this.data.AddOrder(product);
    }
    public OnFilterProducts(data: Products[]):void {
        this.FiltereProducts = data;
    }

}
