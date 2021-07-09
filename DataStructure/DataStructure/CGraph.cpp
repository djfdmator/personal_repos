#include"CGraph.h"
#include<iostream>
#include <queue>

using namespace std;
Graph::Graph()
{
	vertexCount = 0;
	for (int i = 0; i < MAXVERTEXSIZE; i++)
	{
		for (int j = 0; j < MAXVERTEXSIZE; j++)
		{
			vertex[i][j] = 0;
		}
	}
	ResetVisited();
}

Graph::~Graph()
{
}

bool Graph::IsEmpty()
{
	return false;
}

void Graph::InsertVertex()
{
	if (vertexCount >= MAXVERTEXSIZE)
	{
		return;
	}
	vertexCount++;
}

void Graph::InsertEdge(int u, int v)
{
	if (u >= vertexCount || v >= vertexCount)
	{
		return;
	}
	vertex[u][v] = 1;
}

void Graph::DeleteVertex()
{
	if (vertexCount <= 0)
	{
		vertexCount = 0;
		return;
	}

	for (int i = 0; i < vertexCount; i++)
	{
		vertex[vertexCount][i] = 0;
	}
	for (int i = 0; i < vertexCount; i++)
	{
		vertex[i][vertexCount] = 0;
	}
	vertexCount--;

}

void Graph::DeleteEdge(int u, int v)
{
	if (u >= vertexCount || v >= vertexCount)
	{
		return;
	}
	vertex[u][v] = 0;
}

void Graph::PrintGraph()
{
	if (vertexCount <= 0) return;

	for (int i = 0; i < vertexCount; i++)
	{
		for (int j = 0; j < vertexCount; j++)
		{
			cout << vertex[i][j] << " ";
		}
		cout << endl;
	}
}

void Graph::DFS(int s)
{
	if (s > vertexCount) return;
	visited[s] = true;
	for (int i = 0; i < vertexCount; i++)
	{
		if (i == s) continue;
		if (vertex[s][i] == 0) continue;

		if(!visited[i])
		{ 
			cout << "{" << s << "," << i << "}" << endl;
			DFS(i);
		}
	}
}

void Graph::BFS(int s)
{
	if (s > vertexCount) return;
	visited[s] = true;
	queue<int> q;
	q.push(s);
	cout << s << endl;
	while (!q.empty())
	{
		int a = q.front();
		q.pop();
		for (int i = 0; i < vertexCount; i++)
		{
			if (i == a) continue;
			if (vertex[a][i] == 0) continue;
			
			if (!visited[i])
			{
				cout << i << endl;
				q.push(i);
				visited[i] = true;
			}
		}
	}
}

void Graph::ResetVisited()
{
	for (int i = 0; i < MAXVERTEXSIZE; i++)
	{
		visited[i] = false;
	}
}
