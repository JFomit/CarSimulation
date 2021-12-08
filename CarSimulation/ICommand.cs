using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    interface ICommand
    {
        /// <summary>
        /// Operation code of this command
        /// </summary>
        public int OpCode { get; }
        /// <summary>
        /// Execute this command
        /// </summary>
        /// <param name="thisAgent"><c>agent</c>, which command will be executed</param>
        /// <returns>Amount to increment commands pointer after execution</returns>
        public int Execute(Car thisAgent);
    }
}
