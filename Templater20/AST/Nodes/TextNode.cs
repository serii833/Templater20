namespace Templater20.AST.Nodes
{
    public class TextNode:Node
    {
        public TextNode(Token token) : base(token)
        {
        }

        public override string Render(ViewBag data)
        {
            return token.Text;
        }
    }
}
