import { Component } from '@angular/core'
import { AuthService } from 'src/app/services/auth.service'
import { ActivatedRoute } from '@angular/router'
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment'

@Component({
    selector: 'rh-layout',
    templateUrl: './layout.component.html'
})
export class LayoutComponent {
    title: 'ClientAPP'

    name: string

    constructor(private readonly authService: AuthService, private readonly http: HttpClient) {
        this.name = this.authService.name

        this.http.get(`${environment.apiUri}/WeatherForecast`, { responseType: 'json' }).subscribe(s => console.log(s))
    }

    logout() {
        this.authService.signout()
    }
}
