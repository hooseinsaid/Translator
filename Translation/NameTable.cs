using System;
using System.Collections.Generic;

namespace Translation
{
    public enum TCat
    {
        Const, Var, Type
    };

    public enum TType
    {
        None, Int, Bool
    };

    public struct Identifier
    {
        public string name;
        public TCat cat;
        public TType type;
    }

    public static class NameTable
    {
        public static LinkedList<Identifier> Identifiers = new LinkedList<Identifier>();

        public static Identifier Addidentifier(string name, TCat cat)
        {
            Identifier identifier = new Identifier
            {
                name = name,
                cat = cat
            };
            Identifiers.AddLast(identifier);
            return identifier;
        }

        public static Identifier SearchByName(string name)
        {
            LinkedListNode<Identifier> node = Identifiers.First;
            try
            {
                while (node != null & node.Value.name != name)
                {
                    node = node.Next;
                }
            }
            catch (Exception)
            {
            }
            return node == null ? new Identifier() : node.Value;
        }

        public static void Initialize()
        {
            Identifiers = new LinkedList<Identifier>();
        }
    }
}