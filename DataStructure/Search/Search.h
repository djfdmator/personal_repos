#pragma once
#include<vector>
#define HASHTABLESIZE 100

using namespace std;

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

	struct Bucket
	{
		vector<int> list;
	};
	struct HashAddress
	{
		int bucketAddress;
		int slotAddress;
	};
	Bucket HashTable[HASHTABLESIZE];
	int Hash_ModMethod(int key, int tablesize = HASHTABLESIZE);
	void Hash_Insert(int key);
	HashAddress Hash_Search(int key);
};