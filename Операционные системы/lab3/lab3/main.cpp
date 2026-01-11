#include <windows.h>
#include <iostream>
#include <string>
#include <vector>

using namespace std;

const char* FILENAME = "test.txt";
const int WAIT_MS = 500;

vector<string> ReadFileLines(HANDLE hFile) {
    vector<string> lines;
    const DWORD bufferSize = 1024;
    char buffer[bufferSize];
    DWORD bytesRead;
    string currentLine;

    while (ReadFile(hFile, buffer, bufferSize, &bytesRead, NULL) && bytesRead > 0) {
        for (DWORD i = 0; i < bytesRead; ++i) {
            char ch = buffer[i];
            if (ch == '\r') continue;
            if (ch == '\n') {
                lines.push_back(currentLine);
                currentLine.clear();
            }
            else {
                currentLine += ch;
            }
        }
    }

    if (!currentLine.empty())
        lines.push_back(currentLine);

    return lines;
}

void SetCursorToCenter(const string& text, int& count) {
    CONSOLE_SCREEN_BUFFER_INFO csbi;
    GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), &csbi);

    int centerX = (csbi.srWindow.Right - csbi.srWindow.Left - text.size()) / 2;
    int centerY = csbi.srWindow.Top + count;

    COORD pos = { static_cast<SHORT>(centerX), static_cast<SHORT>(centerY) };
    SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), pos);

    count++;
}

void PrintConsole(const string& text, int& count, WORD color) {
    SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), color);

    SetCursorToCenter(text, count);

    cout << text;
}

int main() {
    HANDLE hFile;
    int count = 0;

    while (true) {
        count = 0;

        hFile = CreateFileA(
            FILENAME,
            GENERIC_READ,
            0,
            NULL,
            OPEN_EXISTING,
            FILE_ATTRIBUTE_NORMAL,
            NULL
        );

        if (hFile != INVALID_HANDLE_VALUE) {
            PrintConsole("File opened successfully!", count, FOREGROUND_GREEN | FOREGROUND_INTENSITY);
            PrintConsole("File content:", count, FOREGROUND_BLUE | FOREGROUND_INTENSITY);

            vector<string> lines = ReadFileLines(hFile);
            
            for (const string& line : lines) {
                PrintConsole(line, count, FOREGROUND_RED | FOREGROUND_INTENSITY);
            }

            break;
        }
        else {
            DWORD err = GetLastError();
            if (err == ERROR_SHARING_VIOLATION) {
                PrintConsole("File is busy, wait...", count, FOREGROUND_RED | FOREGROUND_INTENSITY);
                Sleep(WAIT_MS);
            }
            else if (err == ERROR_FILE_NOT_FOUND) {
                PrintConsole("File not found!", count, FOREGROUND_RED | FOREGROUND_INTENSITY);
                break;
            }
            else {
                PrintConsole("Unexpected error when file opening!", count, FOREGROUND_RED | FOREGROUND_INTENSITY);
                break;
            }
        }
    }

    PrintConsole("Press Enter to escape...", count, FOREGROUND_RED | FOREGROUND_INTENSITY);
    cin.get();

    if (hFile) {
        CloseHandle(hFile);
    }

    return 0;
}
