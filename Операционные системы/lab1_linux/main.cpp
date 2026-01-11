#include <iostream>
#include <unistd.h>
#include <string.h>
#include <fcntl.h>

using namespace std;

int main() {
    int inputH  = STDIN_FILENO;
    int outputH = STDOUT_FILENO;

    bool stdin_is_tty  = isatty(inputH);
    bool stdout_is_tty = isatty(outputH);

    char buffer[256];
    ssize_t bytesRead, bytesWritten;

	if (stdin_is_tty) {
		const char* inputText = "Enter text: ";
		if (!stdout_is_tty) {
			int terminalH = open("/dev/tty", O_WRONLY | O_CLOEXEC);
            write(terminalH, inputText, strlen(inputText));
		} else {
			write(outputH, inputText, strlen(inputText));
		}
	}

    bytesRead = read(inputH, buffer, sizeof(buffer) - 1);
    if (bytesRead < 0) {
        cerr << "Read error." << endl;
        return 1;
    }
    buffer[bytesRead] = '\0';

    const char* outputText = "You entered: ";
    write(outputH, outputText, strlen(outputText));

    write(outputH, buffer, bytesRead);

    return 0;
}
