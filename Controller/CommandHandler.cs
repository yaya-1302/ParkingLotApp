using ParkingLotApp.Models;
using ParkingLotApp.Services;

namespace ParkingLotApp.Controller
{
	public static class CommandHandler
	{
		private static ParkingManager manager;

		static CommandHandler()
		{
			manager = new ParkingManager(new ParkingLot(0));
		}

		public static void HandleCommand(string[]? commandArgs)
		{
			if (commandArgs == null || commandArgs.Length < 1) return;

			switch (commandArgs[0])
			{
				case "create_parking_lot":
					HandleCreateParkingLot(commandArgs);
					break;

				case "park":
					HandleParkVehicle(commandArgs);
					break;

				case "leave":
					HandleLeaveVehicle(commandArgs);
					break;

				case "status":
					HandleStatus();
					break;

				case "type_of_vehicles":
					HandleVehicleTypeReport(commandArgs);
					break;

				case "registration_numbers_for_vehicles_with_odd_plate":
					HandleVehiclesByPlate("odd");
					break;

				case "registration_numbers_for_vehicles_with_even_plate":
					HandleVehiclesByPlate("even");
					break;

				case "registration_numbers_for_vehicles_with_color":
					HandleVehicleByColor(commandArgs);
					break;
					
				case "slot_numbers_for_vehicles_with_colour":
					HandleSlotNumbersByColor(commandArgs);
					break;

				case "slot_number_for_registration_number":
					HandleSlotByRegistration(commandArgs);
					break;
				
				
				case "exit":
					Environment.Exit(0);
					break;
				default:
					Console.WriteLine("Invalid command.");
					break;
			}
		}

		private static void HandleCreateParkingLot(string[] commandArgs)
		{
			if (commandArgs.Length < 2 || !int.TryParse(commandArgs[1], out int numberOfSlots) || numberOfSlots <= 0)
			{
				Console.WriteLine("Please specify a valid number of parking slots.");
				return;
			}

			// Create a new parking lot with the specified number of slots
			ParkingLot parkingLot = new(numberOfSlots);
			manager = new ParkingManager(parkingLot); // Update the ParkingManager with the new lot
			Console.WriteLine($"Created a parking lot with {numberOfSlots} slots.");
		}

		private static void HandleParkVehicle(string[] commandArgs)
		{
			if (commandArgs.Length < 4)
			{
				Console.WriteLine("Please provide registration number, color, and vehicle type.");
				return;
			}

			string registrationNumber = commandArgs[1];
			string color = commandArgs[2];
			string type = commandArgs[3];
			Console.WriteLine(manager.ParkVehicle(registrationNumber, color, type));
		}

		private static void HandleLeaveVehicle(string[] commandArgs)
		{
			if (commandArgs.Length < 2 || !int.TryParse(commandArgs[1], out int slotNumber))
			{
				Console.WriteLine("Please provide a valid slot number.");
				return;
			}

			Console.WriteLine(manager.LeaveSlot(slotNumber));
		}

		private static void HandleStatus()
		{
			Console.WriteLine(manager.GetParkingStatus());
		}

		private static void HandleVehicleTypeReport(string[] commandArgs)
		{
			if (commandArgs.Length < 2)
			{
				Console.WriteLine("Please specify a vehicle type (Mobil or Motor).");
				return;
			}

			string type = commandArgs[1];
			Console.WriteLine(manager.GetReportByType(type));
		}

		private static void HandleVehicleByColor(string[] commandArgs)
		{
			if (commandArgs.Length < 2)
			{
				Console.WriteLine("Please specify a color.");
				return;
			}

			string color = commandArgs[1];
			Console.WriteLine(manager.GetVehiclesByColor(color));
		}

		private static void HandleVehiclesByPlate(string commandArgs)
		{
			if (commandArgs.Length < 2)
			{
				Console.WriteLine("Please specify a plate prefix.");
				return;
			}

			string platePrefix = commandArgs;
			Console.WriteLine(manager.GetVehiclesByPlate(platePrefix));
		}

		private static void HandleSlotByRegistration(string[] commandArgs)
		{
			if (commandArgs.Length < 2)
			{
				Console.WriteLine("Please provide a valid registration number.");
				return;
			}

			string registrationNumber = commandArgs[1];
			Console.WriteLine(manager.GetSlotForVehicleByRegistration(registrationNumber));
		}

		private static void HandleSlotNumbersByColor(string[] commandArgs)
		{
			if (commandArgs.Length < 2)
			{
				Console.WriteLine("Please specify a color.");
				return;
			}

			string color = commandArgs[1];
			Console.WriteLine(manager.GetSlotNumbersByColor(color));
		}

	}
}
