#include "Search.h"

int Search::UnsortedSequentialSearch(int* arr, int arrsize, int findkey)
{
	int i = 0;
	while (i < arrsize && arr[i] != findkey)
	{
		i++;
	}

	if (i < arrsize) return i;
	else return -1;
}

int Search::SortedSequentialSearch(int * arr, int arrsize, int findkey)
{
	int i = 0;
	while (i < arrsize && arr[i] < findkey)
	{
		i++;
	}
	if (arr[i] == findkey) return i;
	else return -1;
}

Search::IndexTable * Search::MakeIndexTable(int * arr, int arrsize, int indexsize)
{
	IndexTable* result = new IndexTable[indexsize];
	int n = arrsize / indexsize;
	if (arrsize%indexsize > 0) n++;
	for (int i = 0; i < indexsize; i++)
	{
		result[i].Index = i * n;
		result[i].key = arr[i * n];
	}
	return result;
}

int Search::IndexSequentialSearch(int * arr, int arrsize, int findkey)
{
	if (arr[0] > findkey || arr[arrsize - 1] < findkey) return -1;

	int indexsize = 3;
	IndexTable * IT = MakeIndexTable(arr, arrsize, indexsize);
	
	int i, begin, end;
	for (i = 0; i < indexsize - 1; i++)
	{
		if (IT[i].key <= findkey && IT[i + 1].key > findkey)
		{
			begin = IT[i].Index;
			end = IT[i + 1].Index;
			break;
		}
	}

	if (i == indexsize - 1)
	{
		begin = IT[i].Index;
		end = arrsize;
	}

	while (begin < end && arr[begin] != findkey)
	{
		begin++;
	}
	
	if (begin < end) return arr[begin];
	else return -1;
}

int Search::BinarySearch(int * arr, int begin, int end, int findkey)
{
	int result = 0;
	int middle = (begin + end) / 2;
	if (arr[middle] == findkey) result = arr[middle];
	else if (arr[middle] > findkey) result = BinarySearch(arr, begin, middle - 1, findkey);
	else if (arr[middle] < findkey) result = BinarySearch(arr, middle + 1, end, findkey);
	else result = -1;
	return result;
}

int Search::Hash_ModMethod(int key, int tablesize)
{
	return key % tablesize;
}

void Search::Hash_Insert(int key)
{
	HashTable[Hash_ModMethod(key)].list.push_back(key);
}

Search::HashAddress Search::Hash_Search(int key)
{
	HashAddress result;
	int bucketAddress = Hash_ModMethod(key);

	int i = 0;
	while (i < HashTable[bucketAddress].list.size() && HashTable[bucketAddress].list[i] != key)
	{
		i++;
	}

	if (i < HashTable[bucketAddress].list.size())
	{
		result.bucketAddress = bucketAddress;
		result.slotAddress = i;
	}
	else
	{
		result.bucketAddress = -1;
		result.slotAddress = -1;
	}

	return result;
}
