using System;
using System.Text.RegularExpressions;

namespace Templater20.AST.Nodes
{
    public class IfNode : Node
    {
        public IfNode(Token token) : base(token)
        {
        }

        public override string Render(ViewBag data)
        {
            var text = token.Text.Replace("if", "").Trim();

            var parts = Regex.Split(text, "(=)");

            var a = new Operand(parts[0], data);
            var op = parts[1];
            var b = new Operand(parts[2], data);

            var baseType = OperandType.Decimal;
            if (a.Type == OperandType.String || b.Type == OperandType.String)
                baseType = OperandType.String;


            var processIf = true;
            if (baseType == OperandType.Decimal)
            {
                if (Convert.ToDecimal(a.Value) != Convert.ToDecimal(b.Value))
                    processIf = false;
            }
            if (baseType == OperandType.String)
            {
                if (a.Value.ToString() != b.Value.ToString())
                    processIf = false;
            }

            var ret = "";

            if (processIf)
            {
                foreach (var child in Children)
                    ret += child.Render(data);
            }

            ret = ret.Trim();
            return ret;
        }
    }




}
