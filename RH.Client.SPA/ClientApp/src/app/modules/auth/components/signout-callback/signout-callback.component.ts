import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core'
import { AuthService } from 'src/app/services/auth.service'
import { Router } from '@angular/router'

@Component({
    selector: 'rh-auth-signout-callback',
    templateUrl: './signout-callback.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SignoutCallbackComponent implements OnInit {
    constructor(private readonly service: AuthService, private readonly router: Router) { }

    async ngOnInit() {
        await this.service.signOutComplete()
        this.router.navigate([''])
    }
}
