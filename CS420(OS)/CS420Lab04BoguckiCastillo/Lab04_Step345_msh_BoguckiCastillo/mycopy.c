#include <fcntl.h>
#include <unistd.h>


#define BUFFER_SIZE 4096

int main(int arc, char *argv[]) {
	char buffer[4096];
	ssize_t bytes_read;

	int source = open(argv[1], O_RDONLY);
	int target = open(argv[2], O_WRONLY | O_CREAT, 0644);

	while((bytes_read = read(source, buffer, BUFFER_SIZE)) > 0) {
		write(target, buffer, bytes_read);
	}

	close(source);
	close(target);

	return 0;
} 
