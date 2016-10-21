using System.Reflection;

namespace Templater20.AST.Nodes
{
    public class VarNode : Node
    {
        public VarNode(Token token) : base(token)
        {
        }

        public override string Render(ViewBag data)
        {
            var varName = token.Text;

            var val = "";

            if (!varName.Contains("."))
                val = data[varName].ToString();
            else
                val = ViewBag.GetValue(varName, data).ToString();

            return val;
        }

        
    }
}
