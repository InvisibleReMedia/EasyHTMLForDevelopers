﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Library2.File f = new Library2.File("test");
            Marshalling.MarshallingHash hash = f.ExportToHash();
            Library2.File f2 = Marshalling.PersistentDataObject.Import<Library2.File>(hash);

            Console.WriteLine(f2.ToString());

            Console.ReadKey();
        }
    }
}
