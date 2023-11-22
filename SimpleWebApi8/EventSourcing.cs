using Marten.Events;

namespace SimpleWebApi8;

public class VolumeChanged
{
    public int Volume { get; init; }

    public override string ToString()
    {
        return $"New volume: {Volume}  ";
    }
}

public abstract class MuteToggled(bool muted)
{
    public bool Muted { get; set; } = muted;

    public override string ToString()
    {
        return $"Muted: {Muted}  ";
    }
}

public abstract class NameChanged(string name)
{
    public string Name { get; set; } = name;

    public override string ToString()
    {
        return $"New name: {Name}  ";
    }
}

public class DeviceInitialized(string name)
{
    public string Name { get; init; } = name;
    public int Volume { get; init; }
    public bool Muted { get; init; }

    public override string ToString()
    {
        return $"Initialized with name: {Name}, volume: {Volume}, muted: {Muted}  ";
    }
}

public class Device(string name, Guid id)
{
    public Guid Id { get; set; } = id;
    public string Name { get; private set; } = name;
    private int Volume { get; set; }
    private bool Muted { get; set; }

    public void Apply(VolumeChanged newVolume) => Volume = newVolume.Volume;
    public void Apply(MuteToggled muteToggled) => Muted = muteToggled.Muted;
    public void Apply(NameChanged newName) => Name = newName.Name;
    public void Apply(DeviceInitialized deviceInitialized)
    {
        Name = deviceInitialized.Name;
        Volume = deviceInitialized.Volume;
        Muted = deviceInitialized.Muted;
    }

    public override string ToString()
    {
        return $"Device {Id} with name: {Name}, volume: {Volume}, muted: {Muted}  ";
    }
    
    public static Device Create(IEvent<DeviceInitialized> @event)
    {
        var newDevice = new Device(@event.Data.Name, @event.StreamId)
        {
            Muted = @event.Data.Muted,
            Volume = @event.Data.Volume
        };

        return newDevice;
    }
}