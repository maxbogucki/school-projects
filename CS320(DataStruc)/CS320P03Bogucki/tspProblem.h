#pragma once
#include <bitset>
#include <vector>
#include "graph.h"
using namespace std;

class tspProblem {
private:
	vector<vector<long int>> D;
	vector<vector<long int>> P;
	Graph* W;
	int computeMinTourCost(int i, bitset<32> bits);
	size_t size;
	const size_t MAX_BITS = 32;
	unsigned long fullMask;
	bitset<32> S;
	void printPath() const;

public:
	// constructor which takes in a pointer
	// to a graph.
	tspProblem(Graph* g) : W(g), size(g->getNumVertices()) {
		fullMask = (1 << size); // 1*2^size, we can do powers
		// quickly via bitshifts.
		for (size_t i = 0; i < size; i++)
			S.set(i);

		// this method avoids overflows:
		D.resize(size, vector<long int>(static_cast<size_t>(1) << size, -1));
		P.resize(size, vector<long int>(static_cast<size_t>(1) << size, -1));
	}


	void solve();
};