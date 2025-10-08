using System;

namespace Classes_M2;

public class BankCustomer
{
    private static int s_nextCustomerId;
    private string _firstName = "Tim";
    private string _lastName = "Shao";
    public readonly string CustomerId;

    static BankCustomer()
    {
        Random random = new Random();
        s_nextCustomerId = random.Next(10000000, 20000000);
    }

    public BankCustomer(string firstName, string lastName)
    {
        this._firstName = firstName;
        this._lastName = lastName;
        this.CustomerId = (s_nextCustomerId++).ToString("D10");
    }

    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    public string ReturnFullName()
    {
        return $"{_firstName} {_lastName}";
    }

    public void UpdateName(string firstName, string lastName)
    {
        _firstName = firstName;
        _lastName = lastName;
    }

    public string DisplayCustomerInfo()
    {
        return $"Customer ID: {CustomerId}, Name: {ReturnFullName()}";
    }

}
