import { Component, Input, EventEmitter, Output} from '@angular/core';
import { DataService } from '../Shared/DataService';
import { Products } from "../Shared/products";

@Component({
    selector: 'Search',
    templateUrl: './SearchProductComponent.html',
    styleUrls: []

})
export class SearchProduct{
   

    constructor(private data: DataService) {
    }

    @Input() producto: Products[];
    @Output() ProductSearch: EventEmitter<Products[]> = new EventEmitter<Products[]>();
    @Input() filterproduct: Products[];
    public filterBy: string = "";

    get Filter(): string {
        return this.filterBy.toLocaleLowerCase();
    }
    set Filter(value: string){
        this.filterBy = value;
        this.producto = this.Filter ? this.OnSearchClick(this.Filter) : this.filterproduct;

        console.log("allo", this.producto.length.toString());
        console.log("all", this.filterproduct.length.toString());
        this.ProductSearch.emit(this.producto);
        this.producto = this.filterproduct;

    }
    public OnSearchClick(value: string):Products [] {

        return this.producto.filter((items: Products) =>
            items.title.toLocaleLowerCase().indexOf(value) !== -1);
        
    }

}