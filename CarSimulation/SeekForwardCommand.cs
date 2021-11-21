using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class SeekForwardCommand : ICommand
    {
        public int OpCode { get => opCode; }
        public const int opCode = 4;

        private readonly int DetectionRange;
        private readonly int IntervalCounts;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detectionRange">Range in from..to format, where car can detect obstacles</param>
        /// <param name="intervalsCount">amount of intervals, which conditional jumps are based on</param>
        public SeekForwardCommand(int detectionRange, int intervalsCount)
        {
            DetectionRange = detectionRange;
            IntervalCounts = intervalsCount;
        }

        public int Execute(Agent thisAgent)
        {
            float Range = thisAgent.Raycast(thisAgent.Rotation, DetectionRange, thisAgent.Map);

            int skip = (int)MathF.Ceiling((float)Range / (DetectionRange / IntervalCounts));

            return skip == 0 ? 1 : skip;
        }
    }
}
