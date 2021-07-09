#pragma once

template<class T>
class Deque
{
	typedef struct Node
	{
		T data;
		Node* rLink;
		Node* lLink;
	};
private:
	int size;
	Node* front;
	Node* back;
public:
	Deque() 
	{
		size = 0;
		front = nullptr;
		back = nullptr;
	}
	~Deque()
	{
		while (!IsEmpty())
		{
			Pop_Front();
		}
	}
	void Push_Front(T _data)
	{
		size++;
		Node* node = new Node;
		node->data = _data;
		if (front == nullptr)
		{
			front = node;
			back = node;
			node->rLink = nullptr;
			node->lLink = nullptr;
		}
		else
		{
			front->lLink = node;
			node->rLink = front;
			node->lLink = nullptr;
			front = node;
		}
	}
	void Push_Back(T _data)
	{
		size++;
		Node* node = new Node;
		node->data = _data;
		if (back == nullptr)
		{
			front = node;
			back = node;
			node->rLink = nullptr;
			node->lLink = nullptr;
		}
		else
		{
			back->rLink = node;
			node->lLink = back;
			node->rLink = nullptr;
			back = node;
		}
	}
	void Pop_Front()
	{
		if (size <= 0) return;
		size--;
		Node* node = front;
		front = front->rLink;
		if (front == nullptr)
		{
			back = nullptr;
		}
		else
		{
			front->lLink = nullptr;
		}

		delete node;
	}
	void Pop_Back()
	{
		if (size <= 0) return;
		size--;
		Node* node = back;
		back = back->lLink;
		if (back == nullptr)
		{
			front = nullptr;
		}
		else
		{
			back->rLink = nullptr;
		}
		delete node;
	}
	T Front()
	{
		return front->data;
	}
	T Back()
	{
		return back->data;

	}
	bool IsEmpty()
	{
		return size <= 0 ? true : false;
	}
	int Size() { return size; }
};