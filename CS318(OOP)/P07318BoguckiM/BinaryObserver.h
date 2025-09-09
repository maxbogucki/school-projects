#pragma once
#include "Observer.h"
#include "Subject.h"
#include <bitset>
#include <iostream>

class BinaryObserver : public Observer {
public:
	BinaryObserver(Subject* subject) {
		this->subject = subject;
		this->subject->attach(this);
	}

    void update() override {
        bitset<32> binary(subject->getState());
        string binaryString = binary.to_string();

        binaryString = binaryString.substr(binaryString.find('1'));

        cout << "Binary String: " << binaryString << endl;
    }
};