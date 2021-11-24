using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class GeneticAlgorithGenomeBuilder : IBuilder
    {
        private readonly ICommand[] commands;
        private readonly float MutationChance;
        private readonly int HighestCommandID;
        private Random rnd;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldGenome">old genome</param>
        /// <param name="mutationChance">chance of the mutation for this new genome (<c>0f</c> to <c>1f</c>)</param>
        /// <param name="highestCommandID">highest command id. Exclusive</param>
        public GeneticAlgorithGenomeBuilder(Genome oldGenome, float mutationChance, int highestCommandID, Random rnd)
        {
            MutationChance = mutationChance;
            HighestCommandID = highestCommandID;
            commands = oldGenome.Commands;
            this.rnd = rnd;
        }
        public IBuilder BuildCommandsList()
        {
            if (rnd.NextDouble() >= MutationChance)
            {
                int newCommand = rnd.Next(0, HighestCommandID);

                commands[rnd.Next(0, commands.Length)] = newCommand switch
                {
                    MoveForwardCommand.opCode => new MoveForwardCommand(2f),
                    MoveBackwardCommand.opCode => new MoveBackwardCommand(2f),
                    RotateLeftCommand.opCode => new RotateLeftCommand(2f),
                    RotateRightCommand.opCode => new RotateRightCommand(2f),
                    SeekForwardCommand.opCode => new SeekForwardCommand(150, 2),
                    SeekBackwardCommand.opCode => new SeekBackwardCommand(150, 2),
                    SeekLeftCommand.opCode => new SeekLeftCommand(150, 2),
                    SeekRightCommand.opCode => new SeekRightCommand(150, 2),
                    _ => new UnconditioalJumpCommand(newCommand),
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
