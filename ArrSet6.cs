using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShefferMultioperationRank4
{
    /// <summary>
    /// Represent algebra if unary multioperations
    /// </summary>
    class ArrSet6
    {
        private readonly int[] opFlags = new int[1];
        private readonly Multioperation6[] opList = new Multioperation6[1];

        private int _count = 0;

        public int Count { get { return _count; } }
        public int Add(Multioperation6 op)
        {
            var prev = Interlocked.CompareExchange(ref opFlags[op.Value], 1, 0);
            if (prev != 0) return prev;
            opList[_count] = op;
            Interlocked.Add(ref _count, 1 - prev);
            return prev;
        }

        public bool Contains(Multioperation6 op)
        {
            return opFlags[op.Value] == 1;
        }

        public Multioperation6 this[int i] { get { return opList[i]; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShefferMultioperationRank4.ArrSet6"/> class.
        /// Creates an algebra with nullary multioperations (tetha, epsilon, pi) and op
        /// </summary>
        /// <param name="op">Multioperation op</param>
        public ArrSet6(Multioperation6 op)
        {
            Add(Multioperation6.Zero);
            Add(Multioperation6.E);
            Add(Multioperation6.All);
            Add(op);
        }

        /// <summary>
        /// Generate the whole algebra
        /// </summary>
        /// <returns>Number of multioperations in algebra</returns>
        /// <param name="op">Multioperation generator</param>
        public static int CheckOp4Arr(Multioperation6 op)
        {
            var set = new ArrSet6(op);
            if (set.Count <= 3) return set.Count;

            var prevCount = 0;

            // generate new multioperation while it is possible
            while (set.Count < 65536 && prevCount != set.Count)
            {
                prevCount = set.Count;

                // add mu(f) multioperations
                for (int i = 0; i < set.Count; i++)
                    set.Add(!set[i]);

                for (int i = 0; i < set.Count; i++)
                {
                    var op1 = set[i];

                    // For multioperation tetha: (tetha * x)(x) = (tetha)               
                    // and (tetha intersection f)(x) = (tetha)
                    // so we could skip this step
                    if (op1 == Multioperation6.Zero) continue;
                    for (int j = 0; j < set.Count; j++)
                    {
                        var op2 = set[j];
                        // For multioperation tetha: (f * tetha)(x) = (tetha)
                        // and (f intersection tetha)(x) = (tetha)
                        // so we could skip this step too
                        if (op2 == Multioperation6.Zero) continue;
                        // (f * g)(x)
                        var opSup = op1 * op2;
                        // (f intersection g)(x)
                        var opUn = op1 & op2;
                        set.Add(opSup);
                        set.Add(opUn);
                        if (op1 == op2) continue;
                        // (g * f)(x)
                        opSup = op2 * op1;
                        set.Add(opSup);
                    }
                }
            }
            return set.Count;
        }

        public static bool IsConst(Multioperation6 op)
        {
            return op == Multioperation6.All || op == Multioperation6.E || op == Multioperation6.Zero;
        }

        public static bool IsFirstType(Multioperation6 op)
        {
            if (ArrSet6.IsConst(op) == true)
            {
                return false;
            }
            var first = op & Multioperation6.E;
            if (first != Multioperation6.E)
            {
                return false;
            }
            var mu = !op;
            if (mu != op)
            {
                return false;
            }
            var op2 = op * op;
            if (op2 != op)
            {
                return false;
            }
            return true;
        }

        public static bool IsSecondType(Multioperation6 op)
        {
            if (ArrSet6.IsConst(op) == true)
            {
                return false;
            }
            var first = op & Multioperation6.E;
            if (first != Multioperation6.E)
            {
                return false;
            }
            var mu = !op;
            if (mu != op)
            {
                return false;
            }
            var op2 = op * op;
            if (op2 != Multioperation6.All)
            {
                return false;
            }
            return true;
        }

        public static bool IsThirdType(Multioperation6 op)
        {
            if (ArrSet6.IsConst(op) == true)
            {
                return false;
            }
            var first = op & Multioperation6.E;
            if (first != Multioperation6.E)
            {
                return false;
            }
            var second = (!op) & op;
            if (second != Multioperation6.E)
            {
                return false;
            }
            var third1 = op * (!op);
            var third2 = (!op) * op;
            if (third1 != Multioperation6.All || third2 != Multioperation6.All)
            {
                return false;
            }

            var fourth = op * op;
            if (fourth != op)
            {
                return false;
            }
            return true;
        }

        public static bool IsFourthType(Multioperation6 op)
        {
            if (ArrSet6.IsConst(op) == true)
            {
                return false;
            }
            var first = op & Multioperation6.E;
            if (first != Multioperation6.E)
            {
                return false;
            }
            var second = (!op) & op;
            if (second != Multioperation6.E)
            {
                return false;
            }
            var third1 = op * (!op);
            var third2 = (!op) * op;
            if (third1 != Multioperation6.All || third2 != Multioperation6.All)
            {
                return false;
            }

            var fourth = op * op;
            if (fourth != Multioperation6.All)
            {
                return false;
            }
            return true;
        }

        public static bool IsFifthType(Multioperation6 op)
        {
            if (ArrSet6.IsConst(op) == true)
            {
                return false;
            }
            var first = op & Multioperation6.E;
            if (first != Multioperation6.Zero)
            {
                return false;
            }
            if (!op != op)
            {
                return false;
            }
            var third = op * op;
            if (third != Multioperation6.All)
            {
                return false;
            }
            return true;
        }
    }
}
