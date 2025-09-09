#include "tspProblem.h"
#include <bitset>
using namespace std;

static ostream& operator<<(ostream& os, const vector<vector<long int>>& g) {
	for (const auto& row : g) {
		for (const auto& val : row) {
			os << val << " ";
		}
		os << '\n';
	}
	return os;
}


int tspProblem::computeMinTourCost(int vertex, bitset<32> bits) {
	unsigned long mask = bits.to_ulong();
	if (mask == 0)
		return W->getEdgeCost(vertex, 0);

	if (D[vertex][mask] != -1)
		return D[vertex][mask];

	int bestCost = INT_MAX;
	bool visitedNode{ false };

	// If two nodes dont connect, then D[i][j] is gonna be INT_MAX;

	// make sure to check that D[][] > 0;
	for (int i = 0; i < size; i++) {
		if (bits[i]) {
			bitset<32> b = bits;
			b.reset(i);
			int cost = computeMinTourCost(i, b) + W->getEdgeCost(vertex, i);
			if (cost < 0 || cost == INT_MAX) continue;
			if (cost < bestCost) {
				visitedNode = true;
				bestCost = cost;
				P[vertex][mask] = i;

			}
		}
	}
	D[vertex][mask] = bestCost;
	return bestCost;
}

void tspProblem::solve() {
	bitset<32> bits;
	for (int i = 1; i < size; i++)
		bits.set(i);

	unsigned int cost = computeMinTourCost(0, bits);
	if (cost != INT_MAX) {
		cout << "Optimal Tour Cost = [" << cost << ", ";
		printPath();
		cout << "]";
	}
	else {
		cout << "No Hamiltonian Cycle";
	}
}

void tspProblem::printPath() const {
	unsigned int vertex = 0;
	bitset<32> bits = S;
	bits.reset(vertex);

	cout << "<" << W->getVertexName(vertex);


	while (bits.any()) {
		cout << ",";
		unsigned long currentBits = bits.to_ulong();
		vertex = P[vertex][currentBits];
		cout << W->getVertexName(vertex);
		bits.reset(vertex);
	}
	cout << "," << W->getVertexName(0) << ">";
}