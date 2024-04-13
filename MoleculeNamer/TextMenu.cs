using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;

namespace MoleculeNamer
{
    public class TextMenu
    {
        public string final = "";
        private static string SFconversion(string First)
        {//objective Must 1a

            string final = "";
            for (int i = 0; i < First.Length; i++)
            {
                if (First[i] == 'C' || First[i] == '(' || First[i] == ')')
                {

                    final = final + First[i];





                }
            }
            return final;

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
            string CSF;
            SFInput();
            CSF = final;
            MoleculeProcesssor molProc = new();  //creating an event og molecule process

            Graph graph = molProc.ProcessMolecule(CSF);
            //graph.PrintMatrix();
            Console.WriteLine(graph.NameMolecule());
        }
    }
}