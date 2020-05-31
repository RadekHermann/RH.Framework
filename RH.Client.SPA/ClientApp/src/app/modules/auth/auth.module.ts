import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { RouterModule } from '@angular/router'

import { AUTH_ROUTES } from './auth.routing'
import { AuthServiceModule } from './auth-service.module'
import { LoginComponent } from './components/login/login.component'
import { SigninCallbackComponent } from './components/signin-callback/signin-callback.component'
import { SignoutCallbackComponent } from './components/signout-callback/signout-callback.component'


@NgModule({
    declarations: [
        LoginComponent,
        SigninCallbackComponent,
        SignoutCallbackComponent
    ],
    imports: [
        CommonModule,
        RouterModule.forChild(AUTH_ROUTES),

        AuthServiceModule
    ],
})
export class AuthModule {
}
