using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class PermitationBase : EnumerateSetOnPosition<int, int>
    {
        private int[] _freePositions;
        protected int _fSize;
        protected int _fLimit;
        //--------------------------------------------------------------------------------------
        public PermitationBase(int pSize, int pLimit)
            : base(pSize)
        {
            _fBreakElement = -1;
            _fSize = pSize; 
            _fLimit = pLimit;
            _fCurrentSet = Enumerable.Repeat(0, pSize).ToList(); // new List<int>();
            _freePositions = new int[_fLimit];
            _freePositions[0] = 1;
        }
        //--------------------------------------------------------------------------------------
        protected override int FirstElement(int pPosition)
        {
            return NextElementFromStart( 0);
        }
        //--------------------------------------------------------------------------------------
        protected override bool NextElement(int pPosition)
        {
            int pos = NextElementFromStart( _fCurrentSet[pPosition] + 1);
            bool result = pos != _fBreakElement;
            if (result)
            _fCurrentSet[pPosition] = pos;
            return result;
        }
        //--------------------------------------------------------------------------------------
        private int NextElementFromStart(int pStart)
        {
            int i = pStart;
            while (i < _fLimit)
            {
                if (_freePositions[i] == 0)
                {
                    _freePositions[i] = 1;
                    return i;
                }
                i++;
            }
            return _fBreakElement;
        }
        //--------------------------------------------------------------------------------------
        protected override int InitialElement()
        {
            return 0;
        }
        //--------------------------------------------------------------------------------------
        protected override bool IsCompleteCondition()
        {
            if (_fCurrentPosition >= _fSize - 1)
            {
                return true;
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override bool MakeAction()
        {
            return true;
        }
        //--------------------------------------------------------------------------------------



        //--------------------------------------------------------------------------------------
        protected override void AddAction(int p)
        {
        }

        protected override void BackAction()
        {
        }

         protected override void ForwardAction()
        {
        }

        protected override void PostAction()
        {
        }

        protected override void RemoveAction(int p)
        {
            _freePositions[p] = 0;
        }

        protected override void SupplementInitial()
        {
        }
    }
}
