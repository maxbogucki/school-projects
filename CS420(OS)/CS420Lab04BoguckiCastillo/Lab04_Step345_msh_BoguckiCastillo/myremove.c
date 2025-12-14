#include <unistd.h>
#include <sys/stat.h>

int main(int argc, char *argv[]) {
	struct stat file_info;

	stat(argv[1], &file_info);

	if (S_ISREG(file_info.st_mode)) {
		unlink(argv[1]);
	} else if (S_ISDIR(file_info.st_mode)) {
		rmdir(argv[1]);
	}

	return 0;
}
