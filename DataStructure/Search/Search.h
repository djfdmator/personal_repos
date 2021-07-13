#pragma once


class Search
{
public:
	int UnsortedSequentialSearch(int* arr, int arrsize, int findkey);
	int SortedSequentialSearch(int* arr, int arrsize, int findkey);

	struct IndexTable
	{
		int Index;
		int key;
	};
	IndexTable* MakeIndexTable(int* arr, int arrsize, int indexsize);
	int IndexSequentialSearch(int* arr, int arrsize, int findkey);

	int BinarySearch(int* arr, int begin, int end, int findkey);
};