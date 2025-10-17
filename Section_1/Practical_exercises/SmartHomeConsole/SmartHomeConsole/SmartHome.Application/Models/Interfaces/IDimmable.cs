
namespace SmartHome.Application.Models.Interfaces
{
    public interface IDimmable
    {
        public int Brightness { get; }
        public void SetBrightness(int value);
    }
}
