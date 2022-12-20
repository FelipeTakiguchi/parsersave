public class ParseTree
{
    public Token Token  { get; set; }
    public object? Value { get; set; }
    public List<ParseTree> NodeList = new List<ParseTree>();

    public float Calculate()
    {   
                Console.WriteLine($"token: {Token} valor: {Value}");
        switch (Token)
        {
            case Token.NUM:
                return float.Parse(Value.ToString());
            case Token.lowExp:
                if (NodeList[1].Token == Token.OPSUM)
                    return NodeList[0].Calculate() + NodeList[2].Calculate();
                else
                    return NodeList[0].Calculate() - NodeList[2].Calculate();
            case Token.medExp:
                if (NodeList[1].Token == Token.OPDIV)
                    return NodeList[0].Calculate() / NodeList[2].Calculate();
                else
                    return NodeList[0].Calculate() * NodeList[2].Calculate();
            case Token.highExp:
                return NodeList[1].Calculate();
            case Token.exp:
                return NodeList[0].Calculate();
            default:
                return 0;
        }    
    }
}

public enum Token
{
    NUM,
    OPSUM,
    OPSUB,
    OPMUL,
    OPDIV,
    OPENPARENTHESIS,
    CLOSEPARENTHESIS,

    exp,
    lowExp,
    medExp,
    highExp,
}

// NUM
// OPSUM
// OPSUB
// OPMUL
// OPDIV
// OPENPARENTHESIS
// CLOSEPARENTHESIS

// highExp = OPENPARENTHESIS exp CLOSEPARENTHESIS
// medExp = exp OPMUL exp | exp OPDIV exp
// lowExp = exp OPSUM exp | exp OPSUB exp
// exp = highExp | medExp | lowExp | NUM

// 4 * (1 + 2)
// NUM OPMUL OPENPARENTHESIS NUM OPSUM NUM CLOSEPARENTHESIS
// exp OPMUL OPENPARENTHESIS exp OPSUM exp CLOSEPARENTHESIS
// exp OPMUL OPENPARENTHESIS lowExp CLOSEPARENTHESIS
// exp OPMUL OPENPARENTHESIS exp CLOSEPARENTHESIS
// exp OPMUL highExp
// exp OPMUL exp
// medExp
// exp

// 1 + 2 * 3
// NUM OPSUM NUM OPMUL NUM
// exp OPSUM exp OPMUL exp
// exp OPSUM medExp
// exp OPSUM exp
// lowExp
// exp