import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'

import { AdminServiceModule } from './admin-service.module'
import { AdminRoutingModule } from './admin.routing'

@NgModule({
    imports: [
        CommonModule,

        AdminServiceModule,
        AdminRoutingModule
    ]
})
export class AdminModule { }
