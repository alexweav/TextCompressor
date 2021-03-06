﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PriorityQueue;

namespace TextCompressor {

    //Text-based user interface for TextCompressor which works in-console
    //Responsible for obtaining information from and interacting with the user
    class UserInterface {

        public void init() {
            printHeader();
            programLoop();
        }

        //Writes the header which shows on program start
        private void printHeader() {
            Console.WriteLine("---Welcome to TextCompressor---");
        }

        //Main menu
        private void programLoop() {
            Console.WriteLine("Would you like to compress text or read binary data?");
            Console.WriteLine("1. Compress text");
            Console.WriteLine("2. Read binary data");
            int choice = Console.Read();
            while(choice != 49 && choice != 50) {
                Console.WriteLine("Invalid choice.");
                choice = Console.Read();
            }
            if (choice == 49) {
                TextFile text = getTextFile();
                EncodedFile enf = text.encodeFile();
            } else if (choice == 50) {
                EncodedFile encoded = getEncodedFile();
                encoded.decodeFile();
                
            }
        }

        //Obtains a text file from the user
        private TextFile getTextFile() {
            Console.WriteLine("Please enter the location of the text file you want to compress.");
            string fp = Console.ReadLine();
            TextFile file;
            while(true) {
                try {
                    file = new TextFile(fp);
                } catch(ArgumentException) {
                    Console.WriteLine("Invalid filepath.");
                    Console.WriteLine("Please enter a new filepath.");
                    fp = Console.ReadLine();
                    continue;
                }
                break;
            }
            return file;
            //return new TextFile("E:\\Users\\Alexander Weaver\\My Documents\\ayy lmao.txt");    //testing purposes
        }

        //gets an encoded file from the user
        private EncodedFile getEncodedFile() {
            Console.WriteLine("Please enter the location of the encoded file you want to view.");
            string fp = Console.ReadLine();
            EncodedFile file;
            while (true) {
                try {
                    file = new EncodedFile(fp, EncodedFile.USE_EXISTING);
                }
                catch (ArgumentException) {
                    Console.WriteLine("Invalid filepath.");
                    Console.WriteLine("Please enter a new filepath.");
                    fp = Console.ReadLine();
                    continue;
                }
                break;
            }
            return file;
            //return new EncodedFile("E:\\Users\\Alexander Weaver\\My Documents\\encodedTEST.hct", EncodedFile.USE_EXISTING);    //testing purposes
        }
    }
}
