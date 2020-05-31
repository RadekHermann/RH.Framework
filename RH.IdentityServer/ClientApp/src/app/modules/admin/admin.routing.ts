import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'

const ADMIN_ROUTES: Routes = []

@NgModule({
    imports: [RouterModule.forChild(ADMIN_ROUTES)],
    exports: [RouterModule]
})
export class AdminRoutingModule { }
