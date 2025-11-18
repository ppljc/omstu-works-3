#include <iostream>
#include <chrono>
#include <random>
#include <string>
#include <unistd.h>
#include <pthread.h>

using namespace std;
using namespace std::chrono;

const string ALPHABET = "ABCDEFGHIJKLMNOPQRST";
const string RED    = "\033[31m";
const string GREEN  = "\033[32m";
const string CYAN   = "\033[36m";
const string YELLOW = "\033[33m";
const string BOLD   = "\033[1m";
const string RESET  = "\033[0m";

const int COL1 = 15, COL2 = 48, COL3 = 81; // расположение столбцов

// переход в координату
void gotoxy(int y, int x) {
    cout << "\033[" << y << ";" << x << "H";
}

// очищение строки
void clear_line(int row) {
    gotoxy(row, 1);
    cout << "\033[2K";
}

// вывод текста
void print(int row, int col, const string& text, const string& color = "") {
    gotoxy(row, col);
    cout << color << text << RESET << flush;
}

// функция первого потока
void* thread1(void*) {
    pthread_setcancelstate(PTHREAD_CANCEL_DISABLE, nullptr);
    clear_line(23); print(23, 0, "Нить 1: отмена запрещена (на всё время работы)", RED);

    random_device rd; mt19937 gen(rd());
    uniform_int_distribution<int> d(100, 900);

    for (int k = 1; k <= 20; ++k) {
        usleep(1000 * d(gen));
        char letter = ALPHABET[k-1];
        print(k + 1, COL1, string(2, letter), RED);  // 2 раза
    }
    clear_line(23); print(23, 0, "Нить 1: завершена нормально (неотменяемая)", RED);

    return nullptr;
}

// функция второго потока
void* thread2(void*) {
    clear_line(24); print(24, 0, "Нить 2: отмена разрешена по умолчанию", GREEN);

    random_device rd; mt19937 gen(rd());
    uniform_int_distribution<int> d(120, 950);

    for (int k = 1; k <= 20; ++k) {
        usleep(1000 * d(gen));
        char letter = ALPHABET[k-1];
        print(k + 1, COL2, string(4, letter), GREEN);  // 4 раза
    }
    clear_line(24); print(24, 0, "Нить 2: завершена нормально", GREEN);

    return nullptr;
}

// функция третьего потока
void* thread3(void*) {
    pthread_setcancelstate(PTHREAD_CANCEL_DISABLE, nullptr);
    clear_line(25); print(25, 0, "Нить 3: отмена запрещена в начале работы", CYAN);

    random_device rd; mt19937 gen(rd());
    uniform_int_distribution<int> d(100, 880);

    for (int k = 1; k <= 20; ++k) {
        usleep(1000 * d(gen));
        char letter = ALPHABET[k-1];
        print(k + 1, COL3, string(6, letter), CYAN);

        if (k == 13) {
            pthread_setcanceltype(PTHREAD_CANCEL_DEFERRED, nullptr);
            pthread_setcancelstate(PTHREAD_CANCEL_ENABLE, nullptr);
            clear_line(25); print(25, 0, "Нить 3: на 13-м шаге разрешила отмену (отложенный режим)", CYAN);
        }

        if (k == 16) {
            clear_line(25); print(25, 0, "Нить 3: точка отмены между 16 и 17 шагом - pthread_testcancel()", CYAN);
            pthread_testcancel();
        }
    }

    return nullptr;
}

int main() {
    system("clear");
    print(1, COL1, "Нить 1", RED);
    print(1, COL2, "Нить 2", GREEN);
    print(1, COL3, "Нить 3", CYAN);

    pthread_t tid1, tid2, tid3;
    
    pthread_create(&tid1, nullptr, thread1, nullptr);
    pthread_create(&tid2, nullptr, thread2, nullptr);
    pthread_create(&tid3, nullptr, thread3, nullptr);

    random_device rd; mt19937 gen(rd());
    uniform_int_distribution<int> main_d(400, 1100);

    for (int step = 1; step <= 20; ++step) {
        usleep(1000 * main_d(gen));

        if (step == 6) {
            pthread_cancel(tid1);;
            clear_line(26); print(26, 0, "Главная нить: 6-й шаг - пытается отменить Нить 1 - ОТКАЗАНО", YELLOW);
        }
        if (step == 11) {
            pthread_cancel(tid3);
            clear_line(26); print(26, 0, "Главная нить: 11-й шаг - пытается отменить Нить 3 - запрос принят", YELLOW);
        }
    }

    pthread_join(tid1, nullptr);
    pthread_join(tid2, nullptr);
    pthread_join(tid3, nullptr);

    print(28, 0, "Нажмите Enter для выхода...", BOLD);
    cin.get();

    cout << RESET;
    return 0;
}
