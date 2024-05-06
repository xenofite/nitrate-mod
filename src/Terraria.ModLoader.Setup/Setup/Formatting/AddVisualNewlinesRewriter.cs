﻿using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Terraria.ModLoader.Setup.Formatting;

internal sealed class AddVisualNewlinesRewriter : CSharpSyntaxRewriter
{
	private readonly HashSet<SyntaxNode> modifyNodes = [];
	
	public override SyntaxNode VisitBlock(BlockSyntax node)
	{
		var stmts = node.Statements;
		for (var i = 0; i < stmts.Count - 1; i++)
		{
			var prev = stmts[i];
			var next = stmts[i + 1];
			if (!prev.SpansSingleLine() && !next.GetLeadingTrivia().FirstOrDefault().IsKind(SyntaxKind.EndOfLineTrivia))
			{
				modifyNodes.Add(next);
			}
		}
		
		return base.VisitBlock(node);
	}
	
	public override SyntaxNode Visit(SyntaxNode node)
	{
		if (modifyNodes.Remove(node))
		{
			node = node?.WithLeadingTrivia(node.GetLeadingTrivia().Insert(0, SyntaxFactory.EndOfLine(Environment.NewLine)));
		}
		
		return base.Visit(node);
	}
}