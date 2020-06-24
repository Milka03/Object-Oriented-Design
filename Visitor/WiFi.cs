using System;
using Problems;

namespace Solvers
{
    class WiFi : NetworkDevice
    {
        private readonly double packetLossChance;
        private static readonly Random rng = new Random(1597);

        public WiFi(string model, int dataLimit, double packetLossChance) : base(model, dataLimit)
        {
            DeviceType = "WiFi";
            this.packetLossChance = packetLossChance;
        }

        public double PacketLossChance { get { return packetLossChance; } }
        public Random Rng { get { return rng; } }

        public override void Accept(Problem visitor)
        {
            visitor.VisitWiFi(this);
        }

    }
}