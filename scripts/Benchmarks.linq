<Query Kind="Program">
  <Reference Relative="..\src\Arborist.Trees\bin\Release\net7.0\Arborist.Core.dll">C:\Users\jason\source\repos\Arborist\src\Arborist.Trees\bin\Release\net7.0\Arborist.Core.dll</Reference>
  <Reference Relative="..\src\Arborist.Trees\bin\Release\net7.0\Arborist.dll">C:\Users\jason\source\repos\Arborist\src\Arborist.Trees\bin\Release\net7.0\Arborist.dll</Reference>
  <Reference Relative="..\src\Arborist.Trees\bin\Release\net7.0\Arborist.Linq.dll">C:\Users\jason\source\repos\Arborist\src\Arborist.Trees\bin\Release\net7.0\Arborist.Linq.dll</Reference>
  <Reference Relative="..\src\Arborist.Trees\bin\Release\net7.0\Arborist.Trees.dll">C:\Users\jason\source\repos\Arborist\src\Arborist.Trees\bin\Release\net7.0\Arborist.Trees.dll</Reference>
  <Namespace>Arborist.Core</Namespace>
  <Namespace>Arborist.Linq</Namespace>
  <Namespace>Arborist.Trees</Namespace>
</Query>

void Main()
{
	CountLeaves(20).Dump();
	
	//Enumerable.Range(2, 2 << 21).Select(x => new CollatzTreeNode((ulong)x)).Count().Dump();
}

// You can define other methods, fields, classes and namespaces here

ITreenumerable<ulong> tree = new CompleteBinaryTree();

int CountLeaves(int depth) => tree.PruneAfter(x => x.Position.Depth == depth).GetLeaves().Count();

int CountLeaves8() => CountLeaves(8);
int CountLeaves10() => CountLeaves(10);
int CountLeaves12() => CountLeaves(12);
int CountLeaves14() => CountLeaves(14);
int CountLeaves20() => CountLeaves(20);
int CountLeaves22() => CountLeaves(22);
