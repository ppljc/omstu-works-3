#include <iostream>
#include <iomanip>
#include <random>
#include <string>
using namespace std;

const int N = 7;

// Print main (true) or anti-diagonal (false)
void printDiagonal(const int matrix[N][N], bool isMain)
{
    for (int i = 0; i < N; ++i)
    {
        if (isMain)
            cout << setw(4) << matrix[i][i];
        else
            cout << setw(4) << matrix[i][N - 1 - i];
    }
    cout << "\n";
}

// Calculate sum of main (true) or anti-diagonal (false)
long long sumDiagonal(const int matrix[N][N], bool isMain)
{
    long long sum = 0;
    for (int i = 0; i < N; ++i)
    {
        sum += isMain ? matrix[i][i] : matrix[i][N - 1 - i];
    }
    return sum;
}

// Fill matrix with random integers in range [min, max]
void fillRandom(int matrix[N][N], int minVal = -50, int maxVal = 50)
{
    random_device rd;
    mt19937 gen(rd());
    uniform_int_distribution<> dis(minVal, maxVal);

    for (int i = 0; i < N; ++i)
        for (int j = 0; j < N; ++j)
            matrix[i][j] = dis(gen);
}

// Print full matrix
void printMatrix(const int matrix[N][N], const string& name)
{
    cout << "Matrix " << name << ":\n";
    for (int i = 0; i < N; ++i)
    {
        for (int j = 0; j < N; ++j)
            cout << setw(4) << matrix[i][j];
        cout << "\n";
    }
    cout << "\n";
}

// Process and display information for one matrix
void processMatrix(const int matrix[N][N], const string& name)
{
    cout << "Matrix " << name << ":\n";

    cout << "  Main diagonal:    ";
    printDiagonal(matrix, true);

    cout << "  Anti-diagonal:    ";
    printDiagonal(matrix, false);

    long long sumMain = sumDiagonal(matrix, true);
    long long sumAnti = sumDiagonal(matrix, false);

    cout << "  Sum of main diagonal:     " << sumMain << "\n";
    cout << "  Sum of anti-diagonal:     " << sumAnti << "\n\n";
}

int main()
{
    int A[N][N], B[N][N];

    // Seed is different each run thanks to random_device
    fillRandom(A);
    fillRandom(B);

    // Optional: print full matrices
    printMatrix(A, "A");
    printMatrix(B, "B");

    processMatrix(A, "A");
    processMatrix(B, "B");

    long long difference = sumDiagonal(A, true) - sumDiagonal(B, false);
    cout << "Difference (main diagonal of A - anti-diagonal of B) = " << difference << "\n";

    cin.get();
    return 0;
}
