#include <iostream>
#include <fstream>
#include <string>
#include <windows.h>

using namespace std;

int main() {
    HANDLE hInput = GetStdHandle(STD_INPUT_HANDLE);
    HANDLE hOutput = GetStdHandle(STD_OUTPUT_HANDLE);
    DWORD oldMode, newMode;
    GetConsoleMode(hInput, &oldMode);

    newMode = oldMode | ENABLE_MOUSE_INPUT | ENABLE_EXTENDED_FLAGS;
    newMode &= ~(ENABLE_QUICK_EDIT_MODE | ENABLE_LINE_INPUT | ENABLE_ECHO_INPUT);
    SetConsoleMode(hInput, newMode);

    string filename = "input.txt";
    ifstream file(filename);
    if (!file.is_open()) {
        cout << "Cannot open file\n";
        return 1;
    }

    string line;
    int lineCount = 0;
    while (getline(file, line)) {
        cout << line << "\n";
        lineCount++;
    }
    file.close();

    INPUT_RECORD input;
    DWORD events;
    COORD coord;
    CHAR ch;

    while (true) {
        ReadConsoleInput(hInput, &input, 1, &events);
        if (input.EventType == MOUSE_EVENT) {
            MOUSE_EVENT_RECORD mer = input.Event.MouseEvent;
            if (mer.dwEventFlags == 0 && (mer.dwButtonState & FROM_LEFT_1ST_BUTTON_PRESSED)) {
                coord = mer.dwMousePosition;
                CONSOLE_SCREEN_BUFFER_INFO csbi;
                GetConsoleScreenBufferInfo(hOutput, &csbi);
                DWORD read;
                ReadConsoleOutputCharacterA(hOutput, &ch, 1, coord, &read);
                COORD infoPos = {0, static_cast<SHORT>(csbi.dwSize.Y - 1)};
                SetConsoleCursorPosition(hOutput, infoPos);
                if (read > 0 && ch != ' ') {
                    cout << "Char: " << ch << "  Line: " << coord.Y + 1 << "  Col: " << coord.X + 1 << "          ";
                } else {
                    cout << string(csbi.dwSize.X, ' ');
                }
            }
            if (mer.dwEventFlags == 0 && (mer.dwButtonState & RIGHTMOST_BUTTON_PRESSED)) {
                break;
            }
        }
    }

    SetConsoleMode(hInput, oldMode);
    return 0;
}
