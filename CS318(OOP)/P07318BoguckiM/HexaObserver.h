#pragma once
#include "Observer.h"
#include "Subject.h"
#include <iostream>
#include <iomanip>

class HexaObserver : public Observer {
public:
	HexaObserver(Subject* subject) {
		this->subject = subject;
		this->subject->attach(this);
	}

	void update() override {
		cout << "Hexadecimal String: " << hex << uppercase << subject->getState() << endl;
	}
};
