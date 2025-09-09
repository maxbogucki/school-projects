#pragma once
#include <iostream>

using namespace std;

class Subject;

class Observer {
protected:
	Subject* subject;
public:
	virtual ~Observer() = default;
	virtual void update() = 0;
};