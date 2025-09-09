#pragma once

#include <iostream>
#include <vector>
#include <set>
#include <cassert>

using namespace std;

class MapColor {
private:
	// should we use adjMat or adjList

	/*
	* Here, I'm using a nested class for a few reasons:
	*	I dont want to immediately construct an adjMat. I want to do that in getInput();
	*	I want to hide the underlying datastructure from the user
	*		The user of the MapColor likely doesn't (and shouldn't) care about the adjMat datastructure.
	*/
	class AdjList {
	private:
		vector<set<int>> v;
	public:
		// just use the default constructor. 
		// no reason to do anything else.
		void addEdge(int useful1, int useful2);
		const set<int>& getAdj(size_t n) const;
		size_t getSize() const { return v.size(); }
	};

	size_t numColors;
	size_t numEdges;
	bool backtrack(size_t vertex);
	AdjList myList;
	vector<int> colorings;
	bool goodChoice(size_t v, int color) const;
public:
	void solve();
	void getInput();
	void printOutput() const;
};