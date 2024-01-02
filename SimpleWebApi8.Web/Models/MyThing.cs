namespace SimpleWebApi8.Web.Models;

public record MyThing(
    Guid Id, string Name, 
    string Description, 
    string[] Tags, 
    byte[] Picture, 
    string Place);