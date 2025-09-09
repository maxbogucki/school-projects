#ifndef TOUR_H
#define TOUR_H

#include <vector>
#include <string>
#include <algorithm>
#include <cmath>
#include <iostream>
#include "City.h"

using namespace std;

class Tour {
private:
    vector<City> tour;
    int distance = 0;

    static double euclideanDistance(const City& a, const City& b) {
        int dx = a.getX() - b.getX();
        int dy = a.getY() - b.getY();
        return sqrt(dx * dx + dy * dy);
    }

public:
    Tour() : distance(0) {}

    Tour(const ::vector<City>& tourList)
        : tour(tourList), distance(0) {}

    vector<City> getTour() const { return tour; }

    City getCity(int index) const { return tour.at(index); }

    void setCity(int index, const City& city) {
        tour.at(index) = city;
        distance = 0;
    }

    int tourSize() const { return static_cast<int>(tour.size()); }

    void generateInitialTour(const vector<City>& destinationCities) {
        tour = destinationCities;
        random_shuffle(tour.begin(), tour.end());
        distance = 0;
    }

    int getTotalDistance() {
        if (distance == 0 && !tour.empty()) {
            int total = 0;
            for (int i = 0; i < tourSize(); ++i) {
                const City& from = getCity(i);
                const City& to = getCity((i + 1) % tourSize());
                total += static_cast<int>(euclideanDistance(from, to));
            }
            distance = total;
        }
        return distance;
    }

    void printTour() const {
        if (tour.empty()) return;
        cout << tour.front().getCityName();
        for (int i = 1; i < tourSize(); ++i) {
            cout << " -> " << tour[i].getCityName();
        }
        cout << "\n";
    }
};

#endif 