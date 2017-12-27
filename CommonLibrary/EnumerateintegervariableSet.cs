using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    //--------------------------------------------------------------------------------------
    // class EnumerateintegervariableSet 
    //--------------------------------------------------------------------------------------
    public class EnumerateintegervariableSet : EnumerateSetOnPosition<int>
    {
        protected int _fSize;
        protected int[] _fLimits;
        protected int[] _fMinimumValues;
        //--------------------------------------------------------------------------------------
        public EnumerateintegervariableSet(int[] pLimits, int pLength, int[] pMinimumValues = null)
            : base(pLength)
        {
            _fSize = pLength;
            _fLimits = pLimits;
            _fMinimumValues = pMinimumValues;
            if (_fMinimumValues == null)
                _fMinimumValues = new int[pLimits.Length];
            _fBreakElement = -1;
            if (pLength != _fLimits.Length)
                throw new ArgumentException("Argument inconsistency: pLength must be equal pLimits.Length");
            if (pLength != _fMinimumValues.Length)
                throw new ArgumentException("Argument inconsistency: pLength must be equal pMinimumValues.Length");
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
            fIterationCount++;
            if (_fCurrentPosition >= _fSize - 1)
                return true;
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
            return _fMinimumValues[pPosition];
        }
        //--------------------------------------------------------------------------------------
        protected override bool NextElement(int pPosition)
        {
            if (_fCurrentSet[pPosition] >= _fLimits[pPosition])
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
