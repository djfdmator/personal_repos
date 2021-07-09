#pragma once


typedef struct Node
{
	int value;
	Node* link;
};

class CList
{
private:
	int count;
	Node* mHead;
	Node* mTail;

public:
	CList();
	~CList();
	void Push(int _value);
	void Insert(int _index, int _value);
	void Pop();
	int GetData(int _index);
	int Size();
};