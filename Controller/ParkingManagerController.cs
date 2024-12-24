using ParkingLotApp.Models;
using ParkingLotApp.Services;

namespace ParkingLotApp.Controller;

public static class ParkingManagerController
{
	public static ParkingManager CreateParkingManager()
	{
		Console.WriteLine("Enter number of parking slots:");
		int totalSlots = int.Parse(Console.ReadLine() ?? "0");

		var parkingLot = new ParkingLot(totalSlots);
		return new ParkingManager(parkingLot);
	}
}
