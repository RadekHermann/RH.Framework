import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router'

import { AuthService } from '../services/auth.service'
import { Injectable } from '@angular/core'

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    constructor(private router: Router, private authService: AuthService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (this.authService.isAuthenticated()) {
            return true
        }

        this.router.navigate(['auth', 'login'], { queryParams: { redirect: state.url }, replaceUrl: true })
        return false
    }
}
