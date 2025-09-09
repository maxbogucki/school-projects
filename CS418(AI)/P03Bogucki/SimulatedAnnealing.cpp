#include <iostream>
#include <fstream>
#include <sstream>
#include <vector>
#include <string>
#include <cstdlib>
#include <ctime>
#include "City.h"
#include "Tour.h"

using namespace std;

double acceptanceProbability(int currentDistance, int newDistance, double temperature) {
    if (newDistance < currentDistance) return 1.0;
    return exp((currentDistance - newDistance) / temperature);
}

double randomDouble() {
    return static_cast<double>(rand()) / RAND_MAX;
}

int randomInt(int min, int max) {
    return min + rand() % (max - min);
}

int main() {
    srand(static_cast<unsigned int>(time(NULL)));

    string filename;
    char runAgain;

    do {
        double temp;
        double coolingRate;

        cout << "*** TSP using Simulated Annealing ***" << endl << endl;
        cout << "Starting Temperature: ";
        cin >> temp;
        cout << endl;
        cout << "Cooling Rate: ";
        cin >> coolingRate;
        cout << endl;

        vector<City> cities;

        // Keep asking for a valid filename until successful or user quits
        while (true) {
            cout << "Please enter a file name (quit to end): ";
            cin >> filename;
            cout << endl;

            if (filename == "quit") {
                return 0;
            }

            ifstream inFile(filename);
            if (!inFile) {
                cerr << "Error opening file: " << filename << ". Please try again." << endl << endl;
                continue;
            }

            string cityName;
            int x, y;
            cities.clear();

            while (getline(inFile, cityName)) {
                if (!(inFile >> x >> y)) {
                    cerr << "Error reading coordinates for city: " << cityName << endl;
                    break;
                }
                inFile.ignore();
                cities.push_back(City(cityName, x, y));
            }
            inFile.close();

            if (!cities.empty()) {
                break;
            }
            else {
                cerr << "No cities loaded from file. Please try again." << endl << endl;
            }
        }

        // Output the list of destination cities
        cout << "Cities:" << endl;
        for (const City& c : cities) {
            cout << "       " << c.getCityName() << " " << c.getX() << " " << c.getY() << endl;
        }
        cout << endl;

        // Create an initial solution
        Tour currentSolution;
        currentSolution.generateInitialTour(cities);

        cout << "Initial solution distance: " << currentSolution.getTotalDistance() << endl;
        cout << "Tour:" << endl;
        currentSolution.printTour();
        cout << endl;

        Tour* best = new Tour(currentSolution.getTour());

        int newStatesTested = 0;
        int newStatesAccepted = 0;

        // Simulated annealing loop
        while (temp > 1) {
            Tour newSolution(currentSolution.getTour());

            // Pick two random tour positions
            int tourPos1 = randomInt(0, newSolution.tourSize());
            int tourPos2 = randomInt(0, newSolution.tourSize());
            while (tourPos1 == tourPos2) {
                tourPos2 = randomInt(0, newSolution.tourSize());
            }

            // Swap two cities
            City citySwap1 = newSolution.getCity(tourPos1);
            City citySwap2 = newSolution.getCity(tourPos2);
            newSolution.setCity(tourPos1, citySwap2);
            newSolution.setCity(tourPos2, citySwap1);

            newStatesTested++;

            // Get distances of the current and new solutions
            int currentDistance = currentSolution.getTotalDistance();
            int neighbourDistance = newSolution.getTotalDistance();

            // Calculate acceptance probability
            double ap = acceptanceProbability(currentDistance, neighbourDistance, temp);
            double randVal = randomDouble();

            if (ap > randVal) {
                currentSolution = Tour(newSolution.getTour());
                newStatesAccepted++;
            }

            // Update the best solution if the current one is better
            if (currentSolution.getTotalDistance() < best->getTotalDistance()) {
                *best = Tour(currentSolution.getTour());
            }

            // Cool down the temperature
            temp *= (1 - coolingRate);
        }

        // Output statistics
        cout << "Number of new states tested: " << newStatesTested << endl;
        cout << "Number of new states accepted: " << newStatesAccepted << endl << endl;
        cout << "Final solution distance: " << best->getTotalDistance() << endl;
        cout << "Tour:" << endl;
        best->printTour();

        delete best;

        cout << endl << "Do you want to run the program again? (y/n): ";
        cin >> runAgain;
        cout << endl << endl;

    } while (runAgain == 'y' || runAgain == 'Y');

    return 0;
}