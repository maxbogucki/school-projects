#include "graph.h"
#include "tspProblem.h"
#include <bitset>

ifstream getFile(string& filename);

int main() {
    Graph* g = new Graph();
    string filename;
    ifstream f = getFile(filename);
    assert(f.is_open());
    g->read(f);

    cout << filename << " Contents: " << endl << endl;
    g->print();
    cout << endl;

    cout << "*********Output according to " << filename << " contents: " << endl << endl;
    tspProblem salesperson(g);
    salesperson.solve();
    cout << endl;

    delete g;
    return 0;
}

ifstream getFile(string& filename) {
    cout << "Please input file name: ";
    cin >> filename;
    cout << endl;
    ifstream f(filename, ios::in);
    return f;
}