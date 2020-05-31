import { SudokuMaterialModule } from './sudoku-material.module'
import { CommonModule } from '@angular/common'
import { NgModule } from '@angular/core'

import { SudokuRoutingModule } from './soduku.routing'
import { SudokuServiceModule } from './sudoku-service.module'

import { SudokuLayoutComponent } from './layouts/sudoku-layout/sudoku-layout.component'
import { SudokuGridComponent } from './components/sudoku-grid/sudoku-grid.component'
import { FormsModule } from '@angular/forms'


@NgModule({
    declarations: [
        SudokuLayoutComponent,

        SudokuGridComponent
    ],
    imports: [
        CommonModule,
        FormsModule,

        SudokuRoutingModule,
        SudokuServiceModule,
        SudokuMaterialModule
    ]
})
export class SudokuModule { }
