#include <iostream>
#include <vector>
#include <queue>
#include <cstdlib>
#include <ctime>

using namespace std;

int main() {
	cout << "**** Dorothy and Emeralds ****\n\n";

	int baskets;
	while (true) {
		cout << "Enter the number of baskets: ";
		cin >> baskets;

		if (baskets < 0) {
			cout << "Error: Number of baskets can't be negative.\n\n";
		}
		else {
			cout << endl;
			break;
		}
	}

	int minutes;
	while (true) {
		cout << "Enter the number of minutes: ";
		cin >> minutes;

		if (minutes < 0) {
			cout << "Error: Number of minutes can't be negative.\n\n";
		}
		else {
			cout << endl;
			break;
		}
	}

	srand(static_cast<unsigned int>(time(0)));

	if (baskets == 0 || minutes == 0) {
		cout << "Dorothy has gathered 0 emeralds!\n";
		return 0;
	}

	priority_queue<int> basketsQueue;

	// Populate baskets with emeralds (1-300 emeralds/basket)
	for (int i = 0; i < baskets; i++) {
		basketsQueue.push(rand() % 300 + 1);
	}

	// Collect emeralds for the given number of minutes
	int result = 0;
	for (int i = 0; i < minutes; i++) {
		result += basketsQueue.top();
		int temp = basketsQueue.top() / 2;
		basketsQueue.pop();
		if (temp != 0) {
			basketsQueue.push(temp);
		}
	}

	cout << "Dorothy has gathered " << result << " emeralds!\n";
}