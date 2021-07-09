#include "CList.h"

CList::CList()
{
	count = 0;
	mHead = new Node;
	mTail = new Node;

	mHead->value = 0;
	mHead->link = mTail;
	mTail->value = 0;
	mTail->link = nullptr;
}

CList::~CList()
{
	for (int i = 0; i < count; i++)
	{
		Pop();
	}
	delete mHead;
	delete mTail;
}

void CList::Push(int _value)
{
	count++;
	Node* NewNode = new Node;
	NewNode->value = _value;
	NewNode->link = mHead->link;
	mHead->link = NewNode;
}

void CList::Insert(int _index, int _value)
{
	if (_index >= count) return;
	
	count++;

	Node* node = mHead;
	for (int i = 0; i <= _index; i++)
	{
		node = node->link;
	}

	Node* NewNode = new Node;
	NewNode->value = _value;
	NewNode->link = node->link;
	node->link = NewNode;
}

void CList::Pop()
{
	if (count <= 0) return;
	count--;
	Node* node;

	node = mHead->link;
	mHead->link = mHead->link->link;

	delete node;
}

int CList::GetData(int _index)
{
	if (count <= 0 || count <= _index) return -1;

	Node* node = mHead;
	for (int i = 0; i <= _index; i++)
	{
		node = node->link;
	}

	return node->value;
}

int CList::Size()
{
	return count;
}
