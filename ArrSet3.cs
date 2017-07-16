using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShefferMultioperationRank4
{
    class ArrSet3
    {
        private readonly int[] opFlags = new int[512];
        private readonly Multioperation3[] opList = new Multioperation3[512];
        private int _count = 0;

        public int Count { get { return _count; } }

        public int Add(Multioperation3 op)
        {
            var prev = Interlocked.CompareExchange(ref opFlags[op.Value], 1, 0);
            if (prev != 0) return prev;
            opList[_count] = op;
            Interlocked.Add(ref _count, 1 - prev);
            return prev;
        }

        public bool Contains(Multioperation3 op)
        {
            return opFlags[op.Value] == 1;
        }

        public Multioperation3 this[int i] { get { return opList[i]; } }

        public ArrSet3(Multioperation3 op)
        {
            Add(Multioperation3.Zero);
            Add(Multioperation3.E);
            Add(Multioperation3.All);
            Add(op);
        }

        public static int CheckOp3Arr(Multioperation3 op)
        {
            var set = new ArrSet3(op);
            if (set.Count <= 3) return set.Count;

            var prevCount = 0;

            while (set.Count < 512 && prevCount != set.Count)
            {
                prevCount = set.Count;

                for (int i = 0; i < set.Count; i++)
                    set.Add(!set[i]);

                for (int i = 0; i < set.Count; i++)
                {
                    var op1 = set[i];
                    if (op1 == Multioperation3.Zero) continue;
                    for (int j = 0; j < set.Count; j++)
                    {
                        var op2 = set[j];
                        if (op2 == Multioperation3.Zero) continue;
                        var opSup = op1 * op2;
                        var opUn = op1 & op2;
                        set.Add(opSup);
                        set.Add(opUn);
                        if (op1 == op2) continue;
                        opSup = op2 * op1;
                        set.Add(opSup);
                    }
                }
            }

            return set.Count;
        }
    }

    
}
