#include <iostream>
#include <queue>
#include <vector>
#include <algorithm>
#include "PuzzleMove.h" 

using namespace std;

struct PuzzleNode {
    PuzzleState currState;
    PuzzleNode* prevState;
    MoveType move;

    PuzzleNode(PuzzleState c, PuzzleNode* p, MoveType m) : currState(c), prevState(p), move(m) {}
};

void bfs(PuzzleState start, PuzzleState goal) {
    if (start == goal) {
        cout << "*** Solution found immediately: start state is the goal state. ***" << endl;
        return;
    }

    queue<PuzzleNode*> q;
    vector<PuzzleState> explored;
    int nodesExpanded = 0;

    // Mark the starting state as explored and enqueue it.
    explored.push_back(start);
    q.push(new PuzzleNode(start, nullptr, nullMove));

    while (!q.empty()) {
        PuzzleNode* current = q.front();
        q.pop();
        

        // Check for goal
        if (current->currState == goal) {
            cout << "*** Solution Found ***" << endl;

            // Reconstruct path
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

            // Cleanup remaining nodes in the queue
            delete current;
            while (!q.empty()) {
                PuzzleNode* temp = q.front();
                q.pop();
                delete temp;
            }
            return;
        }

        nodesExpanded++;

        // Generate children in the order: down, left, up, right.
        // Ensure there are no duplicates
        vector<PuzzleNode*> children;

        if (current->currState.canMoveDown()) {
            PuzzleState childState = current->currState.moveBlankDown();
            bool alreadyExplored = false;
            for (const auto& state : explored) {
                if (state == childState) { alreadyExplored = true; break; }
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
                if (state == childState) { alreadyExplored = true; break; }
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
                if (state == childState) { alreadyExplored = true; break; }
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
                if (state == childState) { alreadyExplored = true; break; }
            }
            if (!alreadyExplored) {
                explored.push_back(childState);
                children.push_back(new PuzzleNode(childState, current, MoveType::right));
            }
        }

        // Enqueue children
        for (PuzzleNode* child : children) {
            q.push(child);
        }
    }

    cout << "No solution found." << endl;
}

int main() {
    cout << "*** P01 Puzzle Uninformed Search - BFS ***" << endl;
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

    bfs(start, goal);
    return 0;
}
