#include <iostream>
#include <queue>
#include <vector>
#include <algorithm>
#include <cmath>
#include "PuzzleState.h"
#include "PuzzleMove.h"

using namespace std;

// Compute the Manhattan distance between the current state and the goal state.
int computeManhattanDistance(const PuzzleState& current, const PuzzleState& goal) {
    int distance = 0;
    int N = current.getRows();  // Assumes a square board (N x N)
    const vector<int>& currentTiles = current.getTiles();
    const vector<int>& goalTiles = goal.getTiles();

    // Precompute goal positions for each nonzero tile.
    vector<pair<int, int>> goalPositions(N * N + 1, make_pair(0, 0));
    for (int i = 0; i < goalTiles.size(); i++) {
        int tile = goalTiles[i];
        if (tile != 0) {
            goalPositions[tile] = make_pair(i / N, i % N);
        }
    }

    // Sum the Manhattan distances for each nonzero tile.
    for (int i = 0; i < currentTiles.size(); i++) {
        int tile = currentTiles[i];
        if (tile != 0) {
            int current_row = i / N;
            int current_col = i % N;
            int goal_row = goalPositions[tile].first;
            int goal_col = goalPositions[tile].second;
            distance += abs(current_row - goal_row) + abs(current_col - goal_col);
        }
    }
    return distance;
}

// Node structure for A* search.
struct AStarNode {
    PuzzleState state;
    AStarNode* parent;
    MoveType move; 
    int g; // Cost from the start state.
    int h; // Heuristic cost (Manhattan distance).
    int f; // Total cost f = g + h.

    AStarNode(PuzzleState s, AStarNode* p, MoveType m, int gVal, int hVal)
        : state(s), parent(p), move(m), g(gVal), h(hVal), f(gVal + hVal) { }
};

struct CompareAStar {
    bool operator()(AStarNode* a, AStarNode* b) {
        return a->f > b->f;
    }
};

void astar(PuzzleState start, PuzzleState goal) {
    if (start == goal) {
        cout << "*** Solution found immediately: start state is the goal state. ***" << endl;
        return;
    }

    priority_queue<AStarNode*, vector<AStarNode*>, CompareAStar> open;
    vector<PuzzleState> closed;
    int nodesExpanded = 0;

    int hStart = computeManhattanDistance(start, goal);
    AStarNode* startNode = new AStarNode(start, nullptr, nullMove, 0, hStart);
    open.push(startNode);

    while (!open.empty()) {
        AStarNode* current = open.top();
        open.pop();

        // Goal check.
        if (current->state == goal) {
            cout << "Solution:" << endl;
            // Reconstruct the solution path.
            vector<PuzzleState> pathStates;
            vector<MoveType> pathMoves;
            AStarNode* node = current;
            while (node != nullptr) {
                pathStates.push_back(node->state);
                if (node->parent != nullptr)
                    pathMoves.push_back(node->move);
                node = node->parent;
            }
            reverse(pathStates.begin(), pathStates.end());
            reverse(pathMoves.begin(), pathMoves.end());

            // Print the solution path.
            cout << pathStates[0] << endl;
            for (size_t i = 0; i < pathMoves.size(); ++i) {
                switch (pathMoves[i]) {
                case MoveType::down:  cout << "down" << endl; break;
                case MoveType::up:    cout << "up" << endl; break;
                case MoveType::left:  cout << "left" << endl; break;
                case MoveType::right: cout << "right" << endl; break;
                default: break;
                }
                cout << pathStates[i + 1] << endl;
            }
            cout << "Number of nodes expanded: " << nodesExpanded << endl;
            cout << "Path length to Goal: " << pathMoves.size() << endl;

            // Cleanup remaining nodes.
            delete current;
            while (!open.empty()) {
                AStarNode* temp = open.top();
                open.pop();
                delete temp;
            }
            return;
        }

        nodesExpanded++;
        closed.push_back(current->state);

        // Expand children in the order: down, left, up, right.
        vector<AStarNode*> children;
        // Down
        if (current->state.canMoveDown()) {
            PuzzleState childState = current->state.moveBlankDown();
            bool alreadyExplored = false;
            for (const auto& s : closed) {
                if (s == childState) {
                    alreadyExplored = true;
                    break;
                }
            }
            if (!alreadyExplored) {
                int childG = current->g + 1;
                int childH = computeManhattanDistance(childState, goal);
                children.push_back(new AStarNode(childState, current, MoveType::down, childG, childH));
            }
        }
        // Left
        if (current->state.canMoveLeft()) {
            PuzzleState childState = current->state.moveBlankLeft();
            bool alreadyExplored = false;
            for (const auto& s : closed) {
                if (s == childState) {
                    alreadyExplored = true;
                    break;
                }
            }
            if (!alreadyExplored) {
                int childG = current->g + 1;
                int childH = computeManhattanDistance(childState, goal);
                children.push_back(new AStarNode(childState, current, MoveType::left, childG, childH));
            }
        }
        // Up
        if (current->state.canMoveUp()) {
            PuzzleState childState = current->state.moveBlankUp();
            bool alreadyExplored = false;
            for (const auto& s : closed) {
                if (s == childState) {
                    alreadyExplored = true;
                    break;
                }
            }
            if (!alreadyExplored) {
                int childG = current->g + 1;
                int childH = computeManhattanDistance(childState, goal);
                children.push_back(new AStarNode(childState, current, MoveType::up, childG, childH));
            }
        }
        // Right
        if (current->state.canMoveRight()) {
            PuzzleState childState = current->state.moveBlankRight();
            bool alreadyExplored = false;
            for (const auto& s : closed) {
                if (s == childState) {
                    alreadyExplored = true;
                    break;
                }
            }
            if (!alreadyExplored) {
                int childG = current->g + 1;
                int childH = computeManhattanDistance(childState, goal);
                children.push_back(new AStarNode(childState, current, MoveType::right, childG, childH));
            }
        }

        // Push all children into the open list.
        for (AStarNode* child : children) {
            open.push(child);
        }
    }

    cout << "No solution found." << endl;
    // Cleanup remaining nodes.
    while (!open.empty()) {
        AStarNode* temp = open.top();
        open.pop();
        delete temp;
    }
}

int main() {
    cout << "*** P02 Puzzle Informed Search - A* ***" << endl;
    cout << "<======================================================>" << endl;

    cout << "Enter number of rows and columns: ";
    int size;
    cin >> size;
    cout << endl;

    PuzzleState start(size, size), goal(size, size);

    cout << "Enter Start State row by row:" << endl;
    cin >> start;
    cout << endl;

    cout << "Enter Goal State row by row:" << endl;
    cin >> goal;
    cout << endl;

    astar(start, goal);
    return 0;
}
