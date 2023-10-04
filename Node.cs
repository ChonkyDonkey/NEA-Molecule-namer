using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molecule_Namer
{
    public class Node
    {
        public string Name;
        public List<Arc> Arcs = new List<Arc>();

        public Node(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Create a new arc, connecting this Node to the Nod passed in the parameter
        /// Also, it creates the inversed node in the passed node
        /// </summary>
        public Node AddArc(Node child, int w)
        {
            Arcs.Add(new Arc
            {
                Parent = this,
                Child = child,
                Weight = w
            });

            if (!child.Arcs.Exists(a => a.Parent == child && a.Child == this))
            {

                child.AddArc(this, w);
            }

            return this;
        }

    }
}
