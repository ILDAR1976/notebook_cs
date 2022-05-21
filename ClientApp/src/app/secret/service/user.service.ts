import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {User} from '../model/user';
import {environment} from 'src/environments/environment';

@Injectable()
export class UserService{

    private url = "https://localhost:7048/base/rest/admin/users";
    constructor(private http: HttpClient){ }

    getUsers(){
        return this.http.get<Array<User>>(this.url);
    }

    createUser(user: User){
        const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
        return this.http.post<User>(this.url, JSON.stringify(user), {headers: myHeaders});
    }

    updateUser(user: User) {
        const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
        return this.http.put<User>(this.url + "/" + user.id, JSON.stringify(user), {headers:myHeaders});
    }

    deleteUser(id: number|null = null){

        return this.http.delete<User>(this.url + '/' + id);
    }


    logout() {
      console.log('The pass this phase');
      return this.http.get('${environment.host}/logout');
    }
}
