import { Component } from "@angular/core";
import { DataService } from './Shared/DataService';
import { Router } from "@angular/router";

@Component({
  selector: "checkout",
  templateUrl: "checkout.component.html",
  styleUrls: ['checkout.component.css']
})
export class Checkout {

   
    constructor(public data: DataService, public router: Router) {
    }
    public errormessage: string = "";
    public OnCheckout() {
        this.data.Checkout()
            .subscribe(success => {
                this.router.navigate([""]);

            }, error => this.errormessage = "Failed to Save the order");
    }
   
  

}