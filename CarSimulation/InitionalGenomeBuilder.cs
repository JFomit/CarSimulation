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
                commands[i] = OperationsCodes[i] switch
                {
                    MoveForwardCommand.opCode => new MoveForwardCommand(2f),
                    MoveBackwardCommand.opCode => new MoveBackwardCommand(2f),
                    RotateLeftCommand.opCode => new RotateLeftCommand(2f),
                    RotateRightCommand.opCode => new RotateRightCommand(2f),
                    SeekForwardCommand.opCode => new SeekForwardCommand(150, 2),
                    SeekBackwardCommand.opCode => new SeekBackwardCommand(150, 2),
                    SeekLeftCommand.opCode => new SeekLeftCommand(150, 2),
                    SeekRightCommand.opCode => new SeekRightCommand(150, 2),
                    _ => new UnconditioalJumpCommand(OperationsCodes[i]),
                };
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
