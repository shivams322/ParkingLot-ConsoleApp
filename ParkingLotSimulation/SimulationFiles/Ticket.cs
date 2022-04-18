using System;

namespace ParkingLotSimulation.SimulationFiles
{
    internal class Ticket
    {
        public string VehicleNumber;

        public int Id;

        public string ParkingSpotName;

        public DateTime ArrivalTime;

        public Size VehicleSize;

        public Ticket(int availableSpotNumber, string vehicle, string parkingSpotName, DateTime entryTime, Size vehicleSize)
        {
            Id = availableSpotNumber;
            VehicleNumber = vehicle;
            ParkingSpotName = parkingSpotName;
            ArrivalTime = entryTime;
            VehicleSize = vehicleSize;
        }

        public void ShowTicket()
        {
            Console.WriteLine($"Vehicle Number is {VehicleNumber}");
            Console.WriteLine($"Parking Space Name is {ParkingSpotName}");
            Console.WriteLine($"Entry Time is {ArrivalTime}");
        }
    }
}
