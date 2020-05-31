import { Component, OnInit } from '@angular/core'
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { ActivatedRouteSnapshot, ActivatedRoute } from '@angular/router'
import { AccountService } from '../../services/account.service'

@Component({
    selector: 'is-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    hide = true

    form: FormGroup

    constructor(
        private readonly fb: FormBuilder,
        private readonly service: AccountService,
        private readonly activeRoute: ActivatedRoute) { }

    ngOnInit() {
        this.form = this.fb.group({
            userName: ['', Validators.required],
            password: ['', Validators.required],
            rememberMe: [false]
        })
    }

    login() {
        if (this.form.valid) {
            this.service.login({
                userName: this.form.value.userName,
                password: this.form.value.password,
                rememberMe: this.form.value.rememberMe,
                returnUrl: this.activeRoute.snapshot.queryParamMap.get('ReturnUrl')
            }).subscribe(s => window.location.href = s, (error) => console.log(error))
        }
    }
}
