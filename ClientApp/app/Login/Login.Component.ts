import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from '../Shared/DataService';
import { Products } from "../Shared/products";

@Component({
    selector: "login",
    templateUrl:"Login.Component.html"
})
export class Login {

    constructor(private data: DataService, private router: Router) {
    }

    
    public errormessage: string = "";


    public creds = {
        username: "",
        password: ""
    };


    public OnLogin() {
        this.data.Login(this.creds)
            .subscribe(issucess => {
                if (issucess) {
                    if (this.data.order.items.length > 0) { this.router.navigate(["Checkout"]); }
                    else { this.router.navigate([""]); }
                }
            
            }, errors => this.errormessage = "failed to login");

    }


}