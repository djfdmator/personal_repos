#include "CSort.h"
#include <limits>
#include <iostream>
#include <queue>
#include <cmath>
#include "MinHeap.h"
#include "CTree.h"

using namespace std;

void Sort::SelectionSort(int * arr, int arrsize)
{
	cout << endl << "Process :: SelectionSort" << endl;
	for (int i = 0; i < arrsize - 1; i++)
	{
		int min_Index = i;
		for (int j = i + 1; j < arrsize; j++)
		{
			if (arr[j] < arr[min_Index])
			{
				min_Index = j;
			}
		}

		int temp = arr[min_Index];
		arr[min_Index] = arr[i];
		arr[i] = temp;
	}
}

void Sort::BubbleSort(int * arr, int arrsize)
{
	cout << endl << "Process :: BubbleSort" << endl;
	for (int i = arrsize - 1; i > 0; i--)
	{
		for (int j = 0; j < i; j++)
		{
			if (arr[j] > arr[j + 1])
			{
				int temp = arr[j];
				arr[j] = arr[j + 1];
				arr[j + 1] = temp;
			}
		}
	}
}

void Sort::QuickSort(int * arr, int begin, int end)
{
	cout << endl << "Process :: QuickSort" << endl;
	if (begin >= end) return;
	int pivot = (begin + end) / 2;
	int L = begin;
	int R = end;

	while (L < R)
	{
		while (arr[L] < arr[pivot] && L < R) L++;
		while (arr[R] > arr[pivot] && L < R) R--;
		if (L < R)
		{
			int temp = arr[L];
			arr[L] = arr[R];
			arr[R] = temp;

			if (L == pivot)
				pivot = R;
		}
	}
	int temp = arr[pivot];
	arr[pivot] = arr[R];
	arr[R] = temp;

	QuickSort(arr, begin, R - 1);
	QuickSort(arr, R + 1, end);
}

void Sort::InsertSort(int * arr, int arrsize)
{
	cout << endl << "Process :: InsertSort" << endl;
	for (int i = 0; i < arrsize; i++)
	{
		int temp = arr[i];
		int j = i;
		while (j > 0 && arr[j - 1] > temp)
		{
			arr[j] = arr[j - 1];
			j--;
		}
		arr[j] = temp;
	}
}

void Sort::ShellSort(int * arr, int arrsize)
{
	cout << endl << "Process :: ShellSort" << endl;
	int interval = arrsize / 2;

	while (interval > 0)
	{
		for (int i = 0; i < interval; i++)
		{
			for (int j = i; j < arrsize; j += interval)
			{
				int temp = arr[j];
				int k = j;
				while (k - interval >= 0 && arr[k - interval] > temp)
				{
					arr[k] = arr[k - interval];
					k -= interval;
				}
				arr[k] = temp;
			}
		}

		interval /= 2;
	}
}

void Sort::MergeSort(int * arr, int begin, int end)
{
	cout << endl << "Process :: MergeSort" << endl;
	int middle = (begin + end) / 2;
	if (begin < end)
	{
		MergeSort(arr, middle + 1, end);
		MergeSort(arr, begin, middle);

		int* temp_arr = new int[end - begin + 1];
		int i = begin, j = middle + 1, k = 0;
		while (i <= middle && j <= end)
		{
			if (arr[i] > arr[j])
			{
				temp_arr[k] = arr[j];
				j++;
			}
			else
			{
				temp_arr[k] = arr[i];
				i++;
			}
			k++;
		}

		if (i <= middle)
		{
			for (; i <= middle; i++, k++)
				temp_arr[k] = arr[i];
		}
		else
		{
			for (; j <= end; j++, k++)
				temp_arr[k] = arr[j];
		}

		for (i = begin, k = 0; i <= end; i++, k++)
		{
			arr[i] = temp_arr[k];
		}

		delete[] temp_arr;
	}
}

void Sort::RadixSort(int * arr, int arrsize)
{
	int factor = 1;
	int digit = 0;
	int max = 0;
	for (int i = 0; i < arrsize; i++)
	{
		if (max < arr[i]) max = arr[i];
	}

	for (int i = 0; i < 9; i++)
	{
		if(0 == max / (int)pow(10, i)) break;
		digit++;
	}
	
	queue<int>* q = new queue<int>[10];
	for (int i = 0; i < digit; i++)
	{
		for (int j = 0; j < arrsize; j++)
		{
			q[(arr[j] / factor) % 10].push(arr[j]);
		}

		for (int j = 0, k = 0; j < 10; j++)
		{
			while (!q[j].empty())
			{
				arr[k] = q[j].front();
				q[j].pop();
				k++;
			}
		}

		factor *= 10;
	}
	delete[] q;
}

void Sort::HeapSort(int * arr, int arrsize)
{
	MinHeap<int> minH;
	int i;
	for (i = 0; i < arrsize; i++)
	{
		minH.Insert(arr[i]);
	}
	
	i = 0;
	while (!minH.IsEmpty())
	{
		arr[i] = minH.GetRoot();
		minH.Delete();
		i++;
	}
}

void Sort::TreeSort(int * arr, int arrsize)
{
	Tree T;

	for (int i = 0; i < arrsize; i++)
	{
		T.Insert(arr[i]);
	}

	int* temp_arr = T.Order(Tree::ORDERTYPE::in);
	for (int i = 0; i < arrsize; i++)
	{
		arr[i] = temp_arr[i];
	}
}

void Sort::Print(const int * arr, int arrsize)
{
	cout << endl << "-----------Print Array----------" << endl;
	for (int i = 0; i < arrsize; i++)
	{
		cout << "[" << arr[i] << "]" << endl;
	}
}
