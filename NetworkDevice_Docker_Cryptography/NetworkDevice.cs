
// Base class: NetworkDevice
public abstract class NetworkDevice
{
    public string IPAddress { get; set; }
    public string MACAddress { get; set; }

    // Abstract method to be overridden by derived classes
    public abstract void Connect();

    // Constructor to initialize IPAddress and MACAddress
    protected NetworkDevice(string ipAddress, string macAddress)
    {
        IPAddress = ipAddress;
        MACAddress = macAddress;
    }
}

// Derived class: Router (inheriting from NetworkDevice)
public class Router : NetworkDevice
{
    public int NumberOfPorts { get; set; }

    public Router(string ipAddress, string macAddress, int numberOfPorts)
        : base(ipAddress, macAddress)
    {
        NumberOfPorts = numberOfPorts;
    }

    // Override Connect method to simulate connecting to the network
    public override void Connect()
    {
        Console.WriteLine($"Router with IP {IPAddress} and MAC {MACAddress} is connecting with {NumberOfPorts} ports.");
    }
}

// Derived class: Switch
public class Switch : NetworkDevice
{
    public int NumberOfPorts { get; set; }

    public Switch(string ipAddress, string macAddress, int numberOfPorts)
        : base(ipAddress, macAddress)
    {
        NumberOfPorts = numberOfPorts;
    }

    // Override Connect method to simulate connecting to the network
    public override void Connect()
    {
        Console.WriteLine($"Switch with IP {IPAddress} and MAC {MACAddress} is connecting with {NumberOfPorts} ports.");
    }
}

// Derived class: AccessPoint
public class AccessPoint : NetworkDevice
{
    public string SSID { get; set; }

    public AccessPoint(string ipAddress, string macAddress, string ssid)
        : base(ipAddress, macAddress)
    {
        SSID = ssid;
    }

    // Override Connect method to simulate connecting to the network
    public override void Connect()
    {
        Console.WriteLine($"AccessPoint with IP {IPAddress}, MAC {MACAddress}, and SSID '{SSID}' is connecting to the network.");
    }
}
