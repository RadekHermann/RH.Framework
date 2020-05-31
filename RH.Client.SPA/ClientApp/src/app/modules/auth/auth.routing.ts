import { Route } from '@angular/router'

import { LoginComponent } from './components/login/login.component'
import { SigninCallbackComponent } from './components/signin-callback/signin-callback.component'
import { SignoutCallbackComponent } from './components/signout-callback/signout-callback.component'
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component'

export const AUTH_ROUTES: Route[] = [
    { path: 'login', component: LoginComponent },
    { path: 'signin-callback', component: SigninCallbackComponent },
    { path: 'signout-callback', component: SignoutCallbackComponent },
    { path: 'unauthorized', component: UnauthorizedComponent }
]
