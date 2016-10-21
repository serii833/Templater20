using System.Collections.Generic;
using System.Text.RegularExpressions;
using Templater20.AST;

namespace Templater20
{
    public class Engine
    {
        public string Render(string template, ViewBag data)
        {
            var ret = "";

            var tokens = Tokenize(template);


            var astFactory = new ASTTreeFactory();
            var ast = astFactory.BuildFromTokens(tokens);

            ret = ast.Render(data);


            return ret;
        }



        private static List<Token> Tokenize(string template)
        {
            var tokens = new List<Token>();

            var parts = Regex.Split(template, "({{.*?}}|{%.*?%})");

            foreach (var p in parts)
            {
                var token = new Token(p);
                tokens.Add(token);
            }

            return tokens;
        }
    }
}
