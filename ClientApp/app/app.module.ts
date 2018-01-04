import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';//to permit use component involved with http in the components
import { RouterModule } from '@angular/router';
import { FormsModule } from "@angular/forms";//is necesary to can use ngmodel 
import { AppComponent } from './app.component';
import { ProductList} from './Shop/Products.Component';
import { SearchProduct } from './SearchProducts/SearchProductComponent';
import { DataService } from './Shared/DataService';
import { CartComponent } from './Cart/Cart.Component';
import { Checkout } from './checkout.component';
import { Shop } from './Shop.Component';
import { Login } from './Login/Login.Component';

let route = [//declarations of routes or path that you are going to use to navigate throw one component to other
    {
        path: "", component: Shop //root of the SAP that is the reason of the path is empty
    },
    {
        path: "Checkout", component: Checkout
    },
    {
        path: "Login", component: Login
    }
]

@NgModule({
    declarations: [
        AppComponent,
        ProductList,
        SearchProduct,
        CartComponent,
        Checkout,
        Shop,
        Login


    ],

    imports: [
        BrowserModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot(route, {//es necesario para poder navegar entre un componente y otro

            useHash: true,
            enableTracing: false

        })

    ],

    providers: [DataService],//here comes all class of angular that are injected (constructor) in other components o classes
    bootstrap: [AppComponent]

})
export class AppModule { }
