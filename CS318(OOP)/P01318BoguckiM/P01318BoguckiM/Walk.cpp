#include "Walk.h"
#include <cmath>
#include "UtilRand.h"

// Default Constructor
Walk::Walk() : x(0), y(0), distance(0) {}

// Functions
void Walk::setX(double x) {
    this->x = x;
}

double Walk::getX() const {
    return x;
}

void Walk::setY(double y) {
    this->y = y;
}

double Walk::getY() const {
    return y;
}

void Walk::setDistance(double distance) {
    this->distance = distance;
}

double Walk::getDistance() const {
    return distance;
}

int Walk::generateAngle() {
    return generateRandomInRange(1, 359);
}

int Walk::generateLength() {
    return generateRandomInRange(1, 100);
}

double Walk::convertToRadians(double degree) {
    const double pi = 3.14159265358979323846;
    return degree * pi / 180;
}

void Walk::walkAgain() {
    int length = generateLength();
    int angle = generateAngle();

    double angleRadians = convertToRadians(angle);

    x += length * cos(angleRadians);
    y += length * sin(angleRadians);

    distance += length;
}

std::ostream& operator<<(std::ostream& os, const Walk& w) {
    os << "Point: " << w.getX() << ", " << w.getY() << " Distance: " << w.getDistance();
    return os;
}