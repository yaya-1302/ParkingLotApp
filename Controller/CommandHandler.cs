using ParkingLotApp.Models;
using ParkingLotApp.Services;

namespace ParkingLotApp.Controller;

public static class CommandHandler
{
	public static void HandleCommand(string[]? commandArgs, ParkingManager manager)
	{
		if (commandArgs == null || commandArgs.Length < 1) return;

		switch (commandArgs[0])
		{
			case "create_parking_lot":
				HandleCreateParkingLot(commandArgs, manager);
				break;

			case "park":
				HandleParkVehicle(commandArgs, manager);
				break;

			case "leave":
				HandleLeaveVehicle(commandArgs, manager);
				break;

			case "status":
				HandleStatus(manager);
				break;

			case "type_of_vehicles":
				HandleVehicleTypeReport(commandArgs, manager);
				break;

			case "registration_numbers_for_vehicles_with_colour":
				HandleVehicleByColor(commandArgs, manager);
				break;

			case "slot_number_for_registration_number":
				HandleSlotByRegistration(commandArgs, manager);
				break;

			case "registration_numbers_for_vehicles_with_ood_plate":
				HandleVehiclesByPlate(commandArgs, manager);
				break;

			case "exit":
				Environment.Exit(0);
				break;

			default:
				Console.WriteLine("Invalid command.");
				break;
		}
	}


	private static void HandleCreateParkingLot(string[] commandArgs, ParkingManager manager)
	{
		if (commandArgs.Length < 2 || !int.TryParse(commandArgs[1], out int numberOfSlots) || numberOfSlots <= 0)
		{
			Console.WriteLine("Please specify a valid number of parking slots.");
			return;
		}

		// Create a new parking lot with the specified number of slots
		ParkingLot parkingLot = new(numberOfSlots);
		manager = new ParkingManager(parkingLot);
		Console.WriteLine($"Created a parking lot with {numberOfSlots} slots.");
	}

	// Handler for parking vehicle
	private static void HandleParkVehicle(string[] commandArgs, ParkingManager manager)
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

	// Handler for leaving vehicle
	private static void HandleLeaveVehicle(string[] commandArgs, ParkingManager manager)
	{
		if (commandArgs.Length < 2 || !int.TryParse(commandArgs[1], out int slotNumber))
		{
			Console.WriteLine("Please provide a valid slot number.");
			return;
		}

		Console.WriteLine(manager.LeaveSlot(slotNumber));
	}

	// Handler for showing parking lot status
	private static void HandleStatus(ParkingManager manager)
	{
		Console.WriteLine(manager.GetParkingStatus());

	}

	// Handler for type of vehicles report
	private static void HandleVehicleTypeReport(string[] commandArgs, ParkingManager manager)
	{
		if (commandArgs.Length < 2)
		{
			Console.WriteLine("Please specify a vehicle type (Mobil or Motor).");
			return;
		}

		string type = commandArgs[1];
		Console.WriteLine(manager.GetReportByType(type));
	}

	// Handler for vehicles by color
	private static void HandleVehicleByColor(string[] commandArgs, ParkingManager manager)
	{
		if (commandArgs.Length < 2)
		{
			Console.WriteLine("Please specify a color.");
			return;
		}

		string color = commandArgs[1];
		Console.WriteLine(manager.GetVehiclesByColor(color));
	}

	// Handler for vehicles by registration plate prefix
	private static void HandleVehiclesByPlate(string[] commandArgs, ParkingManager manager)
	{
		if (commandArgs.Length < 2)
		{
			Console.WriteLine("Please specify a plate prefix.");
			return;
		}

		string platePrefix = commandArgs[1];
		Console.WriteLine(manager.GetVehiclesByPlate(platePrefix));
	}

	// Handler for slot number based on registration number
	private static void HandleSlotByRegistration(string[] commandArgs, ParkingManager manager)
	{
		if (commandArgs.Length < 2)
		{
			Console.WriteLine("Please provide a valid registration number.");
			return;
		}

		string registrationNumber = commandArgs[1];
		Console.WriteLine(manager.GetSlotForVehicleByRegistration(registrationNumber));
	}
}
