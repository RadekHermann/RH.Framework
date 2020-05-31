import { NgModule } from '@angular/core'
import { MatInputModule } from '@angular/material/input'
import { MatCardModule } from '@angular/material/card'
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatButtonModule } from '@angular/material/button'
import { MatCheckboxModule } from '@angular/material/checkbox'
import { MatDividerModule } from '@angular/material/divider'
import { MatIconModule } from '@angular/material/icon'

import { FlexLayoutModule } from '@angular/flex-layout'

const MAT_MODULES = [
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    MatCheckboxModule,
    MatDividerModule,
    MatIconModule,

    FlexLayoutModule
]

@NgModule({
    imports: MAT_MODULES,
    exports: MAT_MODULES
})
export class AccountMaterialModule { }
