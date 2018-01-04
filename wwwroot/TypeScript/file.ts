class CustomerUser{

    constructor(private Firstname:string, private LastName:string)
    {
        this.name = Firstname;
        this.apellido = LastName;
    } 

    public showname(): boolean {

        alert(this.Ourname + ' ' + this.apellido);
        return true;

    }
    public visits = 0;
    private Ourname: string;
    private apellido: string;

    set name(val){
        this.Ourname = val;
    }
    get name(){
        return this.Ourname;
    }


    


}