import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core'
import { AuthService } from 'src/app/services/auth.service'

@Component({
    selector: 'rh-auth-login',
    templateUrl: './login.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnInit {
    constructor(private readonly authService: AuthService) { }

    async ngOnInit() {
        await this.authService.signin()
    }
}
