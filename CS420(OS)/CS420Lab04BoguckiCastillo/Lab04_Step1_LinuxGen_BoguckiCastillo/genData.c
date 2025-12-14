#include <stdio.h>

int main(int argc, char *argv[]) {
	FILE *pF = fopen(argv[1], "w");

	for (int i = 0; i < 1024000; i++) {
		fprintf(pF, "A");
	}

	fclose(pF);

	return 0;
}
