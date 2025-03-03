namespace CRUD
{
    public interface ISerializerFactory
    {
        Serializer CreateSerializer();
    }
    public class JsonFactory : ISerializerFactory
    {
        public JsonFactory(string name)
        {
            Name = name;
        }
        public Serializer CreateSerializer()
        {
            return new JsonSerializer();
        }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
    public class BinaryFactory : ISerializerFactory
    {
        public BinaryFactory(string name)
        {
            Name = name;
        }
        public Serializer CreateSerializer()
        {
            return new BinarySerializer();
        }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
    public class ClientFactory : ISerializerFactory
    {
        public ClientFactory(string name)
        {
            Name = name;
        }
        public Serializer CreateSerializer()
        {
            return new ClientSerializer();
        }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}