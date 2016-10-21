namespace Templater20
{
    public enum TokenType
    {
        Text, Variable, BlockIfStart, BlockIfEnd,
        BlockForStart,
        BlockForEnd
    }


    public class TokensBoundaries
    {
        public static string VAR_TOKEN_START = "{{";
        public static string VAR_TOKEN_END = "}}";
        public static string BLOCK_TOKEN_START = "{%";
        public static string BLOCK_TOKEN_END = "%}";
    }

    public class Token
    {
        private string raw;

        public string Text;
        public TokenType Type;

        public Token(string raw)
        {
            this.raw = raw;
            Process();
        }

        public void Process()
        {
            var t = raw;
            if (t.StartsWith(TokensBoundaries.VAR_TOKEN_START))
            {
                Text = t.Replace(TokensBoundaries.VAR_TOKEN_START, "").Replace(TokensBoundaries.VAR_TOKEN_END, "").Trim();
                Type = TokenType.Variable;
            }
            else if (t.StartsWith(TokensBoundaries.BLOCK_TOKEN_START))
            {
                Text = t.Replace(TokensBoundaries.BLOCK_TOKEN_START, "").Replace(TokensBoundaries.BLOCK_TOKEN_END, "").Trim();

                if (Text.StartsWith("if"))
                    Type = TokenType.BlockIfStart;
                if (Text == "endif")
                    Type = TokenType.BlockIfEnd;

                if (Text.StartsWith("for"))
                    Type = TokenType.BlockForStart;
                if (Text == "endfor")
                    Type = TokenType.BlockForEnd;
            }
            else
            {
                Text = t;
                Type = TokenType.Text;
            }


        }
    }
}
