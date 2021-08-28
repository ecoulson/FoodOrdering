using System;
namespace Common.Id
{
    public abstract class Id: IId, IEquatable<IId>
    {
        public string Value { get; }

        public Id(IId id)
        {
            Value = id.Value;
        }

        public Id(string value)
        {
            Assert.NotNull(value, "[Id] Can not create id with null value");
            Value = value;
        }

        public bool Equals(IId other)
        {
            if (other == null)
            {
                return false;
            }
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
