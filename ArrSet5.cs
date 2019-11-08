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
    class ArrSet5
    {
        private readonly int[] opFlags = new int[33554432];
        private readonly Multioperation5[] opList = new Multioperation5[33554432];

        private int _count = 0;

        public int Count { get { return _count; } }
        public int Add(Multioperation5 op)
        {
            var prev = Interlocked.CompareExchange(ref opFlags[op.Value], 1, 0);
            if (prev != 0) return prev;
            opList[_count] = op;
            Interlocked.Add(ref _count, 1 - prev);
            return prev;
        }

        public bool Contains(Multioperation5 op)
        {
            return opFlags[op.Value] == 1;
        }

        public Multioperation5 this[int i] { get { return opList[i]; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShefferMultioperationRank4.ArrSet5"/> class.
        /// Creates an algebra with nullary multioperations (tetha, epsilon, pi) and op
        /// </summary>
        /// <param name="op">Multioperation op</param>
        public ArrSet5(Multioperation5 op)
        {
            Add(Multioperation5.Zero);
            Add(Multioperation5.E);
            Add(Multioperation5.All);
            Add(op);
        }

        /// <summary>
        /// Generate the whole algebra
        /// </summary>
        /// <returns>Number of multioperations in algebra</returns>
        /// <param name="op">Multioperation generator</param>
        public static int CheckOp4Arr(Multioperation5 op)
        {
            var set = new ArrSet5(op);
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
                    if (op1 == Multioperation5.Zero) continue;
                    for (int j = 0; j < set.Count; j++)
                    {
                        var op2 = set[j];
                        // For multioperation tetha: (f * tetha)(x) = (tetha)
                        // and (f intersection tetha)(x) = (tetha)
                        // so we could skip this step too
                        if (op2 == Multioperation5.Zero) continue;
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

        public static bool IsConst(Multioperation5 op)
        {
            return op == Multioperation5.All || op == Multioperation5.E || op == Multioperation5.Zero;
        }

        public static bool IsFirstType(Multioperation5 op)
        {
            if (ArrSet5.IsConst(op) == true)
            {
                return false;
            }
            var first = op & Multioperation5.E;
            if (first != Multioperation5.E)
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

        public static bool IsSecondType(Multioperation5 op)
        {
            if (ArrSet5.IsConst(op) == true)
            {
                return false;
            }
            var first = op & Multioperation5.E;
            if (first != Multioperation5.E)
            {
                return false;
            }
            var mu = !op;
            if (mu != op)
            {
                return false;
            }
            var op2 = op * op;
            if (op2 != Multioperation5.All)
            {
                return false;
            }
            return true;
        }

        public static bool IsThirdType(Multioperation5 op)
        {
            if (ArrSet5.IsConst(op) == true)
            {
                return false;
            }
            var first = op & Multioperation5.E;
            if (first != Multioperation5.E)
            {
                return false;
            }
            var second = (!op) & op;
            if (second != Multioperation5.E)
            {
                return false;
            }
            var third1 = op * (!op);
            var third2 = (!op) * op;
            if (third1 != Multioperation5.All || third2 != Multioperation5.All)
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

        public static bool IsFourthType(Multioperation5 op)
        {
            if (ArrSet5.IsConst(op) == true)
            {
                return false;
            }
            var first = op & Multioperation5.E;
            if (first != Multioperation5.E)
            {
                return false;
            }
            var second = (!op) & op;
            if (second != Multioperation5.E)
            {
                return false;
            }
            var third1 = op * (!op);
            var third2 = (!op) * op;
            if (third1 != Multioperation5.All || third2 != Multioperation5.All)
            {
                return false;
            }

            var fourth = op * op;
            if (fourth != Multioperation5.All)
            {
                return false;
            }
            return true;
        }

        public static bool IsFifthType(Multioperation5 op)
        {
            if (ArrSet5.IsConst(op) == true)
            {
                return false;
            }
            var first = op & Multioperation5.E;
            if (first != Multioperation5.Zero)
            {
                return false;
            }
            if (!op != op)
            {
                return false;
            }
            var third = op * op;
            if (third != Multioperation5.All)
            {
                return false;
            }
            return true;
        }
    }
}
