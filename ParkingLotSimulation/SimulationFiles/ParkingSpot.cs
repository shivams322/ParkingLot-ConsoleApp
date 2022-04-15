using System.Collections.Generic;

namespace ParkingLotSimulation.SimulationFiles
{
    internal class ParkingSpot
    {
        public int spotNumber;

        public Status availabilityStatus;

        public VehicleSize vehicleSize;

        public ParkingSpot(int spotNumber, VehicleSize vehicleSize, Status availabilityStatus)
        {
            this.spotNumber = spotNumber;
            this.vehicleSize = vehicleSize;
            this.availabilityStatus = availabilityStatus;
        }
       
    }
}
