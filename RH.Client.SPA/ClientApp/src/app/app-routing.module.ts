import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'
import { LayoutComponent } from './components/layout/layout.component'
import { AuthGuard } from './guards/auth.guard'

const routes: Routes = [
    { path: 'auth', loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule) },
    { path: '', pathMatch: 'full', component: LayoutComponent, canActivate: [AuthGuard] }
]

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
