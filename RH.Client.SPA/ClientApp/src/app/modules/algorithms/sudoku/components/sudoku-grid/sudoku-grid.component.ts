import { Component } from '@angular/core'
import { Cell } from '../../models/cell'
import { ObjectSolverService } from '../../services/object-solver.service'
import { Grid } from '../../models/grid'

@Component({
    selector: 'rh-sudoku-grid',
    templateUrl: './sudoku-grid.component.html',
    styleUrls: ['./sudoku-grid.component.scss']
})
export class SudokuGridComponent {
    cells: Cell[] = []
    grid: Grid

    constructor(private readonly objectSolver: ObjectSolverService) {

        this.grid = new Grid()

        for (let x = 1; x <= 9; x++) {
            for (let y = 1; y <= 9; y++) {
                this.cells.push(new Cell(this.grid, x, y))
            }
        }
    }

    solve() {
        this.grid.init(this.cells, false)

        this.objectSolver.init(this.grid)
        this.objectSolver.solve()

    }
}
