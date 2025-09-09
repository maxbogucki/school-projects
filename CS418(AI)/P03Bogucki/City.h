#ifndef CITY_H
#define CITY_H

#include <string>

using namespace std;

class City {
private:
	int x;
	int y;
	string cityName;

public:
	City() : x(0), y(0), cityName("") {}

	City(const string &cityName, int x, int y)
		: cityName(cityName), x(x), y(y) {}

	int getX() const { return x; }
	void setX(int x) { this->x = x; }

	int getY() const { return y; }
	void setY(int y) { this->y = y; }

	string getCityName() const { return cityName; }
	void setCityName(const string& cityName) { this->cityName = cityName; }
};

#endif