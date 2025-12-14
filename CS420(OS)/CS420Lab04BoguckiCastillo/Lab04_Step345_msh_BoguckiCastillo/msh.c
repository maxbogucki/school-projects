#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <string.h>

int main(int argc, char *argv[]) {
    pid_t pid;
    pid = fork();
 
    if (pid == 0) {
        // Child process - check which command
        if (strcmp(argv[1], "mycopy") == 0) {
            execl("./mycopy", "mycopy", argv[2], argv[3], (char *)NULL);
        } else if (strcmp(argv[1], "myremove") == 0) {
            execl("./myremove", "myremove", argv[2], (char *)NULL);
        } else if (strcmp(argv[1], "mymove") == 0) {
            execl("./mymove", "mymove", argv[2], argv[3], (char *)NULL);
        }
    } else {
        // Parent process
        wait((int*) 0);
    }

    return 0;
}

