<Query Kind="Program">
  <Reference Relative="..\src\Copse.Trees\bin\Release\net7.0\Copse.Core.dll">C:\Users\jason\source\repos\Copse\src\Copse.Trees\bin\Release\net7.0\Copse.Core.dll</Reference>
  <Reference Relative="..\src\Copse.Trees\bin\Release\net7.0\Copse.dll">C:\Users\jason\source\repos\Copse\src\Copse.Trees\bin\Release\net7.0\Copse.dll</Reference>
  <Reference Relative="..\src\Copse.Trees\bin\Release\net7.0\Copse.Linq.dll">C:\Users\jason\source\repos\Copse\src\Copse.Trees\bin\Release\net7.0\Copse.Linq.dll</Reference>
  <Reference Relative="..\src\Copse.Trees\bin\Release\net7.0\Copse.Trees.dll">C:\Users\jason\source\repos\Copse\src\Copse.Trees\bin\Release\net7.0\Copse.Trees.dll</Reference>
  <Namespace>Copse.Core</Namespace>
  <Namespace>Copse.Linq</Namespace>
  <Namespace>Copse.Trees</Namespace>
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
