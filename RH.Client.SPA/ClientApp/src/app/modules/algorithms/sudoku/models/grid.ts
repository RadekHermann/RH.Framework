import { Cell } from './cell'
import { range, maxBy, filter } from 'lodash'

export class Grid {
    posibilities: number[]
    size: number

    public cells: Cell[]
    public diagonal: boolean

    init(cells: Cell[], diagonalRule: boolean) {
        this.cells = cells
        this.diagonal = diagonalRule

        this.posibilities = range(1, maxBy(this.cells, f => f.row).row + 1)
        this.size = Math.sqrt(this.posibilities.length)
    }

    getRow(rowIndex: number): Cell[] {
        return filter(this.cells, f => f.row === rowIndex)
    }

    getCol(colIndex: number): Cell[] {
        return filter(this.cells, f => f.col === colIndex)
    }

    getSquare(rowIndex: number, colIndex: number): Cell[] {
        const rowTo = Math.ceil(rowIndex / this.size) * this.size
        const rowFrom = rowTo - this.size + 1

        const colTo = Math.ceil(colIndex / this.size) * this.size
        const colFrom = colTo - this.size + 1

        return filter(this.cells, f => rowFrom <= f.row && f.row <= rowTo && colFrom <= f.col && f.col <= colTo)
    }

    getDiagonal(): { [key: number]: Cell[] } {
        const result: { [key: number]: Cell[] } = {}

        result[1] = filter(this.cells, f => f.row === f.col)
        result[2] = filter(this.cells, f => this.posibilities.length + 1 - f.col === f.row)

        return result
    }
}
