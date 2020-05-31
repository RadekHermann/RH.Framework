import { Injectable } from '@angular/core'
import { Router } from '@angular/router'
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http'

import { Observable } from 'rxjs'
import { tap } from 'rxjs/operators'

import { AuthService } from '../services/auth.service'

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(
        private readonly router: Router,
        private readonly authService: AuthService
    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        req = req.clone({
            setHeaders: {
                'Authorization': this.authService.authorizationHeaderValue
            }
        })

        return next.handle(req).pipe(
            tap(
                () => { },
                (err: any) => {
                    if (err instanceof HttpErrorResponse) {
                        if (err.status === 401) {
                            this.router.navigate(['auth', 'unauthorized'])
                        }

                        if (err.status === 403) {
                            this.router.navigate(['auth', 'unauthorized'])
                        }
                    }
                }
            )
        )
    }
}
