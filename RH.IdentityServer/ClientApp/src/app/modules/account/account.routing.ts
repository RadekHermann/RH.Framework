import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'

import { LoginComponent } from './components/login/login.component'
import { LogoutComponent } from './components/logout/logout.component'

const ACCOUNT_ROUTES: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'logout', component: LogoutComponent }
]

@NgModule({
    imports: [RouterModule.forChild(ACCOUNT_ROUTES)],
    exports: [RouterModule]
})
export class AccountRoutingModule { }
