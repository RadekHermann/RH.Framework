import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'


const routes: Routes = [
    {
        path: 'account', loadChildren: () => import('./modules/account/account.module').then(m => m.AccountModule)
    },
    {
        path: 'admin', loadChildren: () => import('./modules/admin/admin.module').then(m => m.AdminModule)
    }
]

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
