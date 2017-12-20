namespace Moviebase.Tests
{
    public class ValueObject
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public static ValueObject CreateMock()
        {
            return new ValueObject {Name = "Fahmi", Value = "t"};
        }
    }
}
