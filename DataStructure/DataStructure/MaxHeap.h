#pragma once
#include"CHeap.h"

//테스트 안함 - minheap 복사해서 부호만 바꿈
template<class T>
class MaxHeap : public Heap<T>
{
public:
	MaxHeap() {}

	void Insert(T _data) override
	{
		if (this->heapSize == MAXHEAPSIZE - 1) return;

		this->heapSize++;
		int index = this->heapSize;
		while (index != 1 && _data > this->tree[index / 2])
		{
			this->tree[index] = this->tree[index / 2];
			index /= 2;
		}
		this->tree[index] = _data;
	}

	void Delete() override
	{
		if (this->IsEmpty()) return;
		int index = this->heapSize;
		this->tree[1] = this->tree[index];
		this->heapSize--;
		this->tree[index] = -1;

		index = 1;
		T data = this->tree[1];
		while (index * 2 <= this->heapSize)
		{
			int child = index * 2;
			if (this->tree[child + 1] != -1 && (this->tree[child] < this->tree[child + 1])) child++;

			if (this->tree[child] < data) break;

			this->tree[index] = this->tree[child];
			index = child;
		}
		this->tree[index] = data;
	}

};