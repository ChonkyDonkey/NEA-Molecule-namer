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
            string Final = "";
            startMenu(Final);
            //string SF = "CH3CH2CH3";
            string CSF = Final;
            MoleculeProcesssor molProc = new MoleculeProcesssor();//creating an event og molecule process

            Graph graph = molProc.processMolecule(CSF);
            graph.PrintMatrix();
            Console.ReadLine();
        }
        static void startMenu(string Final)
        {
            bool valid;
            do
            {
                valid = true;   
                Console.WriteLine("what is your choice? \n type '1' to input a custom structural formula: \n type '2' to list all molecules stored: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("1");
                        valid = false;
                        SFInput(Final);
                        Console.WriteLine("CHICK1");
                        valid = false;
                        break;
                    case "2":
                        Console.WriteLine("2");
                        break;
                    default:
                        Console.WriteLine("na ah");
                        break;
                    

                }

            }
            while (valid == true);
        }
        private static string SFInput(string Final)
        {
            bool valid;
            Final = "";
            string First;
            do
            {
                valid = true;
                Console.WriteLine("what is your structural formula? ");
                First = Console.ReadLine();//gets an input for the Structural formula
                
                SFconversion(First, Final, valid);
                valid = false;


            } while
            (valid == true);
            

            
        }
        static void SFconversion(string First, string Final, bool valid) {
            
            
            Console.WriteLine("test");
            for (int i = 0; i < First.Length; i++){
                if (First[i] == 'C' || First[i] == '('|| First[i] == ')')
                {
                    
                    Final = Final + First[i];
                    Console.WriteLine(Final);
                    

                    //char[] nameArray = {'A', 'l', 'i', 'c', 'e'};
                    //string name = new string(nameArray);
                    
                }
            }
            Console.WriteLine("done");
            ValidCheck(Final, valid);
            Console.WriteLine("chick2");
            
             
        }
        
        static void ValidCheck(string Final, bool valid) { 
            valid = false;
            Console.WriteLine("chick3");
        }



    }
}
    
