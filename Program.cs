using System;
using ParkingLotApp.Controller;
using ParkingLotApp.Services;

class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("#==============================#");
		Console.WriteLine("# Welcome to My Parking System #");
		Console.WriteLine("#==============================#\n");

		Console.WriteLine("First thing you need to Create parking space:\n#create_parking_lot [parking slot space]\n");
		Console.WriteLine("To manage Parking Lot you can use these commands:\n");
		Console.WriteLine(
			"1. Park your vehicle:\n#park [police number] [ vehicle color] [Mobil/Motor]\n"
		);
		Console.WriteLine("2. Leave your vehicle:\n#leave [parking slot number]\n");
		Console.WriteLine("3. See parking:\n#status\n");
		Console.WriteLine("4. Count type vehicle:\n#type_of_vehicles [Mobil/Motor]\n");
		Console.WriteLine("5. Find vehicles with odd plate:\n#registration_numbers_for_vehicles_with_odd_plate\n");
		Console.WriteLine("6. Find vehicles with even plate:\n#registration_numbers_for_vehicles_with_even_plate\n");
		Console.WriteLine("7. Find vehicles by color:\n#registration_numbers_for_vehicles_with_color\n");
		Console.WriteLine("8. Find slot parking by color:\n#slot_numbers_for_vehicles_with_color\n");
		Console.WriteLine("9. Find slot parking by plate:\n#slot_number_for_registration_number\n");
		Console.WriteLine("10. To exit:\n#exit\n");

		do
		{
			Console.WriteLine("Enter command:");
			var command = Console.ReadLine();
			var commandArgs = command?.Split(' ');

			CommandHandler.HandleCommand(commandArgs);
		} while (true);
	}
}
