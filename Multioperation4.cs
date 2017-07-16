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
    public class Multioperation4 : IComparable<Multioperation4>
    {
        /// <summary>
        /// Decimal representation of multioperation
        /// </summary>
        private readonly int _val;

        /// <summary>
        /// Decimal representation of full multioperation pi
        /// </summary>
        private const int OpMax = 15;

        /// <summary>
        /// Rang of algebra (cardinality of set A)
        /// </summary>
        private const int OpSize = 4;

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
        /// Get first component of multioperation
        /// </summary>
        public int A
        {
            get
            {
                return (_val >> OpSize * PosA) & OpMax;
            }
        }

        /// <summary>
        /// Get second component of multioperation
        /// </summary>
        public int B { get { return (_val >> OpSize * PosB) & OpMax; } }

        /// <summary>
        /// Get third component of multioperation
        /// </summary>
        public int C { get { return (_val >> OpSize * PosC) & OpMax; } }

        /// <summary>
        /// Get fourth component of multioperation
        /// </summary>
        public int D { get { return (_val >> OpSize * PosD) & OpMax; } }

        /// <summary>
        /// Decimal representation of multioperation
        /// </summary>
        public int Value { get { return _val; } }

        /// <summary>
        /// Multioperation tetha (empty)
        /// </summary>
        public static readonly Multioperation4 Zero = new Multioperation4(0, 0, 0, 0);

        /// <summary>
        /// Multioperation pi (full)
        /// </summary>
        public static readonly Multioperation4 All = new Multioperation4(15, 15, 15, 15);

        /// <summary>
        /// Multioperation epsilon (projection)
        /// </summary>
        public static readonly Multioperation4 E = new Multioperation4(1, 2, 4, 8);

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ShefferMultioperationRank4.Multioperation4"/> class.
        /// Creates the decimal representation of multioperation
		/// </summary>
		/// <param name="a">The first component of multioperation.</param>
		/// <param name="b">The secong component of multioperation.</param>
		/// <param name="c">The third component of multioperation.</param>
		/// <param name="d">The fourth component of multioperation.</param>
		public Multioperation4(int a, int b, int c, int d)
        {
            _val =
                ((a & OpMax) << OpSize * PosA) |
                ((b & OpMax) << OpSize * PosB) |
                ((c & OpMax) << OpSize * PosC) |
                ((d & OpMax) << OpSize * PosD);
        }

        public Multioperation4(int value)
        {
            this._val = value;
        }

        #region Equality members
        public bool Equals(Multioperation4 other)
        {
            return _val == other._val;
        }

        public override bool Equals(object obj)
        {
            return obj is Multioperation4 && Equals((Multioperation4)obj);
        }

        public override int GetHashCode()
        {
            return _val;
        }

        public static bool operator ==(Multioperation4 left, Multioperation4 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Multioperation4 left, Multioperation4 right)
        {
            return !left.Equals(right);
        }

        #endregion

        #region Overrides of ValueType

        public int CompareTo(Multioperation4 other)
        {
            return other._val - _val;
        }
        public override string ToString()
        {
            return "Op(" + A + ", " + B + ", " + C + ", " + D + ")";
        }

        #endregion

        private static int Translate(Multioperation4 op, int i)
        {
            var a = ((i & 1) >> PosA) * op.A;
            var b = ((i & 2) >> PosB) * op.B;
            var c = ((i & 4) >> PosC) * op.C;
            var d = ((i & 8) >> PosD) * op.D;
            return a | b | c | d;
        }

        /// <summary>
        /// Computes the superposition of <c>op1</c> and <c>op2</c>, yielding a new <see cref="T:ShefferMultioperationRank4.Multioperation4"/>.
        /// </summary>
        /// <param name="op1">The <see cref="ShefferMultioperationRank4.Multioperation4"/> to multiply.</param>
        /// <param name="op2">The <see cref="ShefferMultioperationRank4.Multioperation4"/> to multiply.</param>
        /// <returns>The <see cref="T:ShefferMultioperationRank4.Multioperation4"/> that is the <c>op1</c> * <c>op2</c>.</returns>
        public static Multioperation4 operator *(Multioperation4 op1, Multioperation4 op2)
        {
            return new Multioperation4(Translate(op1, op2.A), Translate(op1, op2.B), Translate(op1, op2.C), Translate(op1, op2.D));
        }

		/// <summary>
		/// Computes the intersection of <c>op1</c> and <c>op2</c>, yielding a new <see cref="T:ShefferMultioperationRank4.Multioperation4"/>.
		/// </summary>
		/// <param name="op1">The <see cref="ShefferMultioperationRank4.Multioperation4"/> to multiply.</param>
		/// <param name="op2">The <see cref="ShefferMultioperationRank4.Multioperation4"/> to multiply.</param>
		/// <returns>The <see cref="T:ShefferMultioperationRank4.Multioperation4"/> that is the <c>op1</c> & <c>op2</c>.</returns>
		public static Multioperation4 operator &(Multioperation4 op1, Multioperation4 op2)
        {
            return new Multioperation4(op1._val & op2._val);
        }

		/// <summary>
        /// Computes the invertible (mu metaoperation) of <c>op</c>
		/// </summary>
		/// <param name="op">The <see cref="ShefferMultioperationRank4.Multioperation4"/> to multiply.</param>
		/// <returns>The <see cref="T:ShefferMultioperationRank4.Multioperation4"/> that is the <c>mu(op)</c> .</returns>
		public static Multioperation4 operator !(Multioperation4 op)
        {
            var reta = (op.A & 1) | (op.B & 1) << 1 | (op.C & 1) << 2 | (op.D & 1) << 3;
            var retb = (op.A & 2) >> 1 | (op.B & 2) | (op.C & 2) << 1 | (op.D & 2) << 2;
            var retc = (op.A & 4) >> 2 | (op.B & 4) >> 1 | (op.C & 4) | (op.D & 4) << 1;
            var retd = (op.A & 8) >> 3 | (op.B & 8) >> 2 | (op.C & 8) >> 1 | (op.D & 8);

            return new Multioperation4(reta, retb, retc, retd);
        }
    }
}
