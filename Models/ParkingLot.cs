namespace ParkingLotApp.Models;

public class ParkingLot
{
	public List<ParkingSlot> Slots { get; private set; }

	public ParkingLot(int numberOfSlots)
	{
		Slots = [];


		for (int i = 1; i <= numberOfSlots; i++)
		{
			Slots.Add(new ParkingSlot(i));
		}
	}

	public bool HasAvailableSlots()
	{
		return Slots.Any(slot => slot.Vehicle == null);
	}
	
	public ParkingSlot? FindAvailableSlot()
	{
		return Slots.FirstOrDefault(slot => slot.Vehicle == null);
	}

	public ParkingSlot? GetSlotByRegistrationNumber(string registrationNumber)
	{
		return Slots.FirstOrDefault(slot => slot.Vehicle?.RegistrationNumber == registrationNumber);
	}
}
