import { HttpClient, HttpHeaders } from '@angular/common/http'; //to can use the module httpclient
import { Injectable} from '@angular/core'; //to can use injectable components in the class directly without specific at providers in the app.module
import { Observable } from 'rxjs';
import { Products } from './products';
import { Orders, OrderItem } from './Orders'
import 'rxjs/add/operator/map'; //to can use map y no pasar la data directamente a cualquiera que llame al metodo

@Injectable()
export class DataService {


    constructor(private http: HttpClient) {

    }

    private Token = "";
    private tokenvalidation: Date;
    public order: Orders = new Orders();

    public get LoginRequired(): boolean {
        return this.Token.length == 0 || this.tokenvalidation > new Date();
    }
    public Login(creds): Observable<boolean> {
        return this.http.post("/Account/CreateToken", creds)
            .map((data: any)=> {
                this.Token = data.token;
                this.tokenvalidation = data.expiration;
                return true;
            });
    }
    LoadProducts(): Observable<Products[]> {
        return this.http.get('/Api/Product')
            .map((data: Products[]) => {
                return data;
            });
    }
    
    public AddOrder(producto: Products) {

       
        let item: OrderItem = this.order.items.find(i => i.productId == producto.productId)

        if (item) {
            item.quantity++;
        }
        else {
            item = new OrderItem();
            item.productId = producto.productId;
            item.productSize = producto.size;
            item.productTitle = producto.title;
            item.productArtId = producto.artId;
            item.productCategory = producto.category;
            item.quantity = 1;
            item.unitPrice = producto.price;
            this.order.items.push(item);
        }

    }
 //headers is a params that proof that we are authenthicated or not in that way we can access to ordercontroller
    public Checkout() {
        if (!this.order.orderNumber) {
            this.order.orderNumber = this.order.orderDate.getFullYear().toString() + this.order.orderDate.getTime().toString();
        }

        return this.http.post('/api/Order', this.order, { headers: new HttpHeaders().set("Authorization", "Bearer " + this.Token) })
            .map(response => {
                this.order = new Orders();
                return true;
            })
    }
    public authenthicated: boolean = false;
    public IsAuthenthicated() {
        return this.http.get('/Account/IsAuthenthicated')
            .map((data: any) => {
                this.authenthicated = data.auth;
                return true;
            });
    }

}