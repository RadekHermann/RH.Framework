import { Component, OnInit } from '@angular/core'
import { AccountService } from '../../services/account.service'
import { ActivatedRoute } from '@angular/router'

@Component({
    selector: 'is-logout',
    templateUrl: './logout.component.html'
})
export class LogoutComponent implements OnInit {
    constructor(private readonly service: AccountService, private readonly activeRoute: ActivatedRoute) { }

    ngOnInit() {
        this.service.logout(this.activeRoute.snapshot.queryParams['logoutId']).subscribe(s => window.location.href = s)
    }
}
