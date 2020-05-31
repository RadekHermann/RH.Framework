import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'

import { SudokuLayoutComponent } from './layouts/sudoku-layout/sudoku-layout.component'

const SUDOKU_ROUTES: Routes = [
    { path: '', component: SudokuLayoutComponent, children: [] }
]

@NgModule({
    imports: [RouterModule.forChild(SUDOKU_ROUTES)],
    exports: [RouterModule]
})
export class SudokuRoutingModule { }
