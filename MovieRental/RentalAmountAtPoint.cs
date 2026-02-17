using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace MovieRental;

//WIP: idea is to implement an abstract syntax tree to retreive the path used by the algorithm and establish which value has been used to generate the result.
//Then use this to create a CLI tool to use outputs and generate inputs (if possible)
public class RentalAmountAtPoint
{
    public static string GenerateAST(string code)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
        CompilationUnitSyntax root = syntaxTree.GetCompilationUnitRoot();

        List<string> astNodes = new List<string>();
        foreach (SyntaxNode node in root.DescendantNodes())
        {
            astNodes.Add(node.Kind().ToString());
        }

        return string.Join(", ", astNodes);
    }
}
