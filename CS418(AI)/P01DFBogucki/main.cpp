#include <iostream>
#include <stack>
#include <vector>
#include <algorithm>
#include "PuzzleMove.h"

using namespace std;

struct PuzzleNode {
    PuzzleState currState;
    PuzzleNode* prevState;
    MoveType move;

    PuzzleNode(PuzzleState c, PuzzleNode* p, MoveType m)
        : currState(c), prevState(p), move(m) {}
};

void dfs(PuzzleState start, PuzzleState goal) {
    if (start == goal) {
        cout << "*** Solution found immediately: start state is the goal state. ***" << endl;
        return;
    }

    stack<PuzzleNode*> s;
    vector<PuzzleState> explored;
    int nodesExpanded = 0;

    // Mark the starting state as explored and push it onto the stack
    explored.push_back(start);
    s.push(new PuzzleNode(start, nullptr, nullMove));

    while (!s.empty()) {
        PuzzleNode* current = s.top();
        s.pop();

        // Check if the current state is the goal
        if (current->currState == goal) {
            cout << "*** Solution Found ***" << endl;

            // Reconstruct the path
            vector<PuzzleState> pathStates;
            vector<MoveType> pathMoves;
            PuzzleNode* node = current;
            while (node != nullptr) {
                pathStates.push_back(node->currState);
                if (node->prevState != nullptr) {
                    pathMoves.push_back(node->move);
                }
                node = node->prevState;
            }
            reverse(pathStates.begin(), pathStates.end());
            reverse(pathMoves.begin(), pathMoves.end());

            // Print the solution path
            if (!pathStates.empty()) {
                cout << pathStates[0] << endl;
                for (size_t i = 0; i < pathMoves.size(); ++i) {
                    if (pathMoves[i] == MoveType::down) {
                        cout << "down" << endl;
                    }
                    else if (pathMoves[i] == MoveType::left) {
                        cout << "left" << endl;
                    }
                    else if (pathMoves[i] == MoveType::up) {
                        cout << "up" << endl;
                    }
                    else if (pathMoves[i] == MoveType::right) {
                        cout << "right" << endl;
                    }
                    cout << pathStates[i + 1] << endl;
                }
            }

            cout << "Number of nodes expanded: " << nodesExpanded << endl;
            cout << "Path length to Goal: " << pathMoves.size() << endl;

            // Clean up remaining nodes
            delete current;
            while (!s.empty()) {
                PuzzleNode* temp = s.top();
                s.pop();
                delete temp;
            }
            return;
        }

        nodesExpanded++;

        // Generate children in the order: down, left, up, right.
        vector<PuzzleNode*> children;

        if (current->currState.canMoveDown()) {
            PuzzleState childState = current->currState.moveBlankDown();
            bool alreadyExplored = false;
            for (const auto& state : explored) {
                if (state == childState) {
                    alreadyExplored = true;
                    break;
                }
            }
            if (!alreadyExplored) {
                explored.push_back(childState);
                children.push_back(new PuzzleNode(childState, current, MoveType::down));
            }
        }
        if (current->currState.canMoveLeft()) {
            PuzzleState childState = current->currState.moveBlankLeft();
            bool alreadyExplored = false;
            for (const auto& state : explored) {
                if (state == childState) {
                    alreadyExplored = true;
                    break;
                }
            }
            if (!alreadyExplored) {
                explored.push_back(childState);
                children.push_back(new PuzzleNode(childState, current, MoveType::left));
            }
        }
        if (current->currState.canMoveUp()) {
            PuzzleState childState = current->currState.moveBlankUp();
            bool alreadyExplored = false;
            for (const auto& state : explored) {
                if (state == childState) {
                    alreadyExplored = true;
                    break;
                }
            }
            if (!alreadyExplored) {
                explored.push_back(childState);
                children.push_back(new PuzzleNode(childState, current, MoveType::up));
            }
        }
        if (current->currState.canMoveRight()) {
            PuzzleState childState = current->currState.moveBlankRight();
            bool alreadyExplored = false;
            for (const auto& state : explored) {
                if (state == childState) {
                    alreadyExplored = true;
                    break;
                }
            }
            if (!alreadyExplored) {
                explored.push_back(childState);
                children.push_back(new PuzzleNode(childState, current, MoveType::right));
            }
        }

        // Push all children onto the stack
        for (PuzzleNode* child : children) {
            s.push(child);
        }
    }

    cout << "No solution found." << endl;
    while (!s.empty()) {
        PuzzleNode* temp = s.top();
        s.pop();
        delete temp;
    }
}

int main() {
    cout << "*** P01 Puzzle Uninformed Search - DFS ***" << endl;
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

    dfs(start, goal);
    return 0;
}
