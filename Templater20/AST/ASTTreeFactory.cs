using System.Collections.Generic;
using Templater20.AST.Nodes;

namespace Templater20.AST
{
    public class ASTTreeFactory
    {
        private List<Token> tokens;
        private int currentTokenIndex;


        public ASTree BuildFromTokens(List<Token> tokens)
        {
            this.tokens = tokens;
            this.currentTokenIndex = 0;

            var ast = (ASTree)Process(new ASTree(null));

            return ast;
        }

        private Token GetNextToken()
        {
            if (currentTokenIndex == tokens.Count)
                return null;

            var tok = tokens[currentTokenIndex];

            currentTokenIndex++;

            return tok;
        }

        private Node Process(Node curNode)
        {
            Token tok;
            while( (tok = GetNextToken()) != null)
            {
                if (tok.Type == TokenType.BlockIfEnd || tok.Type == TokenType.BlockForEnd)
                    return curNode;


                var node = Node.CreateNodeFromToken(tok);

                if (tok.Type == TokenType.BlockIfStart || tok.Type == TokenType.BlockForStart)
                    Process(node);
                

                curNode.AddChild(node);
            }
            return curNode;
        }
    }
}
