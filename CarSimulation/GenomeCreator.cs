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
    }
}
