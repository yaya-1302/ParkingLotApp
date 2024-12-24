using ParkingLotApp.Constant;
using ParkingLotApp.Models;

namespace ParkingLotApp.Services;

public class ParkingManager(ParkingLot parkingLot)
{
	private ParkingLot _parkingLot = parkingLot;

	public string ParkVehicle(string registrationNumber, string color, string type)
	{
		if (!Enum.TryParse(type, true, out VehiclesType vehicleType))
		{
			return "Sorry, only Mobil and Motor are allowed.";
		}

		if (_parkingLot.Slots.Any(slot => slot.Vehicle != null && slot.Vehicle.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase)))
		{
			return $"Vehicle with registration number {registrationNumber} is already parked.";
		}

		var availableSlot = _parkingLot.FindAvailableSlot();
		if (availableSlot == null)
		{
			return "Sorry, parking lot is full.";
		}

		var vehicle = new Vehicles(registrationNumber, color, vehicleType);
		availableSlot.Vehicle = vehicle;
		return $"Allocated slot number: {availableSlot.SlotNumber}";
	}

	public string LeaveSlot(int slotNumber)
	{
		var slot = _parkingLot.Slots.FirstOrDefault(s => s.SlotNumber == slotNumber);
		if (slot == null || slot.Vehicle == null)
		{
			return "Slot not found or already empty.";
		}

		var vehicle = slot.Vehicle;
		decimal fee = vehicle.CalculateParkingFee();
		slot.Vehicle = null;
		return $"Slot number {slotNumber} is free. Total fee for {vehicle.GetParkingDurationInHours} hours : Rp.{fee} ";
	}


	public string GetParkingStatus()
	{
		var status = "Slot\tNo.\tType\tRegistration No\tColour\n";
		foreach (var slot in _parkingLot.Slots)
		{
			if (slot.Vehicle != null)
			{
				var vehicle = slot.Vehicle;
				status += $"{slot.SlotNumber}\t{vehicle.RegistrationNumber}\t{vehicle.Type}\t{vehicle.RegistrationNumber}\t{vehicle.Color}\n";
			}
		}
		return status;
	}

	public string GetReportByType(string type)
	{
		// Validate input type
		if (!Enum.TryParse(type, true, out VehiclesType vehicleType))
		{
			return "Invalid vehicle type. Please use 'Mobil' or 'Motor'.";
		}

		// Count vehicles by type
		var count = _parkingLot.Slots.Count(s => s.Vehicle?.Type == vehicleType);
		return $"{count} {type} vehicles are currently parked.";
	}

	public string GetVehiclesByPlate(string plateType)
	{
		var vehicles = _parkingLot.Slots
			.Where(slot => slot.Vehicle != null && (plateType == "odd" ? int.Parse(slot.Vehicle.RegistrationNumber.Split('-')[1]) % 2 != 0 : int.Parse(slot.Vehicle.RegistrationNumber.Split('-')[1]) % 2 == 0))
			.Select(slot => slot.Vehicle?.RegistrationNumber)
			.ToList();

		return string.Join(", ", vehicles);
	}

	public string GetVehiclesByColor(string color)
	{
		var vehicles = _parkingLot.Slots
			.Where(slot => slot.Vehicle != null && slot.Vehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
			.Select(slot => slot.Vehicle?.RegistrationNumber)
			.ToList();

		if (vehicles.Count == 0)
		{
			return $"No vehicles found with the specified color: {color}.";
		}

		return string.Join(", ", vehicles);
	}

	public string GetSlotForVehicleByRegistration(string registrationNumber)
	{
		var slot = _parkingLot.Slots
			.FirstOrDefault(s => s.Vehicle != null && s.Vehicle.RegistrationNumber.Equals(registrationNumber, StringComparison.OrdinalIgnoreCase));

		if (slot == null)
		{
			return $"Vehicle with registration number {registrationNumber} not found.";
		}

		return $"Slot number {slot.SlotNumber} is occupied by vehicle {registrationNumber}.";
	}
	public string GetSlotNumbersByColor(string color)
	{
		var groupedSlots = _parkingLot.Slots
			.Where(slot => slot.Vehicle != null && 
						   slot.Vehicle.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
			.GroupBy(slot => slot.Vehicle!.Type) 
			.ToDictionary(group => group.Key, group => group.Select(slot => slot.SlotNumber).ToList());

		var result = groupedSlots.Select(group =>
		{
			string vehicleType = Enum.GetName(typeof(VehiclesType), group.Key) ?? "Unknown";
			string slotNumbers = string.Join(", ", group.Value);
			return $"{vehicleType}: {slotNumbers}";
		});

		return result.Any()
			? string.Join(Environment.NewLine, result)
			: "No vehicles found with the specified color.";
	}



}
