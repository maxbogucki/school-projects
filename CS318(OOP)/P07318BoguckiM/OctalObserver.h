#pragma once
#include "Observer.h"
#include "Subject.h"
#include <iostream>
#include <iomanip>

class OctalObserver : public Observer {
public:
	OctalObserver(Subject* subject) {
		this->subject = subject;
		this->subject->attach(this);
	}

	void update() override {
		cout << "Octal String: " << oct << subject->getState() << endl;
	}
};