using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class GenomeCreator
    {
        private readonly IBuilder Builder;
        public GenomeCreator(IBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public Genome CreateGenome()
        {
            return Builder.BuildCommandsList().Build();
        }

        public static ICommand GetCommandFromOpCode(int opCode, float moveSpeed, float rotationSpeed, int detectionRadius, int intervalsCount)
        {
            return opCode switch
            {
                MoveForwardCommand.opCode => new MoveForwardCommand(moveSpeed),
                MoveBackwardCommand.opCode => new MoveBackwardCommand(moveSpeed),
                RotateLeftCommand.opCode => new RotateLeftCommand(rotationSpeed),
                RotateRightCommand.opCode => new RotateRightCommand(rotationSpeed),
                SeekForwardCommand.opCode => new SeekForwardCommand(detectionRadius, intervalsCount),
                SeekBackwardCommand.opCode => new SeekBackwardCommand(detectionRadius, intervalsCount),
                SeekLeftCommand.opCode => new SeekLeftCommand(detectionRadius, intervalsCount),
                SeekRightCommand.opCode => new SeekRightCommand(detectionRadius, intervalsCount),
                SeekForwardRightCommand.opCode => new SeekForwardRightCommand(detectionRadius, intervalsCount),
                SeekRightBackwardCommand.opCode => new SeekRightBackwardCommand(detectionRadius, intervalsCount),
                SeekBackwardLeftCommand.opCode => new SeekBackwardLeftCommand(detectionRadius, intervalsCount),
                SeekLeftForwardCommand.opCode => new SeekLeftForwardCommand(detectionRadius, intervalsCount),
                _ => new UnconditioalJumpCommand(opCode),
            };
        }
    }
}
