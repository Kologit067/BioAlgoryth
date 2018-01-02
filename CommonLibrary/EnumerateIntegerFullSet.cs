using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    //--------------------------------------------------------------------------------------
    // class EnumerateIntegerFullSet 
    //--------------------------------------------------------------------------------------
    public class EnumerateIntegerFullSet : EnumerateSetOnPosition<int,int>
    {
        protected int _fSize;
        protected int _fLimit;
        protected int _fMinimumValue;
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerFullSet(int pLimit, int pLength, int pMinimumValue = 0)
            : base(pLength)
        {
            _fSize = pLength;
            _fLimit = pLimit;
            _fMinimumValue = pMinimumValue;
            _fBreakElement = -1;
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// создать первый элемент - в каждой реализации будет по разному
        /// </summary>
        /// <returns></returns>
        protected override int InitialElement()
        {
            return FirstElement(0);
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// инициализация вспомогательных членов класса (если будут)
        /// </summary>
        protected override void SupplementInitial()
        {
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// произвести действия необходимые при удалениии
        /// (если есть в данной реализации)
        /// </summary>
        /// <param name="p"></param>
        protected override void RemoveAction(int p)
        {
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// произвети действия необходимые при добавлении
        /// (если есть в данной реализации)
        /// </summary>
        /// <param name="p"></param>
        protected override void AddAction(int p)
        {
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// проверить выполнено ли условие для текущего набора
        /// </summary>		
        protected override bool IsCompleteCondition()
        {
//            _fIterationCount++;
            if (_fCurrentPosition >= _fSize - 1)
            {
//                fCountTerminal++;
                return true;
            }
            return false;
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// произвести необходимые действия на наборе удовлетворяющем условиям
        /// </summary>		
        protected override bool MakeAction()
        {
            return false;
        }
        //--------------------------------------------------------------------------------------
        protected override void ForwardAction()
        {
        }
        //--------------------------------------------------------------------------------------
        protected override int FirstElement(int pPosition)
        {
            return _fMinimumValue;
        }
        //--------------------------------------------------------------------------------------
        protected override bool NextElement(int pPosition)
        {
            if (_fCurrentSet[pPosition] >= _fLimit)
                return false;
            _fCurrentSet[pPosition]++;
            return true;
        }
        //--------------------------------------------------------------------------------------
        protected override void BackAction()
        {

        }
        //--------------------------------------------------------------------------------------
        protected override string ShowElementAsString(int pElement)
        {
            return ShowString;
        }
        //--------------------------------------------------------------------------------------
        protected override string ShowElementAsShortString(int pElement)
        {
            return ShowShortString;
        }
        //--------------------------------------------------------------------------------------
        protected override string ShowElementAsFullString(int pElement)
        {
            return ShowFullString;
        }
        //--------------------------------------------------------------------------------------
    }
}
