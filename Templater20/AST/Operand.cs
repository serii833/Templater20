using System;
using System.Collections.Generic;
using System.Text;

namespace Templater20.AST
{

    public enum OperandType
    {
        Decimal, String, Variable
    }


    public class Operand
    {
        private string _raw;
        public string CleanText;
        public OperandType Type;

        public Object Value;


        public Operand(string raw, ViewBag data)
        {
            _raw = raw.Trim();

            if (CanParseToDecimal())
            {
                Type = OperandType.Decimal;
                Value = _raw;
            }
            else if (CanParseToString())
            {
                Type = OperandType.String;
                Value = _raw;
            }
            else
            {
                Type = OperandType.Variable;
                Value = ViewBag.GetValue(_raw, data).ToString();
            }

            CleanText = _raw.Replace("\"", "");
        }

        private bool CanParseToDecimal()
        {
            decimal res;
            return Decimal.TryParse(_raw, out res);
        }

        private bool CanParseToString()
        {
            if (_raw.StartsWith("\"") && _raw.EndsWith("\""))
                return true;

            return false;
        }
    }
}
