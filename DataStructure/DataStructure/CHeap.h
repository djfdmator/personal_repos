#pragma once
#define MAXHEAPSIZE 100

template<class T>
class Heap
{
protected:
	T* tree;
	int heapSize;

	void IncreaseHeapSize() { heapSize++; }
public:
	Heap()
	{
		heapSize = 0;
		tree = new T[MAXHEAPSIZE];
		for (int i = 1; i < MAXHEAPSIZE; i++)
		{
			tree[i] = -1;
		}
	}


	bool IsEmpty() { return heapSize == 0 ? true : false; }

	int Size() { return heapSize; }

	virtual void Insert(T _data) {};

	virtual void Delete() {};

	virtual T GetRoot()
	{
		return tree[1];
	}
};
