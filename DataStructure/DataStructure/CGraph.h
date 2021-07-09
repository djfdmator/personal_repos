#pragma once
#include <queue>
#define MAXVERTEXSIZE 100

using namespace std;
class Graph
{
private:
	int vertex[MAXVERTEXSIZE][MAXVERTEXSIZE];
	int vertexCount;
	bool visited[MAXVERTEXSIZE];
public:
	Graph();
	~Graph();
	bool IsEmpty();
	void InsertVertex();
	void InsertEdge(int u, int v);
	void DeleteVertex();
	void DeleteEdge(int u, int v);
	void PrintGraph();

	void DFS(int s);
	void BFS(int s);
	void ResetVisited();
};