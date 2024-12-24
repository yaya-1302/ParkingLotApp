namespace ParkingLotApp.Models;

public class ParkingSlot(int slotNumber)
{
    public int SlotNumber { get; } = slotNumber;
    public Vehicles? Vehicle { get; set; } = null;
}
