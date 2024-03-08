using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;

namespace MoleculeNamer
{
    public class TextMenu
    {
        public string final = "";
        private static string SFconversion(string First)
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
        private void SFInput()
        {
            bool valid = true;

            string? first;
            do
            {
                Console.WriteLine("what is your structural formula? ");

                first = Console.ReadLine();//gets an input for the Structural formula
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (first.Length > 0)
                {
                    final = SFconversion(first);
                    valid = false;
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            } while (valid);


        }


        public void startMenu()
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
                        SFInput();
                        CSF = final;
                        MoleculeProcesssor molProc = new MoleculeProcesssor();  //creating an event og molecule process

                        Graph graph = molProc.processMolecule(CSF);
                        graph.PrintMatrix();
                        List<int> longest = new List<int>(graph.findLongest());
                        graph.nameMolecule(longest);


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
    }
}