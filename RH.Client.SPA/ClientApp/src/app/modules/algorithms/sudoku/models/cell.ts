import { remove, uniqueId } from 'lodash'
import { Grid } from './grid'

export class Cell {
    public id: string

    constructor(private readonly grid: Grid, public row: number, public col: number) {
        this.id = uniqueId()
    }

    private _value: number
    get value(): number {
        return this._value
    }
    set value(val: number) {
        this._value = !!+val ? +val : null
    }

    getPosibilities(): number[] {
        const pos = [...this.grid.posibilities]

        const takeRow = this.grid.getRow(this.row).filter(f => f.id !== this.id && f.value).map(m => m.value)
        const takeCol = this.grid.getCol(this.col).filter(f => f.id !== this.id && f.value).map(m => m.value)
        const takeSqr = this.grid.getSquare(this.row, this.col).filter(f => f.id !== this.id && f.value).map(m => m.value)

        takeRow.forEach(i => remove(pos, m => m === i))
        takeCol.forEach(i => remove(pos, m => m === i))
        takeSqr.forEach(i => remove(pos, m => m === i))

        if (this.grid.diagonal) {
            const diagonals = this.belongToDiagonal()
            for (let i = 0; i < diagonals.length; i++) {
                this.grid.getDiagonal()[diagonals[i]].forEach(d => remove(pos, m => m === d.value))
            }
        }

        return pos
    }

    belongToDiagonal(): number[] {
        const result = []
        if (this.row === this.col) {
            result.push(1)
        }

        if (this.grid.posibilities.length + 1 - this.col === this.row) {
            result.push(2)
        }

        return result
    }
}
