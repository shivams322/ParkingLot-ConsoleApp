using System;

namespace ParkingLotSimulation.SimulationFiles
{
    internal class Ticket
    {
        public string vehicleNumber;

        public int spotNumber;

        public DateTime arrivalTime;

        public VehicleSize vehicleSize;

        public Ticket(string vehicle, int availableSpotNumber,DateTime entryTime,VehicleSize vehicleSize)
        {
            this.vehicleNumber = vehicle;
            this.spotNumber = availableSpotNumber;
            this.arrivalTime = entryTime;
            this.vehicleSize = vehicleSize;
        }

        public void ShowTicket()
        {
            Console.WriteLine($"Vehicle Number is {vehicleNumber}");
            Console.WriteLine($"Parking Spot Number is {spotNumber}");
            Console.WriteLine($"Entry Time is {arrivalTime}");
        }
    }
}
