using System;

namespace ShefferMultioperationRank4
{
    public class Multioperation3 : IComparable<Multioperation3>
    {
        private readonly int _val;

        private const int OpMax = 7;
        private const int OpSize = 3;
        private const int PosA = 0;
        private const int PosB = 1;
        private const int PosC = 2;

        public int A { get { return (_val >> OpSize * PosA) & OpMax; } }
        public int B { get { return (_val >> OpSize * PosB) & OpMax; } }
        public int C { get { return (_val >> OpSize * PosC) & OpMax; } }

        public int Value { get { return _val; } }

        public static readonly Multioperation3 Zero = new Multioperation3(0, 0, 0);
        public static readonly Multioperation3 All = new Multioperation3(7, 7, 7);
        public static readonly Multioperation3 E = new Multioperation3(1, 2, 4);

        public Multioperation3(int a, int b, int c)
        {
            _val =
                ((a & OpMax) << OpSize * PosA) |
                ((b & OpMax) << OpSize * PosB) |
                ((c & OpMax) << OpSize * PosC);
        }

        public Multioperation3(int value)
        {
            this._val = value;
        }

        #region Equality members

        public bool Equals(Multioperation3 other)
        {
            return _val == other._val;
        }

        public override bool Equals(object obj)
        {
            return obj is Multioperation3 && Equals((Multioperation3)obj);
        }

        public override int GetHashCode()
        {
            return _val;
        }

        public static bool operator ==(Multioperation3 left, Multioperation3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Multioperation3 left, Multioperation3 right)
        {
            return !left.Equals(right);
        }

        #endregion

        #region Overrides of ValueType

        public int CompareTo(Multioperation3 other)
        {
            return other._val - _val;
        }

        public override string ToString()
        {
            return "Op(" + A + ", " + B + ", " + C + ")";
        }

        #endregion

        #region Metaoperations of multioperations
        private static int Translate(Multioperation3 op, int i)
        {
            var a = ((i & 1) >> PosA) * op.A;
            var b = ((i & 2) >> PosB) * op.B;
            var c = ((i & 4) >> PosC) * op.C;
            return a | b | c;
        }

        public static Multioperation3 operator *(Multioperation3 op1, Multioperation3 op2)
        {
            return new Multioperation3(Translate(op1, op2.A), Translate(op1, op2.B), Translate(op1, op2.C));
        }

        public static Multioperation3 operator &(Multioperation3 op1, Multioperation3 op2)
        {
            return new Multioperation3(op1._val & op2._val);
        }

        public static Multioperation3 operator !(Multioperation3 op)
        {
            var reta = (op.A & 1) | (op.B & 1) << 1 | (op.C & 1) << 2;
            var retb = (op.A & 2) >> 1 | (op.B & 2) | (op.C & 2) << 1;
            var retc = (op.A & 4) >> 2 | (op.B & 4) >> 1 | op.C & 4;
            return new Multioperation3(reta, retb, retc);
        }

        #endregion
    }
}

