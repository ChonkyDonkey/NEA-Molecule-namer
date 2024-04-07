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

        readonly Graph _graph = new();
#pragma warning disable IDE0028 // Simplify collection initialization
        readonly Dictionary<string, Node> _molecule = new(); // this makes a dictionary of all the nodes
#pragma warning restore IDE0028 // Simplify collection initialization

#pragma warning disable IDE1006 // Naming Styles
        private bool ValidateString_Char(string CSF)
#pragma warning restore IDE1006 // Naming Styles
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
                    //objective must 2C
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
                return ValidateString_Brackets(CSF, bracketNo);
            }
            else
            {
                return false;
            }
        }


        private bool ValidateString_Brackets(string CSF, int bracketNo)
        {//objective must 2C
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
            return ValidateString_closeBracket(CSF);
        }
        private static bool ValidateString_closeBracket(string CSF)
        {
            bool valid = true;
            return valid;
        }

        public bool ValidateString(string CSF)
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
            bool valid = ValidateString_Char(CSF);
            return valid;
        }

        private void FindArc(string CSF, Dictionary<string, Node> molecule)
        {//objective must 2b

            int cValue = 0; // Index of C atom
            int noBrack = 0;

            for (int i = 0; i < CSF.Length; i++) // Scan each character of the array
            {
                char charValue = CSF[i];

                int trueCarbon = i - noBrack;


                if (charValue == 'C')
                {
                    FindArcLeft(i, CSF, molecule, trueCarbon);
                    FindArcRight(i, CSF, molecule, trueCarbon);
                    cValue++;
                }
                else if (charValue == '(' || charValue == ')')//objective must 2C
                {
                    // Do nothing in these cases, as we only take action from specific atoms
                    noBrack++;
                }

                _graph.PrintMatrix();//helper function
            }
        }

        static void FindArcLeft(int i, string CSF, Dictionary<string, Node> molecule, int trueCarbon)
        {

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

                ogNode.AddArc(connectingNode);
            }
            else if (charValue == ')')//objective must 2C
            {

                int cBracketCount = 1;
                int t = 1;
                int z;
                bool Valid = true;
                int no_Carbon = 0;
                while (Valid)// iterates the position in the code until the bracket is paired revealing the arc
                {
                    t++;
                    z = i - t;
                    Console.WriteLine("cbracketscount = " + cBracketCount);

                    testCharValue = CSF[z];

                    if (testCharValue == '(')
                    {
                        cBracketCount--;
                    }
                    else if (testCharValue == ')')
                    {
                        cBracketCount++;
                    }
                    else if (testCharValue == 'C') { no_Carbon++; }
                    if (cBracketCount == 0 && CSF[z] == 'C') { Valid = false; }
                }  // check for "))" as an ending

                string OGNodeName = "C" + trueCarbon;
                int Left = trueCarbon - no_Carbon;

                string connection = "C" + Left;

                Node ogNode = molecule[OGNodeName];
                Node connectingNode = molecule[connection];

                ogNode.AddArc(connectingNode);

            } // add code to include "C ( C"
            else if (charValue == '(')
            {
                int bracketNo = 0;
                int x = 2;
                int no_Carbon = 0;
                bool valid = true;

                while (valid)
                {
                    char conectionValue = CSF[i - x];

                    if (conectionValue == 'C') { no_Carbon++; }
                    else if (conectionValue == ')') { bracketNo++; }
                    else if (conectionValue == '(') { bracketNo--; }
                    x++;
                    if (conectionValue == 'C' && bracketNo == 0) { valid = false; }
                }

                string OGNodeName = "C" + trueCarbon;
                int left = trueCarbon - no_Carbon;

                string Connection = "C" + left;

                Node ogNode = molecule[OGNodeName];
                Node connectingNode = molecule[Connection];

                ogNode.AddArc(connectingNode, 1);
            }
        }

        static void FindArcRight(int i, string CSF, Dictionary<string, Node> molecule, int trueCarbon)
        {
            //Console.WriteLine("find arc right");
            if (i >= CSF.Length - 1) { return; }  // if last element in string then return early
            char charValue = CSF[i + 1];
            if (charValue == 'C')
            {
                string OGNodeName = "C" + trueCarbon;
                int right = trueCarbon + 1;

                string Connection = "C" + right;

                Node ogNode = molecule[OGNodeName];
                Node connectingNode = molecule[Connection];

                ogNode.AddArc(connectingNode, 1);

            }
            else if (charValue == ')') { } //objective must 2C
            else if (charValue == '(')
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
                    testCharValue = CSF[z];
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

                testCharValue = CSF[i + t + 1];
                totalBrackets--;
                string OGNodeName;
                string Connection;
                int right;
                if (testCharValue == 'C')
                {

                    OGNodeName = "C" + trueCarbon;
                    right = trueCarbon + t - totalBrackets - 1;
                    Console.WriteLine(right);

                    Connection = "C" + right;

                    Node OgNode = molecule[OGNodeName];
                    Node connectingNodes = molecule[Connection];

                    OgNode.AddArc(connectingNodes, 1);
                }

                OGNodeName = "C" + trueCarbon;
                right = trueCarbon + 1;

                Connection = "C" + right;

                Node ogNode = molecule[OGNodeName];
                Node connectingNode = molecule[Connection];

                ogNode.AddArc(connectingNode, 1);
            }
        }
        public Graph ProcessMolecule(string CSF)

        {   //objective must 2a
            // check that the string is valid, before continuing.
            if (!ValidateString(CSF))
            {
                // if the string is empty return the empty graph
                return _graph;
            }

            int count = CSF.Count(x => x == 'C');  // counts the number of 'C'
            string nodeName = "C0";
            _molecule.Add(nodeName, _graph.CreateRoot(nodeName));  // creates the first carbon as a root

            for (int i = 1; i < count; i++)  // creates the rest of the nodes to the count of carbons
            {

                nodeName = "C" + i;
                _molecule.Add(nodeName, _graph.CreateNode(nodeName));
            }
            FindArc(CSF, _molecule);
            return _graph;
        }

    }
}
