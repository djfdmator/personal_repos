#pragma once

template<class T>
class Queue
{
	typedef struct Node
	{
		T data;
		Node* link;
	};
private:
	int size;
	Node* front;
	Node* back;
public:
	Queue()
	{
		size = 0;
		front = nullptr;
		back = nullptr;
	}
	~Queue()
	{
		while (!IsEmpty())
		{
			Pop();
		}
	}
	void Push(T _data)
	{
		size++;
		Node* node = new Node;
		node->data = _data;
		node->link = nullptr;
		if (front == nullptr)
		{
			front = node;
			back = node;
		}
		else
		{
			back->link = node;
			back = node;
		}
	}
	void Pop()
	{
		if (size <= 0) return;
		size--;
		Node* node = front;
		front = front->link;
		if (front == nullptr) back = nullptr;
		delete node;
	}
	T Top()
	{
		return front->data;
	}
	bool IsEmpty()
	{
		return front == nullptr ? true : false;
	}
	int Size() { return size; }
};