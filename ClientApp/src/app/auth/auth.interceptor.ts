import {
  Injectable
} from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import {
  Observable,
  throwError
} from 'rxjs';
import {
  AuthService
} from './auth.service';
import {
  Router
} from '@angular/router';
import {
  catchError
} from 'rxjs/operators';
import { TagContentType } from '@angular/compiler';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  intercept(request: HttpRequest < unknown > , next: HttpHandler): Observable < HttpEvent < unknown >> {

    if (this.authService.isAuth()) {
      request = request.clone({
        setHeaders: {
          //Authorization: `Bearer ${this.authService.token}`
          Authorization: `${this.authService.token}`
        }
      })
    }

    return next.handle(request)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401 || 403 || 400 || 500 || 503 || 404) {
            this.router.navigate(['error']);
          }
          return throwError(error);
        })
      )
  }
}
