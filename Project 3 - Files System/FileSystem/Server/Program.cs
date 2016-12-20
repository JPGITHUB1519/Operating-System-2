﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Server";
            CommunicationServer.setupServer();
            Console.ReadKey();
        }
    }
}
