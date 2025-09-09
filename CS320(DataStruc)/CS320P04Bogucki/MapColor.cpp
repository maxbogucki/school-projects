#include "MapColor.h"


/*
* takes in a vertex and color
* returns false if there are neighbors with same color
* returns true if there are NO neighbors with same color
*/
bool MapColor::goodChoice(size_t v, int color) const {
	const auto& adjSet = myList.getAdj(v);

	/*
	* its a good color if none of the adjacent vertexes
	* are colored the same as the vertex
	*/

	// colorings is a mapping from V -> C
	for (const auto& adjV : adjSet) {
		if (colorings.at(adjV) == color) {
			return false;
		}
	}


	return true;
}



bool MapColor::backtrack(size_t vertex) {

	// if every node is colored, you're all done
	if (vertex == myList.getSize())
		return true;

	else {


		/*
		* for each possible color
		* 	 if it's a good color,
		* 	 color it that, and call backtrack again.
		*/
		for (size_t color = 0; color < numColors; color++) {
			if (goodChoice(vertex, color)) {
				colorings.at(vertex) = color;
				if (backtrack(vertex + 1))
					return true;
				colorings.at(vertex) = -1;
			}
			else
				continue;
		}

		return false;

	}

}

void MapColor::solve() {
	backtrack(0);
}

void MapColor::getInput() {
	char dummy;
	int useful1;
	int useful2;
	cin >> numColors;
	cin >> numEdges;
	assert(numEdges >= 0);
	for (size_t i = 0; i < numEdges; i++) {
		// TODO: can we squish this into 1 cin statement?

		// lParen
		cin >> dummy;
		// first num of pair
		cin >> useful1;
		// second num of pair
		cin >> useful2;
		// rParen
		cin >> dummy;
		if (useful1 != -1 && useful2 != -1) {
			myList.addEdge(useful1, useful2);
			myList.addEdge(useful2, useful1);
		}
		else
			break;
	}
	colorings.assign(myList.getSize(), -1);
}

void MapColor::printOutput() const {
	// make sure there is a proper coloring
	assert(colorings.size() >= 0);
	if (colorings[0] == -1)
		cout << "No possible colorings." << endl;
	else
		for (size_t i = 0; i < colorings.size(); i++)
			cout << i << ": " << colorings[i] + 1 << "\n";
}

void MapColor::AdjList::addEdge(int useful1, int useful2) {
	if (useful1 >= v.size()) {
		v.push_back(set<int>{useful2});
	}
	else {
		v[useful1].insert(useful2);
	}
}

// returns a reference to a set. You cannot use the reference to modify the set
const set<int>& MapColor::AdjList::getAdj(size_t n) const {
	if (n >= v.size()) {
		throw("AdjList out of range");
	}
	return v[n];
}