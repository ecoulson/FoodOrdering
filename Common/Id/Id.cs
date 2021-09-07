using System;
namespace Common.Id
{
    public abstract class Id: IId
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

        public override bool Equals(object other)
        {
            if (!(other is IId))
            {
                return false;
            }
            if (other == null)
            {
                return false;
            }
            return Value == ((IId)other).Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
