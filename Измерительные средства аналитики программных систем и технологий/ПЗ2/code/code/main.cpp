#include <iostream>
#include <vector>
using namespace std;

int sumOfDigits(int x) {
    x = abs(x);
    int s = 0;
    while (x > 0) {
        s += x % 10;
        x /= 10;
    }
    return s;
}

int main() {
    int n;
    cout << "Enter amount: ";
    cin >> n;

    vector<int> arr(n);

    for (int i = 0; i < n; i++) {
        cout << "Enter element " << i + 1 << ": ";
        cin >> arr[i];
    }

    int positiveCount = 0;
    int negativeCount = 0;
    int evenCount = 0;
    int oddCount  = 0;

    for (int i = 0; i < n; i++) {
        int val = arr[i];

        if (val > 0) {
            positiveCount++;
        } else if (val < 0) {
            negativeCount++;
        }

        if (val % 2 == 0) {
            evenCount++;
        } else {
            oddCount++;
        }

        int s = sumOfDigits(val);
        cout << "The sum of digits of " << val << " = " << s << "\n";
    }

    cout << "Positive numbers: " << positiveCount << "\n";
    cout << "Negative numbers: " << negativeCount << "\n";
    cout << "Even numbers: " << evenCount << "\n";
    cout << "Odd numbers: " << oddCount << "\n";


    cout << "\nPress Enter to exit...";
    cin.ignore();
    cin.get();
    return 0;
}
