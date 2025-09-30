#include <unistd.h>
#include <fcntl.h>
#include <stdio.h>
#include <stdlib.h>

using namespace std;

int main() {
    const char* filename = "file.txt";
    int fd = open(filename, O_WRONLY | O_CREAT | O_TRUNC, 0644);
    if (fd < 0) {
        perror("open");
        return 1;
    }

    char buffer[4096];
    ssize_t n;
    while ((n = read(STDIN_FILENO, buffer, sizeof(buffer))) > 0) {
        if (write(fd, buffer, n) != n) {
	        perror("write");
	        close(fd);
	        return 1;
        }
    }

    if (n < 0) {
        perror("read");
    }

    close(fd);
    return 0;
}
