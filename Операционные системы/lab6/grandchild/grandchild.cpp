#include <string>
#include <iostream>
#include <thread>
#include <chrono>
#include <random>
#include <windows.h>

using namespace std;

// Безопасный вывод
void print_sync(const string& s)
{
    HANDLE hCon = GetStdHandle(STD_OUTPUT_HANDLE);
    if (hCon == INVALID_HANDLE_VALUE) {
        cout << s;
        cout.flush();
        return;
    }

    // Блокируем консольный буфер на время записи
    DWORD written;
    WriteConsoleA(hCon, s.c_str(), (DWORD)s.length(), &written, NULL);
}

static mt19937 rng((unsigned)chrono::high_resolution_clock::now().time_since_epoch().count());

// Рандомная задержка в диапозоне
int rint(int a, int b) { return uniform_int_distribution<int>(a, b)(rng); }

// Пауза процесса
void ms_sleep(int ms) { this_thread::sleep_for(chrono::milliseconds(ms)); }

int main(int argc, char** argv)
{
    string myName = argv[1];
    string parentName = argv[2];

    print_sync("[" + myName + " of " + parentName + "] grandchild start\n");

    // Цикл внука
    for (int step = 1; step <= 20; step++)
    {
        print_sync("[" + myName + " of " + parentName + "] step " + to_string(step) + "\n");
        ms_sleep(rint(700, 1600));
    }

    print_sync("[" + myName + "] grandchild exit\n");
    return 0;
}
