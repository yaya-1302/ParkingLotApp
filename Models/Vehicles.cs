using ParkingLotApp.Constant;
namespace ParkingLotApp.Models;

public class Vehicles(string registrationNumber, string color, VehiclesType type)
{
    public string RegistrationNumber { get; } = registrationNumber;
    public string Color { get; } = color;
    public VehiclesType Type { get; } = type;
    public DateTime CheckInTime { get; } = DateTime.Now;

    public int GetParkingDurationInHours()
	{
		return (int)(DateTime.Now - CheckInTime).TotalHours + 1;
	}

	public decimal CalculateParkingFee()
	{
		int hours = GetParkingDurationInHours();

		decimal firstHourRate = Type == VehiclesType.Mobil ? 15000 : 8000; // 15rb untuk mobil, 8rb untuk motor
		decimal additionalHourRate = Type == VehiclesType.Mobil ? 5000 : 2000; // 5rb untuk mobil, 2rb untuk motor

		if (hours == 1)
		{
			// Jika hanya parkir selama 1 jam
			return firstHourRate;
		}
		else
		{
			// Jam pertama + jam berikutnya
			return firstHourRate + (hours - 1) * additionalHourRate;
		}
	}
}
