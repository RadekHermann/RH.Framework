import { Component, OnInit } from '@angular/core'

import { AuthService } from 'src/app/services/auth.service'
import { Router } from '@angular/router'

@Component({
    selector: 'rh-signin-callback',
    templateUrl: './signin-callback.component.html'
})
export class SigninCallbackComponent implements OnInit {
    constructor(
        private readonly router: Router,
        private readonly authService: AuthService
    ) { }

    async ngOnInit() {
        await this.authService.signInComplete()
        this.router.navigate([''])
    }
}
