﻿using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace MoleculeNamer
{
    public class Program
    {

        static void Main(string[] args)
        {
            TextMenu menu = new();
            menu.startMenu();
        }
    }
}

