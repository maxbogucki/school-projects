#pragma once
#include <iostream>
#include <list>
#include "Observer.h"
using namespace std;

class Subject {
private:
	list<Observer*> observers;
	int state;
public:
	Subject() : state(0) {}

	int getState() {
		return state;
	}

	void setState(int state) {
		this->state = state;
		notifyAllObservers();
	}

	void attach(Observer* observer) {
		observers.push_back(observer);
	}

	void notifyAllObservers() {
		for (Observer* observer : observers) {
			observer->update();
		}
	}
};