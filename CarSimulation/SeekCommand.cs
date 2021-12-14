using System;

namespace CarSimulation
{
    internal abstract class SeekCommand : ICommand
    {

        protected readonly int DetectionRange;
        protected readonly int IntervalCounts;

        protected const float RayCastAngle = 0f;

        public abstract int OpCode { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detectionRange">Range in from..to format, where car can detect obstacles</param>
        /// <param name="intervalsCount">amount of intervals, which conditional jumps are based on</param>
        public SeekCommand(int detectionRange, int intervalsCount)
        {
            DetectionRange = detectionRange;
            IntervalCounts = intervalsCount;
        }

        public int Execute(Car thisAgent)
        {
            float Range = thisAgent.Raycast(thisAgent.Rotation + RayCastAngle, DetectionRange, thisAgent.Map);

            int skip = (int)MathF.Ceiling((float)Range / (DetectionRange / IntervalCounts));

            return skip == 0 ? 1 : skip;
        }
    }
}