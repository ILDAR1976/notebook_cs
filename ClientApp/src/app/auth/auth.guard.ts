//import { Route } from '@angular/compiler/src/core';
import {
  Injectable
} from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanLoad,
  Router,
  RouterStateSnapshot,
  UrlSegment,
} from '@angular/router';
import {
  Observable
} from 'rxjs';
import {
  AuthService
} from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanLoad {

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  canLoad(route: any,
      segments: UrlSegment[]):
    Observable < boolean > | Promise < boolean > | boolean { 

      // заглушка, чтобы админ просто так не подгружался
      if (route.path === "admin") {
        this.router.navigate(['login']);
        return false;
      }
      
      if (this.authService.isAuth()) {
        return true;
      } else {
        this.authService.logout();
        this.router.navigate(['login']);
        return false;
      }
    }

  canActivate(
      route: ActivatedRouteSnapshot,
      state: RouterStateSnapshot):
    Observable < boolean > | Promise < boolean > | boolean {
      if (this.authService.isAuth()) {
        return true;
      } else {
        this.authService.logout();
        this.router.navigate(['login']);
        return false;
      }
    }

}
