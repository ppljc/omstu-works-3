#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <sstream>
#include <unistd.h>
#include <fcntl.h>
#include <sys/ioctl.h>
#include <cstring>
#include <errno.h>

using namespace std;

void printCentered(const string& msg, const string& color, int& row) {
    struct winsize w;
    ioctl(STDOUT_FILENO, TIOCGWINSZ, &w);
    int col = (w.ws_col - msg.length()) / 2;
    cout << "\033[" << row << ";" << col << "H" << color  << msg  << "\033[0m" << endl;
    row++;
}

void printFileContent(int fd, int& row) {
    string content;
    char buffer[1024];
    ssize_t bytes_read;
    while ((bytes_read = read(fd, buffer, sizeof(buffer))) > 0) {
        content.append(buffer, bytes_read);
    }

    vector<string> lines;
    stringstream ss(content);
    string line;
    while (getline(ss, line)) {
        lines.push_back(line);
    }

    if (!content.empty() && content.back() != '\n') {
        lines.push_back("");
    }

    for (const auto& l : lines) {
        cout << "\033[" << row << ";1H" << l << endl;
        row++;
    }
}

int main() {
    const char* filename = "test.txt";
    int sleep_time = 7;
    int row = 1;

    cout << "\033[2J\033[H";

    int fd = open(filename, O_RDWR);
    if (fd == -1) {
        string err_msg = "Error opening file: " + string(strerror(errno));
        printCentered(err_msg, "\033[31m", row);
        return 1;
    }

    struct flock fl;
    fl.l_type = F_WRLCK;
    fl.l_whence = SEEK_SET;
    fl.l_start = 0;
    fl.l_len = 0;

    if (fcntl(fd, F_SETLK, &fl) == -1) {
        if (errno == EAGAIN || errno == EACCES) {
            printCentered("Cannot lock now, waiting...", "\033[33m", row);

            if (fcntl(fd, F_SETLKW, &fl) == -1) {
                string err_msg = "Error setting lock: " + string(strerror(errno));
                printCentered(err_msg, "\033[31m", row);
                close(fd);
                return 1;
            }
        }
        else {
            string err_msg = "Error attempting lock: " + string(strerror(errno));
            printCentered(err_msg, "\033[31m", row);
            close(fd);
            return 1;
        }
    }
    else {
        printCentered("Lock acquired successfully", "\033[32m", row);
    }

    row++;

    lseek(fd, 0, SEEK_SET);
    printCentered("File content:", "\033[34m", row);
    printFileContent(fd, row);
	row++;
	
    sleep(sleep_time);

    fl.l_type = F_UNLCK;
    if (fcntl(fd, F_SETLK, &fl) == -1) {
        string err_msg = "Error unlocking: " + string(strerror(errno));
        printCentered(err_msg, "\033[31m", row);
    }
    else {
        printCentered("Lock released", "\033[32m", row);
    }

    close(fd);
    return 0;
}
