using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoleculeNamer
{
    public class MoleculeProcesssor
    {
         
        Graph graph = new Graph();
        Dictionary<string, Node> molecule = new Dictionary<string, Node>(); // this makes a dictionary of all the nodes

        private bool validateString_Char(string CSF)
        {
            int bracketNo = 0;
            int CNo = 0; //carbon number
            if (CSF != null)
            { //checks for null
                for (int i = 0; i <= CSF.Length - 1; i++)
                {
                    if (CSF[i] != 'C' && CSF[i] != '(' && CSF[i] != ')')
                    {// if the string contain anything but these characters then it is an invalid string
                        Trace.WriteLine("validateString_Char: Rejecting due to " + CSF[i] + " not being C, ( or )");
                        return false;
                    }
                    else if (CSF[i] == '(' || CSF[i] == ')') { bracketNo++; }//gets the number of brackets
                    else if (CSF[i] == 'C') { CNo++; }
                }
                if (CNo == 0)
                {
                    //makes sure that Carbon in the string
                    Trace.WriteLine("validateString_Char: No 'C's found in " + CSF);
                    return false;
                }
                if (CSF[0] != 'C')
                {
                    //makes sure first char is a carbon
                    Trace.WriteLine("validateString_Char: First character must be Carbon " + CSF);
                    return false;
                }
                if (CSF[CSF.Length - 1] != 'C') { return false; }//string must end in 'C'
                return validateString_Brackets(CSF, bracketNo);
            }
            else
            {
                return false;
            }


        }
        private bool validateString_Brackets(string CSF, int bracketNo)
        {
            int bracketTracker = 0;
            if (bracketNo > 0)
            {//checks if brackets exist in the string
                if (bracketNo % 2 == 1)//even number of brackets
                {//makes sure there is no odd numberes brackets
                    Trace.WriteLine("validateString_Brackets: Odd numbers of brackets found in " + CSF);
                    return false;
                }
                for (int i = 0; i <= CSF.Length - 1; i++)
                {//checks all the brackets pairs up
                    if (CSF[i] == '(')
                    {
                        bracketTracker++;
                        if (CSF[i + 1] != 'C') { return false; }//makes sure open brackets are always followed by a 'C'
                    }
                    else if (CSF[i] == ')') { bracketTracker--; }

                    if (bracketTracker < 0)//makes sure a closing bracket can only be inputted if its pairopen bracket comes before. 
                    {
                        //verifies '('is the first bracket and that there is never a ')'before its '(' pair
                        Trace.WriteLine("validateString_Brackets: ('is the first bracket and that there is never a ')'before its '(' pair " + CSF);
                        return false;
                    }

                }
                if (bracketTracker != 0)//makes sure there is the same number of '(' as ')'
                {
                    Trace.WriteLine("validateString_Brackets: Differing number of ( and )'s: " + CSF);
                    return false;
                }
            }
            return validateString_closeBracket(CSF);
        }
        private bool validateString_closeBracket(string CSF)
        {
            bool valid = true;
            return valid;
        }

        public bool validateString(string CSF)
        {
            /*
            verify the string consists of 'C' and '()' (this)                                        done
            verify the string has the correct number of closed brackets to open brackets             done   
            verify the first barscket if there is one is open or nothing                             done
            make sure an open bracket is always followed by a 'C'                                    done
            strings must end with a 'C'                                                              done
            make sure you dont have more than 2 '()' following a 'C' unless it is the start node, then it can be 3
            make sure input != Null                                                                  done
            */
            bool valid = true;
            valid = validateString_Char(CSF);



            return valid;
        }
        private void findArc(string CSF, Dictionary<string, Node> molecule)
        {
            // for (int i = 0; i <= count-1; i++)
            int cValue = 0; // Index of C atom
            int noBrack = 0;
            Console.WriteLine(cValue); // test
            for (int i = 0; i < CSF.Length; i++) // Scan each character of the array
            {
                char charValue = CSF[i];
                Console.WriteLine((char)charValue);
                int trueCarbon = i - noBrack;
                Console.WriteLine("FindArc: i=" + i + ", charValue=" + charValue);
                //considering CSF[i]
                if (charValue == 'C')
                {
                    findArcLeft(i, CSF, molecule, trueCarbon);
                    findArcRight(i, CSF, molecule, trueCarbon);
                    cValue++;
                }
                else if (charValue == '(' || charValue == ')')
                {
                    // Do nothing in these cases, as we only take action from specific atoms
                    noBrack++;
                }

                //graph.PrintMatrix();
            }
        }

        static void findArcLeft(int i, string CSF, Dictionary<string, Node> molecule, int trueCarbon)
        {
            //Console.WriteLine("i is this" + i);
            if (i == 0) { return; } // return early if this is the left most early

            char charValue = CSF[i - 1];
            char testCharValue;
            if (charValue == 'C')
            {
                string OGNodeName = "C" + trueCarbon;
                int left = trueCarbon - 1;

                string Connection = "C" + left;

                Node ogNode = molecule[OGNodeName];
                Node connectingNode = molecule[Connection];// the node that the code has identified as an arc

                ogNode.AddArc(connectingNode, 1);
            }
            else if (charValue == ')')
            {
                int cBracketCount = 1;
                int t = 1;
                int z;
                while (cBracketCount > 0)// iterates the position in the code until the bracket is paired revealing the arc
                {
                    t++;
                    z = i - t;
                    Console.WriteLine("cbracketscount = " + cBracketCount);
                    //Console.WriteLine(t + " " + i);
                    testCharValue = CSF[z];
                    //Console.WriteLine(charValue);
                    if (testCharValue == '(')
                    {
                        cBracketCount--;
                    }
                    else if (testCharValue == ')')
                    {
                        cBracketCount++;
                    }
                }  // check for "))" as an ending

                testCharValue = CSF[i - t];
                if (testCharValue == 'C')
                {
                    string OGNodeName = "C" + trueCarbon;
                    int Left = trueCarbon - t;

                    string Connection = "C" + Left;

                    Node ogNode = molecule[OGNodeName];
                    Node connectingNode = molecule[Connection];

                    ogNode.AddArc(connectingNode, 1);
                }


            } // add code to include "C ( C"
            else if (charValue == '(')
            {

                string OGNodeName = "C" + trueCarbon;
                int Left = trueCarbon - 1;

                string Connection = "C" + Left;

                Node ogNode = molecule[OGNodeName];
                Node connectingNode = molecule[Connection];

                ogNode.AddArc(connectingNode, 1);


            }
        }
        static void findArcRight(int i, string CSF, Dictionary<string, Node> molecule, int trueCarbon)
        {
            //Console.WriteLine("find arc right");
            if (i >= CSF.Length - 1) { return; }  // if last element in string then return early
            char charValue = CSF[i + 1];
            if (charValue == 'C')
            {
                string OGNodeName = "C" + trueCarbon;
                int Right = trueCarbon + 1;

                string Connection = "C" + Right;

                Node ogNode = molecule[OGNodeName];
                Node connectingNode = molecule[Connection];

                ogNode.AddArc(connectingNode, 1);
                //Console.WriteLine("right");
            }
            else if (charValue == ')') { }
            else
            {
                /*counts the number of open brackets and subtracts the number of closed
                 letting you know when that first bracket set ends and where the 
                next arc connection is*/
                int oBracketCount = 1;
                int t = 1;
                int totalBrackets = 0;
                char testCharValue;

                while (oBracketCount > 0)
                {
                    t++;
                    int z = i + t;
                    //Console.WriteLine("obracketscount = " + oBracketCount);
                    //Console.WriteLine(t + " " + i); 
                    testCharValue = CSF[z];
                    //Console.WriteLine(charValue);
                    if (testCharValue == '(')
                    {
                        oBracketCount++;
                        totalBrackets++;
                    }
                    else if (testCharValue == ')')
                    {
                        oBracketCount--;
                        totalBrackets++;
                    }
                }// check for "))" as an ending
                //Console.WriteLine("ping");
                testCharValue = CSF[i + t + 1];
                totalBrackets--;
                string OGNodeName;
                string Connection;
                int Right;
                if (testCharValue == 'C')
                {

                    OGNodeName = "C" + trueCarbon;
                    Right = trueCarbon + t - totalBrackets - 1;
                    Console.WriteLine(Right);

                    Connection = "C" + Right;

                    Node OgNode = molecule[OGNodeName];
                    Node connectingNodes = molecule[Connection];

                    OgNode.AddArc(connectingNodes, 1);
                }
                Console.WriteLine("brackets right");
                OGNodeName = "C" + trueCarbon;
                Right = trueCarbon + 1;

                Connection = "C" + Right;

                Node ogNode = molecule[OGNodeName];
                Node connectingNode = molecule[Connection];

                ogNode.AddArc(connectingNode, 1);
            }
        }
        public Graph processMolecule(string CSF)

        {
            // check that the string is valid, before continuing.
            if (!validateString(CSF))
            {
                // if the string is empty return the empty graph
                return graph;
            }

            int count = CSF.Count(x => x == 'C');  // counts the number of 'C'
            //Console.WriteLine(count);

            string nodeName = "C0";
            molecule.Add(nodeName, graph.CreateRoot(nodeName));  // creates the first carbon as a root

            for (int i = 1; i < count; i++)  // creates the rest of the nodes to the count of carbons
            {
                Console.WriteLine(i);
                nodeName = "C" + i;
                molecule.Add(nodeName, graph.CreateNode(nodeName));
            }
            findArc(CSF, molecule);
            return graph;
        }

    }
}
