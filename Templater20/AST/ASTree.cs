using System.Collections.Generic;
using Templater20.AST.Nodes;

namespace Templater20.AST
{
    public class ASTree: Node
    {
        public ASTree(Token token) : base(token)
        {
        }
        

        public override string Render(ViewBag data)
        {
            string ret = "";

            foreach (var child in Children)
            {
                ret += child.Render(data);
            }

            return ret;
        }
    }
}
