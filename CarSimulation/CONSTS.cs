using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CarSimulation
{
    class Consts
    {
        /// <summary>
        /// Path to store all colllected data
        /// </summary>
        public string CsvPath { get; set; }
        /// <summary>
        /// New generation will be created, when amount of Agents hit to this number
        /// </summary>
        public int NextGenerationSwitch { get; set; }
        /// <summary>
        /// Amout of Agents to spawn each generation
        /// </summary>
        public int PopulationSize { get; set; }
        /// <summary>
        /// When an Agent is punished for touching obstacles, its speed will be redused based on this percentage
        /// </summary>
        public float SlownessPercent { get; set; }
        /// <summary>
        /// Measured in ticks. Determines, how long should this Agent suffer for touching an obstacle
        /// </summary>
        public int PunishmentTime { get; set; }
        /// <summary>
        /// Amount of walls, placed when the simulation starts
        /// </summary>
        public int StartingObstaclesAmount { get; set; }
        /// <summary>
        /// Measured in ticks. Each time the <c>counter</c> is evenly divided by this number, new obstacles are generated
        /// </summary>
        public int ObstaclesGenerationInterval { get; set; }
        /// <summary>
        /// Chance of an agent to mutate during Genetic algorithm crossing
        /// </summary>
        public float MutationChance { get; set; }
        /// <summary>
        /// Determines how fast will screen move from left to right, destroing all Agents behind it
        /// </summary>
        public float DeathSideSpeed { get; set; }
        /// <summary>
        /// Determines how many generations will program do, befoure it closes. Set to <c>-1</c>, if you want to remove the limit entirely
        /// </summary>
        public int MaxGenerationAmount { get; set; }
        /// <summary>
        /// Determines weather or not should debug lines be drawn
        /// </summary>
        public bool AlwaysDrawDebugLines { get; set; }
        /// <summary>
        /// Highest command id. Exlusive
        /// </summary>
        public int HighestCommandID { get; set; }
        /// <summary>
        /// Amount of commands each agent will have
        /// </summary>
        public int CommandsCount { get; set; }
    }
}
