using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShefferMultioperationRank4
{
    /// <summary>
    /// Represents the multioperation and implements metaoperations
    /// </summary>
    public class Multioperation6 : IComparable<Multioperation6>
    {
        /// <summary>
        /// Decimal representation of multioperation
        /// </summary>
        private readonly long _val;

        /// <summary>
        /// Decimal representation of full multioperation pi
        /// </summary>
        private const int OpMax = 63;

        /// <summary>
        /// Rang of algebra (cardinality of set A)
        /// </summary>
        private const int OpSize = 6;

        /// <summary>
        /// Defines the first component of multioperation
        /// </summary>
        private const int PosA = 0;

        /// <summary>
        /// Defines the second component of multioperation
        /// </summary>
        private const int PosB = 1;

        /// <summary>
        /// Defines the third component of multioperation
        /// </summary>
        private const int PosC = 2;

        /// <summary>
        /// Defines the fourth component of multioperation
        /// </summary>
        private const int PosD = 3;

        /// <summary>
        /// Defines the fifth component of multioperation
        /// </summary>
        private const int PosE = 4;

        private const int PosF = 5;

        /// <summary>
        /// Get first component of multioperation
        /// </summary>
        public long A
        {
            get
            {
                return (_val >> OpSize * PosA) & OpMax;
            }
        }

        /// <summary>
        /// Get second component of multioperation
        /// </summary>
        public long B { get { return (_val >> OpSize * PosB) & OpMax; } }

        /// <summary>
        /// Get third component of multioperation
        /// </summary>
        public long C { get { return (_val >> OpSize * PosC) & OpMax; } }

        /// <summary>
        /// Get fourth component of multioperation
        /// </summary>
        public long D { get { return (_val >> OpSize * PosD) & OpMax; } }

        public long Ee { get { return (_val >> OpSize * PosE) & OpMax; } }

        public long F { get { return (_val >> OpSize * PosF) & OpMax; } }

        /// <summary>
        /// Decimal representation of multioperation
        /// </summary>
        public long Value { get { return _val; } }

        /// <summary>
        /// Multioperation tetha (empty)
        /// </summary>
        public static readonly Multioperation6 Zero = new Multioperation6(0, 0, 0, 0, 0, 0);

        /// <summary>
        /// Multioperation pi (full)
        /// </summary>
        public static readonly Multioperation6 All = new Multioperation6(63, 63, 63, 63, 63, 63);

        /// <summary>
        /// Multioperation epsilon (projection)
        /// </summary>
        public static readonly Multioperation6 E = new Multioperation6(1, 2, 4, 8, 16, 32);

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShefferMultioperationRank4.Multioperation6"/> class.
        /// Creates the decimal representation of multioperation
        /// </summary>
        /// <param name="a">The first component of multioperation.</param>
        /// <param name="b">The secong component of multioperation.</param>
        /// <param name="c">The third component of multioperation.</param>
        /// <param name="d">The fourth component of multioperation.</param>
        public Multioperation6(long a, long b, long c, long d, long e, long f)
        {
            _val =
                ((a & OpMax) << OpSize * PosA) |
                ((b & OpMax) << OpSize * PosB) |
                ((c & OpMax) << OpSize * PosC) |
                ((d & OpMax) << OpSize * PosD) |
                ((e & OpMax) << OpSize * PosE) |
                ((f & OpMax) << OpSize * PosF);
        }

        public Multioperation6(long value)
        {
            this._val = value;
        }

        #region Equality members
        public bool Equals(Multioperation6 other)
        {
            return _val == other._val;
        }

        public override bool Equals(object obj)
        {
            return obj is Multioperation6 && Equals((Multioperation6)obj);
        }

        public override int GetHashCode()
        {
            return (int)_val;
        }

        public static bool operator ==(Multioperation6 left, Multioperation6 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Multioperation6 left, Multioperation6 right)
        {
            return !left.Equals(right);
        }

        #endregion

        #region Overrides of ValueType

        public int CompareTo(Multioperation6 other)
        {
            return (int)other._val - (int)_val;
        }
        public override string ToString()
        {
            return "Op(" + A + ", " + B + ", " + C + ", " + D + ", " + Ee + ", " + F + ")";
        }

        #endregion

        private static long Translate(Multioperation6 op, long i)
        {
            var a = ((i & 1) >> PosA) * op.A;
            var b = ((i & 2) >> PosB) * op.B;
            var c = ((i & 4) >> PosC) * op.C;
            var d = ((i & 8) >> PosD) * op.D;
            var e = ((i & 16) >> PosE) * op.Ee;
            var f = ((i & 32) >> PosF) * op.F;
            return a | b | c | d | e | f;
        }

        /// <summary>
        /// Computes the superposition of <c>op1</c> and <c>op2</c>, yielding a new <see cref="T:ShefferMultioperationRank4.Multioperation6"/>.
        /// </summary>
        /// <param name="op1">The <see cref="ShefferMultioperationRank4.Multioperation6"/> to multiply.</param>
        /// <param name="op2">The <see cref="ShefferMultioperationRank4.Multioperation6"/> to multiply.</param>
        /// <returns>The <see cref="T:ShefferMultioperationRank4.Multioperation6"/> that is the <c>op1</c> * <c>op2</c>.</returns>
        public static Multioperation6 operator *(Multioperation6 op1, Multioperation6 op2)
        {
            return new Multioperation6(
                Translate(op1, op2.A),
                Translate(op1, op2.B),
                Translate(op1, op2.C),
                Translate(op1, op2.D),
                Translate(op1, op2.Ee),
                Translate(op1, op2.F)
                );
        }

        /// <summary>
        /// Computes the longersection of <c>op1</c> and <c>op2</c>, yielding a new <see cref="T:ShefferMultioperationRank4.Multioperation6"/>.
        /// </summary>
        /// <param name="op1">The <see cref="ShefferMultioperationRank4.Multioperation6"/> to multiply.</param>
        /// <param name="op2">The <see cref="ShefferMultioperationRank4.Multioperation6"/> to multiply.</param>
        /// <returns>The <see cref="T:ShefferMultioperationRank4.Multioperation6"/> that is the <c>op1</c> & <c>op2</c>.</returns>
        public static Multioperation6 operator &(Multioperation6 op1, Multioperation6 op2)
        {
            return new Multioperation6(op1._val & op2._val);
        }

        /// <summary>
        /// Computes the invertible (mu metaoperation) of <c>op</c>
        /// </summary>
        /// <param name="op">The <see cref="ShefferMultioperationRank4.Multioperation6"/> to multiply.</param>
        /// <returns>The <see cref="T:ShefferMultioperationRank4.Multioperation6"/> that is the <c>mu(op)</c> .</returns>
        public static Multioperation6 operator !(Multioperation6 op)
        {
            var reta = (op.A & 1) | (op.B & 1) << 1 | (op.C & 1) << 2 | (op.D & 1) << 3 | (op.Ee & 1) << 4 | (op.F & 1) << 5;
            var retb = (op.A & 2) >> 1 | (op.B & 2) | (op.C & 2) << 1 | (op.D & 2) << 2 | (op.Ee & 2) << 3 | (op.F & 2) << 4;
            var retc = (op.A & 4) >> 2 | (op.B & 4) >> 1 | (op.C & 4) | (op.D & 4) << 1 | (op.Ee & 4) << 2 | (op.F & 4) << 3;
            var retd = (op.A & 8) >> 3 | (op.B & 8) >> 2 | (op.C & 8) >> 1 | (op.D & 8) | (op.Ee & 8) << 1 | (op.F & 8) << 2;
            var rete = (op.A & 16) >> 4 | (op.B & 16) >> 3 | (op.C & 16) >> 2 | (op.D & 16) >> 1 | (op.Ee & 16) | (op.F & 16) << 1;
            var retf = (op.A & 32) >> 5 | (op.B & 32) >> 4 | (op.C & 32) >> 3 | (op.D & 32) >> 2 | (op.Ee & 32) >> 1 | (op.F & 32);

            return new Multioperation6(reta, retb, retc, retd, rete, retf);
        }
    }
}
