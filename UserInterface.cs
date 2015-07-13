using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            while(choice != 1 || choice != 2) {
                Console.WriteLine("Invalid choice.");
                choice = Console.Read();
            }
            if (choice == 1) {
                
            } else if (choice == 2) {

            }
        }
    }
}
