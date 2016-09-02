using System.Collections.Generic;

namespace _0x001.Models.Keywords.Statement_Keywords.Selection_Statement.If_Statement
{
    class If_Clause : Base
    {
        public bool notElseIf;
        public List<Argument_Statement.If_Argument> Arguments;
        // false => or
        // true => and
        public List<bool> LogicGate;
        public List<Method_Handling.Method> Methods;
    }

}
