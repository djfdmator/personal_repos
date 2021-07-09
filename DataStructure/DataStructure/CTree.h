#pragma once
#include<queue>

//Binary Search Tree
class Tree
{
public:
	enum ORDERTYPE
	{
		pre, in, post
	};

private:
	typedef struct Node
	{
		int data;
		Node* lLink;
		Node* rLink;
	};

	Node* RootNode;
	int size;

public:
	Tree();
	~Tree();
	bool IsEmpty() const;
	int Size() const;
	Node* SearchBST(int _data);
	void Insert(int _data);
	void Delete(int _data);
	int* Order(ORDERTYPE ot);

private:
	void Order(Node* RootNode, std::queue<int>* arr, ORDERTYPE ot);

};