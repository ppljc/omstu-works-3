#include <iostream>
#include <unistd.h>
#include <fcntl.h>
#include <stdio.h>
#include <stdlib.h>

using namespace std;

int main() {
    const char* filename = "file.txt";

    int fd1 = open(filename, O_RDONLY);
    if (fd1 < 0) { perror("open fd1"); return 1; }

    int fd2 = dup(fd1);
    if (fd2 < 0) { perror("dup"); return 1; }

    int fd3 = open(filename, O_RDONLY);
    if (fd3 < 0) { perror("open fd3"); return 1; }

    if (lseek(fd1, 10, SEEK_SET) < 0) { perror("lseek"); return 1; }
    
    cout << "fd1 = " << fd1 << ", fd2 = " << fd2 << ", fd3 = " << fd3 << endl;

    char buf[8];
    ssize_t n;

    n = read(fd1, buf, 7);
    if (n < 0) { perror("read fd1"); return 1; }
    buf[n] = '\0';
    cout << "fd1: " << buf << endl;

    n = read(fd2, buf, 7);
    if (n < 0) { perror("read fd2"); return 1; }
    buf[n] = '\0';
    cout << "fd2: " << buf << endl;

    n = read(fd3, buf, 7);
    if (n < 0) { perror("read fd3"); return 1; }
    buf[n] = '\0';
    cout << "fd3: " << buf << endl;

    close(fd1);
    close(fd2);
    close(fd3);
    return 0;
}
