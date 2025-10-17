
namespace SmartHome.Application.Models.Interfaces
{
    public interface ITemperatureControl
    {
        double TargetCelsius { get; }
        void SetTarget(double celsius);
    }
}
