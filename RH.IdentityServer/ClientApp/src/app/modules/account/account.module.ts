import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { HttpClientModule } from '@angular/common/http'
import { ReactiveFormsModule } from '@angular/forms'

import { LoginComponent } from './components/login/login.component'
import { LogoutComponent } from './components/logout/logout.component'
import { AccountMaterialModule } from './account-material.module'
import { AccountRoutingModule } from './account.routing'
import { AccountServiceModule } from './account-service.module'

@NgModule({
    declarations: [
        LoginComponent,
        LogoutComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        ReactiveFormsModule,

        AccountRoutingModule,
        AccountServiceModule,
        AccountMaterialModule
    ]
})
export class AccountModule { }
