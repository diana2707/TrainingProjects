
namespace SmartHome.Application.Models.Interfaces
{
    public interface IMeasurableLoad
    {
        public double CurrentWatts { get; }
        public double TotalWh { get; }
        public void ResetEnergy();
    }
}
