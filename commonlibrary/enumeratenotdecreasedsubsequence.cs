using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLibrary
{
    //--------------------------------------------------------------------------------------
    // class EnumerateNotDecreasedSubSequence 
    //--------------------------------------------------------------------------------------
    public class EnumerateNotDecreasedSubSequence : EnumerateSetOnPosition<int,int>
    {
        protected int _fSize;
        protected int _fLimit;
        protected int[] _fSource;
        //--------------------------------------------------------------------------------------
        public EnumerateNotDecreasedSubSequence(int pSize, int[] pSource)
            : base(pSize)
        {
            _fSize = pSize;
            _fLimit = pSource.Length;
            _fSource = pSource.OrderBy(s => s).ToArray();
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
            IterationAction();
            if (_fCurrentPosition >= _fSize - 1)
            {
                TerminalAction();
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
            if (pPosition == 0)
                return _fSource[0];
            return _fSource[_fCurrentSet[pPosition-1]+1];
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
        protected override void PostAction()
        {

        }
        //--------------------------------------------------------------------------------------
    }
}
