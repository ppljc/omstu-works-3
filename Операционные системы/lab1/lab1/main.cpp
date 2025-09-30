#include <iostream>
#include <Windows.h>

using namespace std;

int main() {
	HANDLE inputH = GetStdHandle(STD_INPUT_HANDLE);
	HANDLE outputH = GetStdHandle(STD_OUTPUT_HANDLE);

	cout << "Standart input handle: " << inputH << endl;
	cout << "Standart output handle: " << outputH << endl;

	char buffer[256];

	DWORD bytesRead = 0;
	DWORD bytesWritten = 0;

	const char* inputText = "Enter text: ";
	WriteFile(outputH, inputText, (DWORD)strlen(inputText), &bytesWritten, NULL);

	if (!ReadFile(inputH, buffer, sizeof(buffer) - 1, &bytesRead, NULL)) {
		cerr << "Read error." << endl;
		return 1;
	}
	buffer[bytesRead] = '\0';

	const char* outputText = "You entered: ";
	WriteFile(outputH, outputText, (DWORD)strlen(outputText), &bytesWritten, NULL);

	WriteFile(outputH, buffer, bytesRead, &bytesWritten, NULL);

	return 0;
}
