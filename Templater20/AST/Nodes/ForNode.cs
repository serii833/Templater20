using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace Templater20.AST.Nodes
{
    class ForNode:Node
    {
        public ForNode(Token token) : base(token)
        {
        }

        public override string Render(ViewBag data)
        {
            var parts = token.Text.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

            var itVarName = parts[1];
            var collectionVarName = parts[3];

            var collection = data[collectionVarName] as IEnumerable;

            var ret = "";

            foreach (var i in collection)
            {
                data[itVarName] = i;
                foreach (var child in Children)
                {   
                    ret += child.Render(data);
                }
            }

            return ret;
        }
    }
}
