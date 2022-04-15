using System;
using System.Collections.Generic;

namespace ParkingLotSimulation.SimulationFiles
{
    internal class ParkingLot
    {
        private static readonly List<ParkingSpot> ParkingSpots = new List<ParkingSpot>();

        private static readonly List<Ticket> VehiclesList = new List<Ticket>();

        public static int twoWheelerSpace, fourWheelerSpace, heavyVehiclespace;

        public static void InitializeParkingLot()
        {
            Console.Write("Enter 2 Wheeler Space: ");
            twoWheelerSpace = Convert.ToInt32(Console.ReadLine());
            InitialParkingSpot(1,twoWheelerSpace,VehicleSize.Small,Status.Available);

            Console.Write("Enter 4 Wheeler Space: ");
            fourWheelerSpace = Convert.ToInt32(Console.ReadLine());
            InitialParkingSpot(twoWheelerSpace+1, fourWheelerSpace, VehicleSize.Medium, Status.Available);

            Console.Write("Enter heavy Vehicle Space: ");
            heavyVehiclespace = Convert.ToInt32(Console.ReadLine());
            InitialParkingSpot(twoWheelerSpace+fourWheelerSpace+1, heavyVehiclespace, VehicleSize.Large, Status.Available);

            SelectOption();
        }

        private static void InitialParkingSpot(int spotNumber, int totalspots,VehicleSize vehicleType,Status availabilityStatus)
        {
            for(int i = 0; i < totalspots; i++)
            {
                ParkingSpots.Add(new ParkingSpot(spotNumber++, vehicleType, availabilityStatus));
            }
        }

        private static void SelectOption()
        {   Loop:
            Console.WriteLine("Select 0 to Display Parking Lot Occupancy details.");
            Console.WriteLine("Select 1 to Park a Vehicle.");
            Console.WriteLine("Select 2 to Unpark a Vehicle.");
            Console.Write("Please select an option from given: ");////
            int selectedOption = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            switch (selectedOption)
            {
                case 0:
                    {
                        Console.Clear();
                        DisplayParkingLotStatus(twoWheelerSpace, fourWheelerSpace, heavyVehiclespace);
                        break;
                    }

                case 1:
                    {
                        Console.Write("Please Enter the Type of Vehicle: 0 for 2 Wheeler/1 for 4 Wheeler/2 for Heavy Vehicle: ");
                        int vehicleType = Convert.ToInt32(Console.ReadLine());
                        VehicleSize v1 = (VehicleSize)vehicleType;
                        int spotNumber = AvailableSpot(vehicleType);

                        if (spotNumber == 0)
                        {
                            Console.WriteLine("There is No Available Spot");
                        }
                        else
                        {
                            Console.Write("Please Enter Vehicle Registration Number: ");
                            string vehicleNumber = Convert.ToString(Console.ReadLine());

                            if (VehiclesList.Exists(vehicle => vehicle.vehicleNumber == vehicleNumber))
                            {
                                Console.WriteLine("This vehicle is already Present, please select other option.");
                            }
                            else
                            {
                                Ticket ticket = new Ticket(vehicleNumber, spotNumber, DateTime.Now, v1);
                                Console.Clear();
                                Console.WriteLine("Parking Ticket Issued At Entry For");
                                ticket.ShowTicket();
                                ParkingSpots[spotNumber - 1].availabilityStatus = Status.Full;
                                VehiclesList.Add(new Ticket(vehicleNumber, spotNumber, DateTime.Now, ParkingSpots[spotNumber - 1].vehicleSize));
                            }

                        }

                        break;
                    }
                case 2:
                    {
                        Console.Write("Please Enter Vehicle Registration Number to Unpark: ");
                        string vehicleNumber = Convert.ToString(Console.ReadLine());

                        if (VehiclesList.Exists(vehicle => vehicle.vehicleNumber == vehicleNumber))
                        {
                            int index = VehiclesList.FindIndex(vehicle => vehicle.vehicleNumber == vehicleNumber);
                            ParkingSpots[(VehiclesList[index].spotNumber) - 1].availabilityStatus = Status.Available;
                            Ticket ticket = new Ticket(vehicleNumber, (VehiclesList[index].spotNumber), VehiclesList[index].arrivalTime, VehiclesList[index].vehicleSize);
                            VehiclesList.RemoveAt(index);
                            Console.Clear();
                            Console.WriteLine("Parking Ticket At Exit For");
                            ticket.ShowTicket();
                            Console.WriteLine($"Exit Time is {DateTime.Now}");
                        }
                        else
                        {
                            Console.WriteLine("There is no such Vehicle is in Parking Lot.");
                        }

                        break;
                    }
            }
            
            Console.WriteLine();
            goto Loop;
        }

        private static int AvailableSpot(int vehicleType)
        {
            int spotNum = 0;
            foreach (var spot in ParkingSpots)
            {
                if (vehicleType == 0)
                {
                    if (spot.vehicleSize == VehicleSize.Small && spot.availabilityStatus == Status.Available)
                    {
                        spotNum = spot.spotNumber;
                        break;
                    }
                }
                else if (vehicleType == 1)
                {
                    if (spot.vehicleSize == VehicleSize.Medium && spot.availabilityStatus == Status.Available)
                    {
                        spotNum = spot.spotNumber;
                        break;
                    }
                }
                else if (vehicleType == 2)
                {
                    if (spot.vehicleSize == VehicleSize.Large && spot.availabilityStatus == Status.Available)
                    {
                        spotNum = spot.spotNumber;
                        break;
                    }
                }
            }

            return spotNum;
        }
        private static void DisplayParkingLotStatus(int total2WheelerSpot, int total4WheelerSpot, int totalHeavyVehicleSpot)
        {
            int freeMspot = 0, freeCspot = 0, freeHspot = 0;
            ParkingSpots.ForEach(spot => {
                if (spot.vehicleSize == VehicleSize.Small && spot.availabilityStatus == Status.Available)
                {
                    freeMspot++;
                }
                else if (spot.vehicleSize == VehicleSize.Medium && spot.availabilityStatus == Status.Available)
                {
                    freeCspot++;
                }
                else if (spot.vehicleSize == VehicleSize.Large && spot.availabilityStatus == Status.Available)
                {
                    freeHspot++;
                }
            });

            Console.WriteLine($"Total 2 Wheeler Spots are: {total2WheelerSpot}");
            Console.WriteLine($"Available 2 Wheeler Spots are: {freeMspot}");
            Console.WriteLine($"Reserverd 2 Wheeler Spots are: {total2WheelerSpot - freeMspot}");
            Console.WriteLine();

            Console.WriteLine($"Total 4 Wheeler Spots are: {total4WheelerSpot}");
            Console.WriteLine($"Available 4 Wheeler Spots are: {freeCspot}");
            Console.WriteLine($"Reserverd 4 Wheeler Spots are: {total4WheelerSpot - freeCspot}");
            Console.WriteLine();

            Console.WriteLine($"Total Heavy Vehicle Spots are: {totalHeavyVehicleSpot}");
            Console.WriteLine($"Available Heavy Vehicle Spots are: {freeHspot}");
            Console.WriteLine($"Reserverd Heavy Vehicle Spots are: {totalHeavyVehicleSpot - freeHspot}");
            Console.WriteLine();
        }
    }
}
