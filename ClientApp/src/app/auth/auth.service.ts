import {
  HttpClient,  HttpHeaders,
} from '@angular/common/http';
import {
  Injectable
} from '@angular/core';
import {
  Observable
} from 'rxjs';
import {
  environment
} from 'src/environments/environment';
import {
  tap
} from 'rxjs/operators';
import {
  User
} from '../secret/model/user';
import {
  Token
} from '../secret/model/token';


@Injectable({
  providedIn: 'root'
})
export class AuthService  {
  private url = "https://localhost:7048/base/rest/admin/users";
  private sourceUrl = "https://localhost:7048/base";

  constructor(
    private httpClient: HttpClient
  ) {}

  isAuth(): boolean {

    if (this.token != null) {
      return (this.token.length > 0) ? true: false;
    } else {
      return false;
    }


  }

  private setToken(response: any | null) {
    if (response) {
      const res = JSON.parse(response);

      localStorage.setItem('accessToken', res.access_token);

    } else {
      localStorage.clear();
    }
  }

  get token() {
    return localStorage.getItem('accessToken');
  }

  login(user: {
    username: string,
    password: string
  }): Observable < any >   {
    let new_user: User = new User(0,"",user.username,user.password);
    let header = new HttpHeaders().set("Content-Type", "application/json");
    return this.httpClient.post(this.sourceUrl + "/api/auth/login",
        JSON.stringify(new_user), {
          headers: header,
          responseType: 'text'
        }).
        pipe(
          tap(this.setToken)
        );
  }


  createUser(user: User){
    const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
    return this.httpClient.post<User>(this.sourceUrl + "/user/register",
        JSON.stringify(user), {headers: myHeaders});
  }


  whoami() : Observable<any> {
    return this.httpClient.post( this.sourceUrl + "/user/whoami", "", {
      responseType: 'text'
    });
  }

  logout() : Observable < any >  {
    const myHeaders = new HttpHeaders().set("Content-Type", "application/json");
    this.setToken(null);
    return this.httpClient.post( this.sourceUrl + "/api/auth/logout", "", {
      headers: myHeaders,
      responseType: 'text'
    });
  }
}
