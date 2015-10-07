using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Text;

namespace LittleBASIC
{
    public static class Interactive
    {
        private static List<string> programs = new List<string>();

        public static void Enter()
        {
            while (true)
            {
                Console.Write(Directory.GetCurrentDirectory().Substring(Directory.GetCurrentDirectory().LastIndexOf("\\") + 1) + "> ");
                string[] parts = Console.ReadLine().Split(' ');

                switch (parts[0].ToUpper())
                {
                    case "QUIT":
                    case "BYE":
                        Console.WriteLine("GOODBYE!");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    case "LOAD":
                        if (!File.Exists(parts[1]))
                        {
                            Console.WriteLine("ERROR FILE DOES NOT EXIST!");
                            break;
                        }
                        programs.Add(parts[1]);
                        break;
                    case "UNLOAD":
                        if (!programs.Contains(parts[1]))
                        {
                            Console.WriteLine("PROGRAM DOES NOT EXIST OR IS NOT LOADED!");
                            break;
                        }
                        programs.Remove(parts[1]);
                        Console.WriteLine("PROGRAM UNLOADED!");
                        break;
                    case "LIST":
                    case "CAT":
                    case "CATALOG":
                        if (programs.Count <= 0)
                        {
                            Console.WriteLine("NO PROGRAMS LOADED. USE LOAD <PATH> TO LOAD!");
                            break;
                        }

                        for (int x = 0; x < programs.Count; x++)
                            Console.WriteLine(x + ". " + programs[x].ToUpper());
                        break;
                    case "RUN":
                        if (programs.Count <= 0)
                        {
                            Console.WriteLine("NO PROGRAMS LOADED. USE LOAD <PATH> TO LOAD!");
                            break;
                        }

                        foreach (string program in programs)
                            new Interpreter.Interpreter(new Parser.Parser(new Lexer.Lexer(File.ReadAllText(program)).Tokenize()).Parse()).Interpret();
                        break;
                    case "CD":
                        if (!Directory.Exists(parts[1]))
                        {
                            Console.WriteLine("DIRECTORY DOES NOT EXIST!");
                            break;
                        }
                        Directory.SetCurrentDirectory(parts[1]);
                        Console.WriteLine("DIRECTORY CHANGED!");
                        break;
                    case "EXP":
                        if (parts.Length < 1)
                        {
                            Console.WriteLine("NOT ENOUGH ARGUMENTS!");
                            break;
                        }

                        switch (parts[1].ToUpper())
                        {
                            case "LOAD":
                                Console.WriteLine("LOADS A PROGRAM.");
                                break;
                            case "UNLOAD":
                                Console.WriteLine("UNLOADS A PROGRAM.");
                                break;
                            case "RUN":
                                Console.WriteLine("EXECUTES ALL LOADED PROGRAMS.");
                                break;
                            case "CAT":
                            case "CATALOG":
                                Console.WriteLine("LISTS ALL LOADED PROGRAMS.");
                                break;
                            case "QUIT":
                            case "BYE":
                                Console.WriteLine("CLOSES LITTLEBASIC.");
                                break;
                            case "CD":
                                Console.WriteLine("CHANGES CURRENT WORKING DIRECTORY.");
                                break;
                            case "EXP":
                            case "ALL":
                                Console.WriteLine("QUIT  LOAD  UNLOAD  RUN\nCAT  CATALOG  QUIT  BYE\nCD  EXP");
                                break;
                            default:
                                Console.WriteLine("UNKNOWN COMMAND. USE EXP EXP TO SHOW ALL");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("UNKNOWN COMMAND! USE EXP FOR HELP.");
                        break;
                }
            }
        }
    }
}
