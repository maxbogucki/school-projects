#ifndef _PUZZLESTATE_
#define _PUZZLESTATE_
#include <iostream>
#include <vector>

using namespace std;

class PuzzleState
{
public:
    // default constructor
    PuzzleState() : rows(0), cols(0), blank_position_row(-1), blank_position_col(-1) {}

    // constructor
    PuzzleState(int n, int m) {
        rows = n;
        cols = m;
        tiles.resize(n * m);
        blank_position_row = -1;
        blank_position_col = -1;
    }

    // destructor
    ~PuzzleState() { }

    // copy constructor
    PuzzleState(const PuzzleState& rhs)
        : rows(rhs.rows), cols(rhs.cols),
        tiles(rhs.tiles),
        blank_position_row(rhs.blank_position_row),
        blank_position_col(rhs.blank_position_col)
    { }

    bool operator==(const PuzzleState& rhs) const {
        return tiles == rhs.tiles;
    }

    bool operator!=(const PuzzleState& rhs) const {
        return !(operator==(rhs));
    }

    const PuzzleState& operator=(const PuzzleState& rhs) { // assignment operator
        if (this != &rhs) {
            rows = rhs.rows;
            cols = rhs.cols;
            tiles = rhs.tiles;
            blank_position_row = rhs.blank_position_row;
            blank_position_col = rhs.blank_position_col;
        }
        return *this;
    }

    bool isNullState() const { return rows == 0; }

    bool canMoveUp() const { return blank_position_row > 0; }
    bool canMoveDown() const { return blank_position_row < rows - 1; }
    bool canMoveLeft() const { return blank_position_col > 0; }
    bool canMoveRight() const { return blank_position_col < cols - 1; }

    PuzzleState moveBlankUp() const {
        if (!canMoveUp()) return *this;
        PuzzleState newState(*this);
        int currentBlankIndex = blank_position_row * cols + blank_position_col;
        int newBlankIndex = (blank_position_row - 1) * cols + blank_position_col;
        swap(newState.tiles[currentBlankIndex], newState.tiles[newBlankIndex]);
        newState.blank_position_row--;
        return newState;
    }

    PuzzleState moveBlankDown() const {
        if (!canMoveDown()) return *this;
        PuzzleState newState(*this);
        int currentBlankIndex = blank_position_row * cols + blank_position_col;
        int newBlankIndex = (blank_position_row + 1) * cols + blank_position_col;
        swap(newState.tiles[currentBlankIndex], newState.tiles[newBlankIndex]);
        newState.blank_position_row++;
        return newState;
    }

    PuzzleState moveBlankLeft() const {
        if (!canMoveLeft()) return *this;
        PuzzleState newState(*this);
        int currentBlankIndex = blank_position_row * cols + blank_position_col;
        int newBlankIndex = blank_position_row * cols + blank_position_col - 1;
        swap(newState.tiles[currentBlankIndex], newState.tiles[newBlankIndex]);
        newState.blank_position_col--;
        return newState;
    }

    PuzzleState moveBlankRight() const {
        if (!canMoveRight()) return *this;
        PuzzleState newState(*this);
        int currentBlankIndex = blank_position_row * cols + blank_position_col;
        int newBlankIndex = blank_position_row * cols + blank_position_col + 1;
        swap(newState.tiles[currentBlankIndex], newState.tiles[newBlankIndex]);
        newState.blank_position_col++;
        return newState;
    }

    void read(istream& in) {
        tiles.resize(rows * cols);
        for (int i = 0; i < tiles.size(); i++) {
            in >> tiles[i];
            if (tiles[i] == 0) {
                blank_position_row = i / cols;
                blank_position_col = i % cols;
            }
        }
    }

    void print(ostream& out) const {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                int index = i * cols + j;
                out << tiles[index] << " ";
            }
            out << endl;
        }
    }

    const vector<int>& getTiles() const {
        return tiles;
    }

    int getRows() const {
        return rows;
    }

    int getCols() const {
        return cols;
    }

    static PuzzleState NullState;

private:
    int rows;
    int cols;
    vector<int> tiles;
    int blank_position_row;
    int blank_position_col;
};

istream& operator>>(istream& in, PuzzleState& rhs) {
    rhs.read(in);
    return in;
}

ostream& operator<<(ostream& out, PuzzleState& rhs) {
    rhs.print(out);
    return out;
}

#endif