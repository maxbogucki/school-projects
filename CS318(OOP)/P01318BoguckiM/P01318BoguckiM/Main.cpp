// Max Bogucki
// CS318
// P01
// Walk to a certain amount of points a certain amount of times and get different data based on results

#include <iostream>
#include <vector>
#include <algorithm>
#include <limits>
#include <iomanip>
#include "UtilRand.h"
#include "Walk.h"

using namespace std;

int main() {
	initRandom();

	cout << "***Walks***" << endl << endl;

	cout << "This program generates a number of random walks." << endl << endl;

	int numPoints, numWalks;
	cout << "Enter number of points for each walk: ";
	cin >> numPoints;
	cout << endl;

	cout << "Enter number of random walks to generate: ";
	cin >> numWalks;
	cout << endl;

	vector<Walk> walks(numWalks);
	
	// Displays Walks and Steps
	cout << fixed << setprecision(2);
	for (int i = 0; i < numWalks; i++) {
		for (int j = 0; j < numPoints; j++) {
			walks[i].walkAgain();
			cout << "Walk/Step: " << i + 1 << "/" << j + 1 << ": Point: " << walks[i].getX() << ", " << walks[i].getY() << " Distance: " << walks[i].getDistance() << endl;
		}
		cout << endl;
	}

	cout << "Walks Sorted: " << endl;

	vector<double> distances;

	// Finds distances and displays data
	double totalDistance = 0, shortest = 0, median = 0, longest = 0;
	for (int i = 0; i < walks.size(); i++) {
		double currDistance = walks[i].getDistance();
		distances.push_back(currDistance);

		cout << "	Point: " << walks[i].getX() << ", " << walks[i].getY() << " Distance: " << walks[i].getDistance() << endl;
		totalDistance += walks[i].getDistance();

		// Finds shortest distance
		if (currDistance < shortest || i == 0) {
			shortest = currDistance;
		}

		// Finds median distance
		sort(distances.begin(), distances.end());
		int size = distances.size();
		if (size % 2 == 0) {
			median = (distances[size / 2 - 1] + distances[size / 2]) / 2;
		}
		else {
			median = distances[size / 2];
		}

		// Finds longest distance
		if (currDistance > longest) {
			longest = currDistance;
		}
	}
	cout << endl;
	
	// Display averages and different distances
	cout << "Average Distance: " << totalDistance / numWalks << endl << endl;

	cout << "Shortest Distance/Walk: " << shortest << endl;
	cout << "Median Distance/Walk: " << median << endl;
	cout << "Longest Distance/Walk: " << longest << endl;
}