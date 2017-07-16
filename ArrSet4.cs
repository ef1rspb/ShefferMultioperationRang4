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
    class ArrSet4
        {
            private readonly int[] opFlags = new int[65536];
            private readonly Multioperation4[] opList = new Multioperation4[65536];

            private int _count = 0;

            public int Count { get { return _count; } }
            public int Add(Multioperation4 op)
            {
                var prev = Interlocked.CompareExchange(ref opFlags[op.Value], 1, 0);
                if (prev != 0) return prev;
                opList[_count] = op;
                Interlocked.Add(ref _count, 1 - prev);
                return prev;
            }

            public bool Contains(Multioperation4 op)
            {
                return opFlags[op.Value] == 1;
            }

            public Multioperation4 this[int i] { get { return opList[i]; } }

            /// <summary>
            /// Initializes a new instance of the <see cref="T:ShefferMultioperationRank4.ArrSet4"/> class.
            /// Creates an algebra with nullary multioperations (tetha, epsilon, pi) and op
            /// </summary>
            /// <param name="op">Multioperation op</param>
            public ArrSet4(Multioperation4 op)
            {
                Add(Multioperation4.Zero);
                Add(Multioperation4.E);
                Add(Multioperation4.All);
                Add(op);
            }

            /// <summary>
            /// Generate the whole algebra
            /// </summary>
            /// <returns>Number of multioperations in algebra</returns>
            /// <param name="op">Multioperation generator</param>
            public static int CheckOp4Arr(Multioperation4 op)
            {
                var set = new ArrSet4(op);
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
						if (op1 == Multioperation4.Zero) continue;
	                    for (int j = 0; j < set.Count; j++)
	                    {
	                            var op2 = set[j];
								// For multioperation tetha: (f * tetha)(x) = (tetha)
								// and (f intersection tetha)(x) = (tetha)
								// so we could skip this step too
								if (op2 == Multioperation4.Zero) continue;
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
        } 
}
