import { NgModule } from '@angular/core'

import { FlexLayoutModule } from '@angular/flex-layout'

import { MatFormFieldModule } from '@angular/material/form-field'
import { MatInputModule } from '@angular/material/input'
import { MatGridListModule } from '@angular/material/grid-list'

const MODULES = [
    FlexLayoutModule,

    MatGridListModule,
    MatFormFieldModule,
    MatInputModule
]

@NgModule({
    imports: MODULES,
    exports: MODULES
})
export class SudokuMaterialModule { }
