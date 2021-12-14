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
        private readonly Random rnd;
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
            commands = new ICommand[oldGenome.Commands.Length];
            oldGenome.Commands.CopyTo(commands, 0);
            this.rnd = rnd;
        }
        public IBuilder BuildCommandsList()
        {
            if (rnd.NextDouble() <= MutationChance)
            {
                for (int i = 0; i < rnd.Next(1, 6); i++)
                {
                    int newCommand = rnd.Next(0, HighestCommandID);

                    commands[rnd.Next(0, commands.Length)] = GenomeCreator.GetCommandFromOpCode(newCommand, 2f, 2f, 150, 2);
                }
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
