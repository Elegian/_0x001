using System.Collections.Generic;

namespace _0x001.Models.Statement_Keywords.Selection_Statement.If_Statement
{
    struct If_Clause
    {
        public bool notElseIf;
        public List<Argument_Statement.If_Argument> Arguments;
        // false => or
        // true => and
        public List<bool> LogicGate;
        public List<Method_Handling.Method> Methods;
    }

}
