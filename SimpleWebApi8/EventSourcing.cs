using Marten.Events;

namespace SimpleWebApi8;

public abstract class NameChanged(string name)
{
    public string Name { get; set; } = name;

    public override string ToString()
    {
        return $"New name: {Name}  ";
    }
}

public class DescriptionChanged
{
    public required string Description { get; set; }
}

public class TagsChanged
{
    public required string[] Tags { get; set; }
}

public class PictureChanged
{
    public required byte[] Picture { get; set; }
}

public class PlaceChanged
{
    public required string Place { get; set; }
}

public class MyThingInitialized
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string[] Tags { get; set; }
    public required byte[] Picture { get; set; }
    public required string Place { get; set; }
}

public class MyThing(string name, Guid id)
{
    public Guid Id { get; set; } = id;
    public string Name { get; private set; } = name;
    public required string Description { get; set; }
    public required string[] Tags { get; set; }
    public required byte[] Picture { get; set; }
    public required string Place { get; set; }

    public void Apply(DescriptionChanged newDescription) => Description = newDescription.Description;
    public void Apply(TagsChanged newTags) => Tags = newTags.Tags;
    public void Apply(PictureChanged newPicture) => Picture = newPicture.Picture;
    public void Apply(PlaceChanged newPlace) => Place = newPlace.Place;
    public void Apply(MyThingInitialized myThingInitialized)
    {
        Name = myThingInitialized.Name;
        Description = myThingInitialized.Description;
        Tags = myThingInitialized.Tags;
        Picture = myThingInitialized.Picture;
        Place = myThingInitialized.Place;
    }

    public override string ToString()
    {
        return $"MyThing {Id} with name: {Name}, description: {Description}, tags: {string.Join(", ", Tags)}, place: {Place}";
    }

    public static MyThing Create(IEvent<MyThingInitialized> @event)
    {
        var newDevice = new MyThing(@event.Data.Name, @event.StreamId)
        {
            Description = @event.Data.Description,
            Tags = @event.Data.Tags,
            Picture = @event.Data.Picture,
            Place = @event.Data.Place
        };

        return newDevice;
    }
}