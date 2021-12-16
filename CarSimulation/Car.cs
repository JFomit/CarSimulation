using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class Car : Agent
    {
        /// <summary>
        /// flag that shows weather this car was punished
        /// </summary>
        public bool IsPunished { get; private set; }
        /// <summary>
        /// Determines how many turns must this car do, before it speeds will return to normal
        /// </summary>
        public int PunishmentTurns { get; private set; }
        /// <summary>
        /// punishments count
        /// </summary>
        public int MaxPunishmentsTurn { get; private set; }
        /// <summary>
        /// percentage applied to move and turn speed when punished
        /// </summary>
        public float PunismentSpeedModifier { get; private set; }
        public Genome Genome { get; private set; }

        public Car(Texture2D texture, Vector2 position, float rotation, Sprite[] map, Genome genome, float modifier, int maxPunishmentTime) : base(texture, position, rotation, map)
        {
            Genome = genome;
            MaxPunishmentsTurn = maxPunishmentTime;
            IsPunished = false;
            PunismentSpeedModifier = modifier;
        }
        /// <summary>
        /// Makes this Car do its turn
        /// </summary>
        public void Think()
        {
            Genome.Next(this);
            if (IsPunished)
            {
                PunishmentTurns--;
                if (PunishmentTurns <= 0)
                {
                    IsPunished = false;
                }
            }
        }

        public void Punish()
        {
            IsPunished = true;
            PunishmentTurns = MaxPunishmentsTurn;
        }
    }
}
