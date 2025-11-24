#include <iostream>
#include <string>
#include <cctype>

using namespace std;

int main() {
    string input;
    cout << "Enter a string (up to 100 characters): ";
    getline(cin, input);

    if (!input.empty() && input.back() == '\n') {
        input.pop_back();
    }

    int length = input.length();
    int vowels = 0;
    int freq[26] = {0};
    char most_common = '\0';
    int max_count = 0;

    string vowel_chars = "aeiouyAEIOUY";

    for (char c : input) {
        if (isalpha(c)) {
            if (vowel_chars.find(c) != string::npos) {
                vowels++;
            }

            char lower = tolower(c);
            int index = lower - 'a';
            freq[index]++;
            if (freq[index] > max_count) {
                max_count = freq[index];
                most_common = lower;
            }
        }
    }

    cout << "String length: " << length << '\n';
    cout << "Number of vowels: " << vowels << '\n';

    if (max_count > 0) {
        cout << "Most frequent letter: '" << most_common 
             << "' (appears " << max_count << " time";
        if (max_count > 1) cout << "s";
        cout << ")" << '\n';
    } else {
        cout << "Most frequent letter: (no letters)" << '\n';
    }

    cout << "\nPress Enter to exit...";
    cin.get();

    return 0;
}