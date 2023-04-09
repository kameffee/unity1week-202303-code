using System;

namespace Unity1week202303.Sentences
{
    public readonly struct Identifier : IEquatable<Identifier>
    {
        public uint Value => _value;

        private readonly uint _value;

        public Identifier(uint value)
        {
            _value = value;
        }

        public bool Equals(Identifier other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is Identifier other && Equals(other);
        }

        public static bool operator ==(Identifier a, Identifier b)
        {
            return a.Value.Equals(b.Value);
        }

        public static bool operator !=(Identifier a, Identifier b)
        {
            return !a.Value.Equals(b.Value);
        }

        public override int GetHashCode()
        {
            return (int)_value;
        }
    }
}
