export class User{
    public id: number |null = null
    public name: string |null = null
    public email: string |null = null
    public password: string |null = null

    constructor(
        id: number|null = null,
        name : string|null = null,
        email: string|null = null,
        password: string|null = null) 
    { 
        this.id = id;
        this.name = name;
        this.email = email;
        this.password = password;
    }
}