#include "CTree.h"

Tree::Tree()
{
	RootNode = nullptr;
	size = 0;
}

Tree::~Tree()
{

}

bool Tree::IsEmpty() const
{
	return RootNode == nullptr ? true : false;
}

int Tree::Size() const
{
	return size;
}

Tree::Node * Tree::SearchBST(int _data)
{
	Node* node = RootNode;
	while (node != nullptr)
	{
		if (node->data == _data)
		{
			return node;
		}
		else if(node->data > _data)
		{
			node = node->lLink;
		}
		else
		{
			node = node->rLink;
		}
	}
	return nullptr;
}

void Tree::Insert(int _data)
{
	size++;
	if (RootNode == nullptr)
	{
		RootNode = new Node;
		RootNode->data = _data;
		RootNode->lLink = nullptr;
		RootNode->rLink = nullptr;
	}
	else
	{
		Node* cur = RootNode;
		while (cur->data != _data)
		{
			if (cur->data < _data)
			{
				if (cur->rLink != nullptr)
				{
					cur = cur->rLink;
				}
				else
				{
					cur->rLink = new Node;
					cur = cur->rLink;
					break;
				}
			}
			else
			{
				if (cur->lLink != nullptr)
				{
					cur = cur->lLink;
				}
				else
				{
					cur->lLink = new Node;
					cur = cur->lLink;
					break;
				}
			}
		}

		cur->data = _data;
		cur->lLink = nullptr;
		cur->rLink = nullptr;
	}
}

void Tree::Delete(int _data)
{
	Node* node = SearchBST(_data);
	if (node == nullptr) return;

	Node* parent = nullptr;
	while (node->data != _data)
	{
		parent = node;
		if (node->data < _data) node = node->rLink;
		else node = node->lLink;
	}

	size--;
	Node* lChild = node->lLink;
	Node* rChild = node->rLink;
	if (lChild != nullptr && rChild != nullptr)
	{
		Node* heir = node->lLink;
		while (heir->rLink != nullptr)
		{
			parent = heir;
			heir = heir->rLink;
		}
		node->data = heir->data;
		parent->rLink = heir->lLink;

		delete heir;
		return;
	}
	else if (lChild != nullptr || rChild != nullptr)
	{
		if (lChild != nullptr)
		{
			if (parent->lLink == node) parent->lLink = node->lLink;
			else parent->rLink = node->lLink;
		}
		else
		{
			if (parent->lLink == node) parent->lLink = node->rLink;
			else parent->rLink = node->rLink;
		}
	}
	else
	{
		if (parent->lLink == node) parent->lLink = nullptr;
		else parent->rLink = nullptr;
	}

	delete node;
}

int * Tree::Order(ORDERTYPE ot)
{
	int* result = new int[size];
	std::queue<int> q;
	Order(RootNode, &q, ot);
	int i = 0;
	while (!q.empty())
	{
		result[i] = q.front();
		q.pop();
		i++;
	}

	return result;
}

void Tree::Order(Node* RootNode, std::queue<int>* arr, ORDERTYPE ot)
{
	if (RootNode)
	{
		switch (ot)
		{
		case Tree::pre:
			(*arr).push(RootNode->data);
			Order(RootNode->lLink, arr, ot);
			Order(RootNode->rLink, arr, ot);
			break;
		case Tree::in:
			Order(RootNode->lLink, arr, ot);
			(*arr).push(RootNode->data);
			Order(RootNode->rLink, arr, ot);
			break;
		case Tree::post:
			Order(RootNode->lLink, arr, ot);
			Order(RootNode->rLink, arr, ot);
			(*arr).push(RootNode->data);
			break;
		}
	}
}
