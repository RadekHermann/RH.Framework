import { Injectable } from '@angular/core'

import { SudokuServiceModule } from './../sudoku-service.module'

@Injectable({ providedIn: SudokuServiceModule })
export class GeneratorService { }
