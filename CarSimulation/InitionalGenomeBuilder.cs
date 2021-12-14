using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class InitionalGenomeBuilder : IBuilder
    {
        private readonly int[] OperationsCodes;
        private readonly ICommand[] commands;

        public InitionalGenomeBuilder(int[] operationsCodes)
        {
            OperationsCodes = operationsCodes;
            commands = new ICommand[OperationsCodes.Length];
        }

        public IBuilder BuildCommandsList()
        {
            for (int i = 0; i < OperationsCodes.Length; i++)
            {
                commands[i] = GenomeCreator.GetCommandFromOpCode(OperationsCodes[i], 2f, 2f, 150, 2);
            }
            return this;
        }

        public Genome Build()
        {
            Genome genome = new Genome(commands);
            return genome;
        }
    }
}
