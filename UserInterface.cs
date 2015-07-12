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

        private void printHeader() {
            Console.WriteLine("---Welcome to TextCompressor---");
        }

        private void programLoop() {

        }
    }
}
