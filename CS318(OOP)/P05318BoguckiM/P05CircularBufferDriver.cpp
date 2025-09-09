// P05CircularBufferDriver.cpp -- starter code

/*  REQUIREMENTS NOTES:

    Fill in the remainder of this driver program.
    Replicate exactly the sample run provided with project's specs.
    Use a capacity of 5

*/
#include <iostream>
#include <algorithm>
#include <iterator>
#include <vector>
#include "circular_buffer.h"
#include "dog.h"

using namespace std;

int main() {
    cout << "\n*************** <int> Circular Buffer Demo ******************\n";

    CircularBuffer<int, 5> intBuffer;
    cout << "Initial state\n" << intBuffer;

    for (int i = 0; i < 8; ++i) {
        intBuffer.push_back(i * 10);
        cout << "Pushing " << i * 10 << "\n" << intBuffer;
    }

    for (int i = 0; i < 5; ++i) {
        cout << "Popping: " << intBuffer.head() << endl;
        intBuffer.pop();
        cout << intBuffer;
    }

    cout << "\n*************** <string> Circular Buffer Demo ******************\n";


    CircularBuffer<string, 5> stringBuffer;
    cout << "Initial state\n" << stringBuffer;

    vector<string> words = { "of", "oz", "the", "wonderful", "world" };

    copy(words.begin(), words.end(), back_inserter(stringBuffer));
    cout << "After using back_insert_iterator\n" << stringBuffer;

    for (size_t i = 0; i < words.size(); ++i) {
        cout << "Popping: " << stringBuffer.head() << endl;
        stringBuffer.pop();
        cout << stringBuffer;
    }


    cout << "\n*************** <Dog> Circular Buffer Demo ******************\n";

    CircularBuffer<dog, 5> dogBuffer;

    vector<dog> dogs = {
        dog("Guinness", "Wheaten", 9),
        dog("Grimlock", "Lab", 2),
        dog("Optimus", "Bulldog", 5),
        dog("Murphy", "Lab", 14),
        dog("Floyd", "Beagle", 12)
    };


    copy(dogs.begin(), dogs.end(), back_insert_iterator<CircularBuffer<dog, 5>>(dogBuffer));
    cout << endl << dogBuffer;

    cout << "\ndogs Full? : " << (dogBuffer.full() ? "true" : "false") << endl;

    dog newDog1 = { "Snoopy", "Beagle", 100 };
    dogBuffer.push_back(newDog1);
    cout << "\nAfter pushing new dog: " << newDog1.getName() << endl;
    cout << dogBuffer;

    dog newDog2 = { "Archie", "Brittany", 100 };
    dogBuffer.push_back(newDog1);
    cout << "\nAfter pushing new dog: " << newDog2.getName() << endl;
    cout << dogBuffer;

    dog newDog3 = { "Penny", "Beagle", 100 };
    dogBuffer.push_back(newDog1);
    cout << "\nAfter pushing new dog: " << newDog3.getName() << endl;
    cout << dogBuffer;

    cout << "\nPopping oldest dog: " << dogBuffer.head().getName() << endl;
    dogBuffer.pop();
    cout << dogBuffer;
}
