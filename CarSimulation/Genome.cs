using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class Genome
    {
        /// <summary>
        /// Commands list
        /// </summary>
        public ICommand[] Commands { get; private set; }
        /// <summary>
        /// Pointer to a next command to execute
        /// </summary>
        public int Pointer { get; private set; }

        public Genome(ICommand[] commands)
        {
            Pointer = 0;
            Commands = commands;
        }
        /// <summary>
        /// Decodes and executes next command in the <c>Commands</c> list
        /// </summary>
        /// <param name="thisAgent">genome holder</param>
        public void Next(Car thisAgent)
        {
            int inc = Commands[Pointer].Execute(thisAgent);

            while ((Pointer + inc) > (Commands.Length - 1))
            {
                inc -= Commands.Length;
            }
            Pointer += inc;
        }
    }
}
