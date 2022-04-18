namespace ParkingLotSimulation.SimulationFiles
{
    internal class ParkingSpot
    {
        public int SpotNumber;

        public Size ParkingSize;

        public Status AvailabilityStatus;

        public string ParkingSpotName
        {
            get { return ParkingSize + "-" + SpotNumber; }
        }

        public ParkingSpot(int spotNumber, Size vehicleSize, Status availabilityStatus)
        {
            SpotNumber = spotNumber;
            ParkingSize = vehicleSize;
            AvailabilityStatus = availabilityStatus;
        }
    }
}
