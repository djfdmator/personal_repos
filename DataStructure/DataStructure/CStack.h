#include <iostream>
#pragma once

template<class T>
class Stack
{
	typedef struct Node
	{
		T data;
		Node* link;
	};

private:
	Node* top;
	int size;
public:
	Stack()
	{
		top = nullptr;
		size = 0;
	}
	~Stack()
	{
		while (top != nullptr)
		{
			Pop();
		}
	}
	void Push(T data)
	{
		size++;
		Node* node = new Node;
		node->data = data;
		node->link = top;
		top = node;
	}
	void Pop()
	{
		if (size == 0) return;

		size--;
		Node* node = top;
		top = top->link;

		delete node;
	}
	T Top()
	{
		return top->data;
	}
	int Size()
	{
		return size;
	}
	void Print()
	{
		Node* node = top;
		std::cout << "--------stack--------" << std::endl;
		while (node != nullptr)
		{
			std::cout << node->data << std::endl;
			node = node->link;
		}
		std::cout << "---------------------" << std::endl;
	}
};
