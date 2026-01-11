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
bool spawn(const string& exe, const vector<string>& args, PROCESS_INFORMATION& pi, DWORD flags)
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
        flags,
        NULL,NULL,
        &si, &pi
    );
    return ok != FALSE;
}

int main()
{
    const string childExe = "child.exe";

    HANDLE job1 = CreateJobObjectA(NULL,NULL);
    HANDLE job2 = CreateJobObjectA(NULL,NULL);

    if (!job1 || !job2)
    {
        print_sync("CreateJobObject failed\n");
        return 1;
    }

    // Имена дочерних и внучатых
    string c1 = "Alice";
    string g1 = "AliceJr";

    string c2 = "Bob";
    string g2 = "BobJr";

    PROCESS_INFORMATION p1{}, p2{};

    // Создаем первого ребенка
    if (!spawn(childExe, {c1, g1}, p1, CREATE_SUSPENDED | 0))
    {
        print_sync("child1 spawn failed\n");
        return 1;
    }
    AssignProcessToJobObject(job1, p1.hProcess);
    ResumeThread(p1.hThread);

    // Создаем второго ребенка
    if (!spawn(childExe, {c2, g2}, p2, CREATE_SUSPENDED | 0))
    {
        print_sync("child2 spawn failed\n");
        TerminateJobObject(job1, 0);
        return 1;
    }
    AssignProcessToJobObject(job2, p2.hProcess);
    ResumeThread(p2.hThread);

    // Цикл родителя
    for (int step = 1; step <= 15; step++)
    {
        print_sync("parent step " + to_string(step) + "\n");

        if (step == 7)
        {
            print_sync("parent: kill branch 1\n");
            TerminateJobObject(job1, 0);
        }

        if (step == 11)
        {
            print_sync("parent: kill branch 2\n");
            TerminateJobObject(job2, 0);
        }

        ms_sleep(rint(1000, 2000));
    }

    WaitForSingleObject(p1.hProcess, 1500);
    WaitForSingleObject(p2.hProcess, 1500);

    CloseHandle(p1.hProcess);
    CloseHandle(p1.hThread);
    CloseHandle(p2.hProcess);
    CloseHandle(p2.hThread);
    CloseHandle(job1);
    CloseHandle(job2);

    cout << "parent exit\n";

    cout << "\nPress Enter to exit...\n";
    cin.get();
    return 0;
}
