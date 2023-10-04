using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Molecule_Namer
{
    public class Program
    {

        static void Main(string[] args)
        {
            startMenu();
        }
        static void startMenu()
        {
            bool valid = true;
            do
            {
                string CSF;
                Console.WriteLine("what is your choice? \n type '1' to input a custom structural formula: \n type '2' to list all molecules stored: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CSF = SFInput();
                        MoleculeProcesssor molProc = new MoleculeProcesssor();  //creating an event og molecule process

                        Graph graph = molProc.processMolecule(CSF);
                        graph.PrintMatrix();

                        valid = false;
                        break;
                    case "2":
                        Console.WriteLine("Got 2");
                        break;
                    default:
                        Console.WriteLine("na ah");
                        break;
                }

            } while (valid);
        }
        private static string SFInput()
        {
            bool valid = true;
            string final;
            string? first;
            do
            {
                Console.WriteLine("what is your structural formula? ");

                first = Console.ReadLine();  //gets an input for the Structural formula
                final = SFconversion(first);
                valid = false;

            } while (valid);

            return final;
        }

        static string SFconversion(string First)
        {
            Console.WriteLine("test");
            string final = "";
            for (int i = 0; i < First.Length; i++)
            {
                if (First[i] == 'C' || First[i] == '(' || First[i] == ')')
                {

                    final = final + First[i];
                    Console.WriteLine(final);


                    //char[] nameArray = {'A', 'l', 'i', 'c', 'e'};
                    //string name = new string(nameArray);

                }
            }
            return final;
            //Console.WriteLine("chick2");
        }
    }
}

