using Marten;

namespace SimpleWebApi8;

public static class DeviceHandlers
{
    public static async Task<Device?> CreateDevice(DocumentStore store)
    {
        var deviceId = Guid.NewGuid();

        await using var session = store.LightweightSession();
        var deviceInitialized = new DeviceInitialized("My Device")
        {
            Volume = 50,
            Muted = false
        };
        var volumeChanged = new VolumeChanged { Volume = 75 };

        session.Events.StartStream<Device>(deviceId, deviceInitialized, volumeChanged);
        await session.SaveChangesAsync();
        
        var test = session.Events.AggregateStream<Device>(deviceId);
        return session.Events.AggregateStream<Device>(deviceId);
    }

    public static async Task<Device?> GetDeviceById(Guid deviceId, DocumentStore store)
    {
        await using var session = store.LightweightSession();
        return session.Events.AggregateStream<Device>(deviceId);
    }
}