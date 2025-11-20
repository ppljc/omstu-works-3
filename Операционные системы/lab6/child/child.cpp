#include <windows.h>
#include <string>
#include <iostream>
#include <vector>
#include <thread>
#include <chrono>
#include <random>

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

// Создание процесса
bool spawn(const string& exe, const vector<string>& args, PROCESS_INFORMATION& pi)
{
    string cmd = exe;
    for (const auto& a : args)
    {
        cmd += " ";
        if (a.find(' ') != string::npos) cmd += "\"" + a + "\"";
        else cmd += a;
    }

    STARTUPINFOA si{};
    si.cb = sizeof(si);
    si.dwFlags = STARTF_USESTDHANDLES;
    si.hStdOutput = GetStdHandle(STD_OUTPUT_HANDLE);
    si.hStdError  = GetStdHandle(STD_ERROR_HANDLE);
    si.hStdInput  = GetStdHandle(STD_INPUT_HANDLE);
    ZeroMemory(&pi, sizeof(pi));

    vector<char> buf(cmd.begin(), cmd.end());
    buf.push_back(0);

    BOOL ok = CreateProcessA(
        NULL,
        buf.data(),
        NULL,NULL,
        TRUE,
        0,
        NULL,NULL,
        &si, &pi
    );
    return ok != FALSE;
}

int main(int argc, char** argv)
{
    string myName = argv[1];
    string grandName = argv[2];

    print_sync("[" + myName + "] child started\n");

    // Создаём внука
    PROCESS_INFORMATION pg{};
    if (!spawn("grandchild.exe", {grandName, myName}, pg))
    {
        print_sync("[" + myName + "] cannot spawn grandchild\n");
    }

    // Цикл ребенка
    for (int step = 1; step <= 20; step++)
    {
        print_sync("[" + myName + "] child step " + to_string(step) + "\n");
        ms_sleep(rint(900, 1500));
    }

    WaitForSingleObject(pg.hProcess, 1500);
    CloseHandle(pg.hProcess);
    CloseHandle(pg.hThread);

    print_sync("[" + myName + "] child exit\n");
    return 0;
}
