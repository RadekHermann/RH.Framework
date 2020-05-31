import { Injectable } from '@angular/core'

import { minBy, filter } from 'lodash'

import { SudokuServiceModule } from './../sudoku-service.module'

import { Cell } from '../models/cell'
import { Grid } from './../models/grid'

@Injectable({ providedIn: SudokuServiceModule })
export class ObjectSolverService {
    private grid: Grid

    init(grid: Grid) {
        this.grid = grid
    }

    solve(): boolean {
        const cell = this.getEmptyCell()
        if (cell) {
            const pos = [...cell.getPosibilities()]
            for (let i = 0; i < pos.length; i++) {
                cell.value = pos[i]
                if (this.solve()) {
                    return true
                } else {
                    cell.value = 0
                }
            }

            return false
        } else {
            return true
        }
    }

    getEmptyCell(): Cell {
        return minBy(filter(this.grid.cells, f => !f.value), b => b.getPosibilities().length)
    }
}
