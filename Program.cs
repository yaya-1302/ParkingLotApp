using System;
using ParkingLotApp.Controller;
using ParkingLotApp.Services;

class Program
{
	static void Main(string[] args)
	{
		ParkingManager manager = ParkingManagerController.CreateParkingManager();

		// ParkingManager? manager = null;

		do
		{
			Console.WriteLine("Enter command:");
			var command = Console.ReadLine();
			var commandArgs = command?.Split(' ');

			CommandHandler.HandleCommand(commandArgs, manager);
		} while (true);
	}
}
