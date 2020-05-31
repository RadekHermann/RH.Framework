import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'
import { DefaultLayoutComponent } from './layouts/default-layout/default-layout.component'

const routes: Routes = [
    { path: 'auth', loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule) },
    { path: 'sudoku', loadChildren: () => import('./modules/algorithms/sudoku/sudoku.module').then(m => m.SudokuModule) },
    { path: '', pathMatch: 'full', component: DefaultLayoutComponent }
]

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
