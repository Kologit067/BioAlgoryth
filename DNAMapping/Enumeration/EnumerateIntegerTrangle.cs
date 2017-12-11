using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNAMapping.Enumeration
{
    public class EnumerateIntegerTrangle : EnumerateSetOnPosition<int>
    {
        protected int fSize;
        protected int _fLimit;
        //--------------------------------------------------------------------------------------
        public EnumerateIntegerTrangle(int pLimit, int pLength)
            : base(pLength)
        {
            fSize = pLength;
            _fLimit = pLimit;

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
            if (fCurrentPosition >= fSize - 1)
                return true;
            return false;
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// произвести необходимые действия на наборе удовлетворяющем условиям
        /// </summary>		
        protected override void MakeAction()
        {
        }
        //--------------------------------------------------------------------------------------
        protected override void ForwardAction()
        {
        }
        //--------------------------------------------------------------------------------------
        protected override int FirstElement(int pPosition)
        {
            return 0;
        }
        //--------------------------------------------------------------------------------------
        protected override bool NextElement(int pPosition)
        {
            if (fCurrentSet[pPosition] >= _fLimit)
                return false;
            fCurrentSet[pPosition]++;
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
