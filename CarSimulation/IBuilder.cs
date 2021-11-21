using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    interface IBuilder
    {
        /// <summary>
        /// Builds commands list to be used in a final <c>Genome</c> class instance
        /// </summary>
        /// <returns></returns>
        public IBuilder BuildCommandsList();
        /// <summary>
        /// Builds a final <c>Genome</c> class instance
        /// </summary>
        /// <returns></returns>
        public Genome Build();
    }
}