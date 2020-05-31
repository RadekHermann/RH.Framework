import { Component } from '@angular/core'
import { AuthService } from 'src/app/services/auth.service'

@Component({
    selector: 'rh-default-layout',
    templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent {
    title: 'ClientAPP'

    name: string

    constructor(private readonly authService: AuthService) {
        this.name = this.authService.name
    }

    logout() {
        this.authService.signout()
    }
}
