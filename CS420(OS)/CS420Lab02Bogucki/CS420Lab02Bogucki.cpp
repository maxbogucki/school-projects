#include <iostream>
#include <fstream>
#include <string>
#include <thread>
#include <vector>
#include <algorithm>
#include <atomic>
#include <array>
using namespace std;

// Loads entire file into memory (heap) for faster threaded access
void fileToMemoryTransfer(char* fileName, char** data, size_t& numOfBytes) {
    streampos begin, end;
    ifstream inFile(fileName, ios::in | ios::binary | ios::ate);
    if (!inFile)
    {
        cerr << "Cannot open " << fileName << endl;
        inFile.close();
        exit(1);
    }

    size_t size = inFile.tellg();
    char* buffer = new char[size];
    inFile.seekg(0, ios::beg);
    inFile.read(buffer, size);
    inFile.close();

    *data = buffer;
    numOfBytes = size;
}

int main(int argc, char* argv[]) {

    // Make sure the executable is run with a filename argument
    if (argc != 2) {
        cerr << "Usage: " << argv[0] << " Filename" << endl;
        return 1;
    }

    // Initialize for fileToMemoryTransfer function
    char* data = nullptr;
    size_t numBytes = 0;

    // Create histogram for Part A (GLOBAL)
    // atomic<long long> allows ++ to be thread-safe without locks
    array<atomic<long long>, 256> histogram = {};

    fileToMemoryTransfer(argv[1], &data, numBytes);

    cout << "Run with one global histogram" << endl;

    // Number of threads based on CPU hardware
    size_t numThreads = thread::hardware_concurrency();

    vector<thread> workers;

    // Divide file size among threads
    size_t threadReadSize = numBytes / numThreads;
    size_t extra = numBytes % numThreads;
    size_t start = 0;
    size_t end = threadReadSize;

    // PART A - GLOBAL HISTOGRAM
    for (size_t t = 0; t < numThreads; t++) {
        // Last thread gets the extra bytes
        if (t == numThreads - 1) {
            end += extra;
        }

        // Each thread processes a portion of the file
        workers.push_back(thread([start, end, &data, &histogram]() {
            for (size_t i = start; i < end; i++) {
                // atomic++ ensures no data race
                histogram[static_cast<unsigned char>(data[i])]++;
            }
            }));

        start = end;
        end = start + threadReadSize;
    }

    // Wait for all worker threads to finish
    for_each(workers.begin(), workers.end(), [](thread& t) { t.join(); });

    // Print global histogram results
    for (int i = 0; i < 256; i++) {
        cout << i << ": " << histogram[i] << endl;
    }

    // PART B - LOCAL HISTOGRAMS

    // Create local histogram that will be merged into here
    array<long long, 256> histogram2 = {};

    // Reset ranges for second pass
    start = 0;
    end = threadReadSize;

    // Create a *contiguous* matrix to store local histograms for each thread
    // Format: threadMatrix[t * 256 + j]
    long long* threadMatrix = new long long[numThreads * 256]();

    vector<thread> flock;

    cout << "Run with local histograms" << endl;

    for (size_t t = 0; t < numThreads; t++) {

        if (t == numThreads - 1) {
            end += extra;
        }

        size_t row = t * 256; // start of this thread's histogram row

        // Each thread updates a local histogram row (no locks required)
        flock.emplace_back([start, end, &data, &threadMatrix, row]() {
            for (size_t i = start; i < end; i++) {
                unsigned char d = static_cast<unsigned char>(data[i]);
                // row + d indexes correctly into the "2D" matrix
                threadMatrix[row + d]++;
            }
            });

        start = end;
        end = start + threadReadSize;
    }

    // Wait for all threads to finish
    for_each(flock.begin(), flock.end(), [](thread& t) { t.join(); });

    // Merge local histograms into global output histogram
    for (size_t j = 0; j < numThreads; j++) {
        size_t row = j * 256;
        for (size_t i = 0; i < 256; i++) {
            histogram2[i] += threadMatrix[row + i];
        }
    }

    // Print final merged histogram
    for (size_t i = 0; i < 256; i++) {
        cout << i << ": " << histogram2[i] << endl;
    }

    // Free heap memory
    delete[] threadMatrix;
    delete[] data;

    return 0;
}
