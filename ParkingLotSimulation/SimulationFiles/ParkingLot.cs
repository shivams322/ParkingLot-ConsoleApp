using System;
using System.Linq;
using System.Collections.Generic;

namespace ParkingLotSimulation.SimulationFiles
{
    internal class ParkingLot
    {
        private static readonly List<ParkingSpot> ParkingSpots = new List<ParkingSpot>();

        private static readonly List<Ticket> VehiclesTicketList = new List<Ticket>();

        public static void InitializeParkingLot()
        {
            Console.Write("Enter 2 Wheeler Space: ");
            InitialParkingSpot(Convert.ToInt32(Console.ReadLine()), Size.Small, Status.Available);

            Console.Write("Enter 4 Wheeler Space: ");
            InitialParkingSpot(Convert.ToInt32(Console.ReadLine()), Size.Medium, Status.Available);

            Console.Write("Enter heavy Vehicle Space: ");
            InitialParkingSpot(Convert.ToInt32(Console.ReadLine()), Size.Large, Status.Available);

            SelectOption();
        }

        private static void InitialParkingSpot(int totalspots, Size vehicleType, Status availabilityStatus)
        {
            for (int i = 0; i < totalspots; i++)
            {
                ParkingSpots.Add(new ParkingSpot(i + 1, vehicleType, availabilityStatus));
            }
        }

        private static void SelectOption()
        {
            Loop:
            Console.Clear();
            Console.WriteLine("Select 1 to Display Parking Lot Occupancy details.\nSelect 2 to Park a Vehicle. \nSelect 3 to Unpark a Vehicle.");
            Console.Write("Please select from above options: ");
            int selectedOption = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (selectedOption)
            {
                case 1:
                    {
                        foreach (Size size in Enum.GetValues(typeof(Size)))
                        {
                            DisplayParkingLotStatus(size);
                        }
                        break;
                    }

                case 2:
                    {
                        Console.Write("1 for 2 Wheeler \n2 for 4 Wheeler \n3 for Heavy Vehicle \nPlease Enter Number for Type of Vehicle to Park: ");
                        int vehicleType = Convert.ToInt32(Console.ReadLine());
                        Size v1 = (Size)(vehicleType-1);
                        int parkingSpaceIndex = AvailableSpot(vehicleType);
                        Console.Write("Please Enter Vehicle Registration Number: ");
                        string vehicleNumber = Convert.ToString(Console.ReadLine());
                        Console.Clear();

                        if (VehiclesTicketList.Exists(vehicle => vehicle.VehicleNumber == vehicleNumber))
                        {
                            Console.WriteLine("This vehicle is already Present, please select other option.");
                            break;
                        }

                        if (parkingSpaceIndex < 0)
                        {
                            Console.WriteLine("No available Parking Spot");
                            break;
                        }
                        ParkingSpots[parkingSpaceIndex].AvailabilityStatus = Status.Full;
                        Ticket ticket = new Ticket(parkingSpaceIndex, vehicleNumber, ParkingSpots[parkingSpaceIndex].ParkingSpotName, DateTime.Now, v1);
                        VehiclesTicketList.Add(ticket);
                        Console.WriteLine("Parking Ticket Issued At Entry For");
                        ticket.ShowTicket();

                        break;
                    }
                case 3:
                    {
                        Console.Write("Please Enter Vehicle Registration Number to Unpark: ");
                        string vehicleNumber = Convert.ToString(Console.ReadLine());

                        if (!VehiclesTicketList.Exists(vehicle => vehicle.VehicleNumber == vehicleNumber))
                        {
                            Console.WriteLine("There is no such Vehicle in Parking Lot.");
                            break;
                        }

                        int index = VehiclesTicketList.FindIndex(vehicle => vehicle.VehicleNumber == vehicleNumber);
                        ParkingSpots[(VehiclesTicketList[index].Id)].AvailabilityStatus = Status.Available;
                        Console.Clear();

                        Console.WriteLine("Parking Ticket Issued At Exit For");
                        VehiclesTicketList[index].ShowTicket();
                        Console.WriteLine($"Exit Time is {DateTime.Now}");
                        int charge = CalculateCharge(VehiclesTicketList[index].ArrivalTime, DateTime.Now,VehiclesTicketList[index].VehicleSize);
                        Console.WriteLine($"Parking charge is Rs.{charge}");
                        VehiclesTicketList.RemoveAt(index);

                        break;
                    }
            }

            Console.WriteLine("\nDo you want to go back to main menu: Y/N");
            char answer = Convert.ToChar(Console.ReadLine());
            if (answer == 'Y' || answer == 'y')
                goto Loop;

        }

        private static int AvailableSpot(int vehicleType)
        {
            Nullable<int> parkingSpaceIndex = null;
            switch (vehicleType)
            {
                case 1:
                    {
                        parkingSpaceIndex = ParkingSpots.FindIndex(spot => spot.AvailabilityStatus == Status.Available);
                        break;
                    }
                case 2:
                    {
                        parkingSpaceIndex = ParkingSpots.FindIndex(spot => spot.AvailabilityStatus == Status.Available && spot.ParkingSize > Size.Small);
                        break;
                    }
                case 3:
                    {
                        parkingSpaceIndex = ParkingSpots.FindIndex(spot => spot.AvailabilityStatus == Status.Available && spot.ParkingSize > Size.Medium);
                        break;
                    }
            }

            return (int)parkingSpaceIndex;
        }

        private static int GetTotalSpots(Size vehicleSize)
        {
            return (ParkingSpots.FindAll(spot => spot.ParkingSize == vehicleSize)).Count;
        }

        private static int GetFreeSpots(Size vehicleSize)
        {
            return ParkingSpots.Where(spot => spot.ParkingSize == vehicleSize && spot.AvailabilityStatus == Status.Available).Count();
        }
        private static void DisplayParkingLotStatus(Size vehicleSize)
        {
            string spotName = "";
            int totalSpots = GetTotalSpots(vehicleSize);
            int freeSpots = GetFreeSpots(vehicleSize);
            switch (vehicleSize)
            {
                case Size.Small:
                    spotName = "2 Wheeler Spots";
                    break;
                case Size.Medium:
                    spotName = "4 Wheeler Spots";
                    break;
                case Size.Large:
                    spotName = "Heavy Vehicle Spots";
                    break;
            }

            Console.WriteLine($"Total {spotName} are: {totalSpots}");
            Console.WriteLine($"Available {spotName} are: {freeSpots}");
            Console.WriteLine($"Reserverd {spotName} are: {totalSpots - freeSpots}");
            Console.WriteLine();
        }

        private static int CalculateCharge(DateTime entryTime, DateTime exitTime,Size vehicleSize)
        {
            int chargePerHour = vehicleSize == Size.Small ? 10 : vehicleSize == Size.Medium ? 20 : 30;
            TimeSpan span = exitTime.Subtract(entryTime);
            return (span.Hours + 1) * chargePerHour;
        }
    }
}
