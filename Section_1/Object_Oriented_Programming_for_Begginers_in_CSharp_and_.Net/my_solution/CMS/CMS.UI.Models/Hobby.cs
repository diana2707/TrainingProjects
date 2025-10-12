
namespace CMS.UI.Models;

public abstract class Hobby
{
    public string name;

    public string GetHobbyName()
    {
        return name;
    }

    public abstract string GetTypeOfHobby();
}