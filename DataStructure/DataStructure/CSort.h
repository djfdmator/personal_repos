#pragma once

class Sort
{
	
public:
	void SelectionSort(int* arr, int arrsize);
	void BubbleSort(int* arr, int arrsize);
	void QuickSort(int* arr, int begin, int end);
	void InsertSort(int* arr, int arrsize);
	void ShellSort(int* arr, int arrsize);
	void MergeSort(int* arr, int begin, int end);
	void RadixSort(int* arr, int arrsize);
	void HeapSort(int* arr, int arrsize);
	void TreeSort(int* arr, int arrsize);

	void Print(const int* arr, int arrsize);
};