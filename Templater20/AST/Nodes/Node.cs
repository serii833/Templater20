using System;
using System.Collections.Generic;

namespace Templater20.AST.Nodes
{
    public abstract class Node
    {
        protected Token token;
        protected List<Node> Children;

        protected Node(Token token)
        {
            this.token = token;
            Children = new List<Node>();
        }

        public void AddChild(Node childNode)
        {
            Children.Add(childNode);
        }

        public abstract string Render(ViewBag data);
    
        
        
        public static Node CreateNodeFromToken(Token token)
        {
            Node ret = null;
            switch (token.Type)
            {
                case TokenType.Text:
                    ret = new TextNode(token);
                    break;
                case TokenType.Variable:
                    ret = new VarNode(token);
                    break;
                case TokenType.BlockIfStart:
                    ret = new IfNode(token);
                    break;
                case TokenType.BlockForStart:
                    ret = new ForNode(token);
                    break;

            }

            if(ret == null)
                throw new Exception("Unsupported Node type.");
            return ret;
        }
    }
}
