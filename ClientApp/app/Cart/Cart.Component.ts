import { Component } from '@angular/core';
import { DataService } from '../Shared/DataService';
import { Router } from '@angular/router'
@Component({

    selector: 'the-cart',
    templateUrl: './App.Cart.html',
    styles: ["CartStyles.css"]

})
export class CartComponent {
    constructor(private data: DataService, private router: Router) {
        this.authe = this.data.authenthicated
    }
    private authe : boolean;

    OnCheckout() {
        if (this.data.LoginRequired && !this.authe) {
            this.router.navigate(["Login"]);//para navegar entre los diversos component especificando el path definido en el app module
        }
        else {
            this.router.navigate(["Checkout"]);
        }
    }
    

}
