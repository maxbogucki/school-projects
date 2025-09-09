#include "Subject.h"
#include "BinaryObserver.h"
#include "OctalObserver.h"
#include "HexaObserver.h"

int main() {
	Subject subject;

	HexaObserver hexaObserver(&subject);
	OctalObserver octalObserver(&subject);
	BinaryObserver binaryObserver(&subject);

	cout << "First state change: 15" << endl;
	subject.setState(15);
	cout << "Second state change: 10" << endl;
	subject.setState(10);
}