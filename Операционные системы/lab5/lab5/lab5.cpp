#include <iostream>
#include <fstream>
#include <windows.h>
#include <string>
#include <vector>  // Добавили для vector

using namespace std;

int main() {
    // Укажите имя файла здесь или передайте как аргумент командной строки
    string filename = "input.txt";  // Замените на реальное имя файла

    // Открываем файл для чтения
    ifstream file(filename);
    if (!file.is_open()) {
        cerr << "Ошибка открытия файла: " << filename << endl;
        return 1;
    }

    // Выводим содержимое файла в консоль
    string line;
    vector<string> lines;  // Сохраняем строки для возможного использования позже
    while (getline(file, line)) {
        cout << line << endl;
        lines.push_back(line);
    }
    file.close();

    // Добавляем пустую строку в конец для статуса (если нужно)
    cout << endl;

    // Получаем дескрипторы консоли
    HANDLE hStdin = GetStdHandle(STD_INPUT_HANDLE);
    HANDLE hStdout = GetStdHandle(STD_OUTPUT_HANDLE);

    if (hStdin == INVALID_HANDLE_VALUE || hStdout == INVALID_HANDLE_VALUE) {
        cerr << "Ошибка получения дескрипторов консоли" << endl;
        return 1;
    }

    // Включаем режим ввода мыши
    DWORD mode;
    if (!GetConsoleMode(hStdin, &mode)) {
        cerr << "Ошибка получения режима консоли" << endl;
        return 1;
    }
    mode |= ENABLE_MOUSE_INPUT;  // Убрали несуществующий ENABLE_EXTENDED_MOUSE_INPUT
    if (!SetConsoleMode(hStdin, mode)) {
        cerr << "Ошибка установки режима консоли" << endl;
        return 1;
    }

    cout << "Режим мыши включен. Кликайте по тексту (левая кнопка) или завершите (правая). Для отладки: события будут логироваться." << endl;

    // Получаем информацию о буфере консоли (обновляем после вывода)
    CONSOLE_SCREEN_BUFFER_INFO csbi;
    if (!GetConsoleScreenBufferInfo(hStdout, &csbi)) {
        cerr << "Ошибка получения информации о буфере консоли" << endl;
        return 1;
    }

    // Цикл опроса событий мыши (читаем все доступные события за раз для избежания блокировок)
    while (true) {
        INPUT_RECORD inputRecord[128];  // Буфер для нескольких событий
        DWORD eventsAvailable;
        DWORD eventsRead;

        // Проверяем доступные события (не блокируемся)
        if (!PeekConsoleInput(hStdin, inputRecord, 128, &eventsAvailable)) {
            cerr << "Ошибка PeekConsoleInput" << endl;
            continue;
        }

        if (eventsAvailable == 0) {
            // Нет событий — небольшая пауза, чтобы не жрать CPU
            Sleep(50);
            continue;
        }

        // Читаем все доступные события
        if (!ReadConsoleInput(hStdin, inputRecord, eventsAvailable, &eventsRead)) {
            cerr << "Ошибка чтения ввода консоли" << endl;
            continue;
        }

        // Обрабатываем каждое событие
        for (DWORD i = 0; i < eventsRead; ++i) {
            INPUT_RECORD& rec = inputRecord[i];

            // Отладочный вывод типа события (уберите после тестирования)
            cout << "Событие: " << rec.EventType << " (KEY=0, MOUSE=1, WINDOW=2, MENU=3, FOCUS=4)" << endl;

            if (rec.EventType == MOUSE_EVENT) {
                MOUSE_EVENT_RECORD mouseEvent = rec.Event.MouseEvent;

                // Отладка: выводим состояние кнопки
                cout << "Кнопка: " << mouseEvent.dwButtonState << " (Левая=2, Правая=1)" << endl;

                // Проверяем левую кнопку мыши (щелчок)
                if ((mouseEvent.dwButtonState & FROM_LEFT_1ST_BUTTON_PRESSED) != 0) {
                    COORD pos = mouseEvent.dwMousePosition;

                    // Отладка: позиция клика
                    cout << "Клик в позиции: X=" << pos.X << ", Y=" << pos.Y << endl;

                    // Читаем символ по координатам (проверяем, что позиция в пределах буфера)
                    if (pos.X < csbi.dwSize.X && pos.Y < csbi.dwSize.Y) {
                        char ch;
                        DWORD charsRead;
                        if (ReadConsoleOutputCharacterA(hStdout, &ch, 1, pos, &charsRead)) {
                            // Если символ не пробел и не пустой
                            if (ch != ' ' && ch != '\0' && isalpha(ch)) {  // Добавили isalpha для букв
                                // Перемещаем курсор в начало последней строки (обновляем csbi)
                                GetConsoleScreenBufferInfo(hStdout, &csbi);  // Обновляем каждый раз
                                SHORT consoleHeight = csbi.srWindow.Bottom - csbi.srWindow.Top + 1;
                                COORD lastLinePos = { 0, csbi.srWindow.Bottom };  // Абсолютная позиция последней строки
                                SetConsoleCursorPosition(hStdout, lastLinePos);

                                // Очищаем строку от курсора до конца (примерно ширину окна)
                                DWORD charsWritten;
                                FillConsoleOutputCharacter(hStdout, ' ', csbi.dwSize.X, lastLinePos, &charsWritten);

                                // Возвращаем курсор в начало строки
                                SetConsoleCursorPosition(hStdout, lastLinePos);

                                // Выводим информацию
                                cout << "Буква: " << ch << ", Строка: " << (pos.Y + 1) << ", Столбец: " << (pos.X + 1) << endl;
                                cout.flush();
                            }
                            else {
                                cout << "Клик по не-букве (символ: '" << ch << "'), игнорируем." << endl;
                            }
                        }
                        else {
                            cerr << "Ошибка чтения символа из консоли" << endl;
                        }
                    }
                    else {
                        cout << "Клик вне буфера консоли." << endl;
                    }
                }
                // Правая кнопка мыши - завершение программы
                else if ((mouseEvent.dwButtonState & RIGHTMOST_BUTTON_PRESSED) != 0) {
                    cout << "Правая кнопка - завершение." << endl;
                    goto exit_loop;  // Выход из цикла
                }
            }
            // Игнорируем другие события (клавиатура и т.д.)
        }
    }

exit_loop:
    // Восстанавливаем исходный режим консоли
    SetConsoleMode(hStdin, mode & ~ENABLE_MOUSE_INPUT);  // Исправили: добавили hStdin и убрали несуществующий флаг

    return 0;
}