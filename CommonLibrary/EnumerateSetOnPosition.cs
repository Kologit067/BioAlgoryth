using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    //--------------------------------------------------------------------------------------
    // class EnumerateSetOnPosition
    //--------------------------------------------------------------------------------------
    public abstract class EnumerateSetOnPosition<T,R>
    {
        protected List<T> _fCurrentSet;		// текущий набор элементов
        protected int _fCurrentPosition;		// текущая глубина при обходе дерева
        protected T _fBreakElement = default(T);
        // statistics
        protected Stopwatch stopwatch;
        //--------------------------------------------------------------------------------------
        //protected long _fIterationCount;
        //public long IterationCount
        //{
        //    get
        //    {
        //        return _fIterationCount;
        //    }
        //}
        //--------------------------------------------------------------------------------------
        protected long _fDurationMilliSeconds;
        public long DurationMilliSeconds
        {
            get
            {
                return _fDurationMilliSeconds;
            }
        }
        //--------------------------------------------------------------------------------------
        protected long _fElapsedTicks;
        public long ElapsedTicks
        {
            get
            {
                return _fElapsedTicks;
            }
        }
        //--------------------------------------------------------------------------------------
        protected bool fOutQueryStop = false;			// 
        public bool IsComplete
        {
            get
            {
                return !fOutQueryStop;
            }
        }
        //protected long fCountTerminal;
        ////--------------------------------------------------------------------------------------
        //public long CountTerminal
        //{
        //    get
        //    {
        //        return fCountTerminal;
        //    }
        //}
        //protected long fUpdateOptcount;
        ////--------------------------------------------------------------------------------------
        //public long UpdateOptcount
        //{
        //    get
        //    {
        //        return fUpdateOptcount;
        //    }
        //}
        //protected long fElemenationCount;
        ////--------------------------------------------------------------------------------------
        //public long ElemenationCount
        //{
        //    get
        //    {
        //        return fElemenationCount;
        //    }
        //}
        //--------------------------------------------------------------------------------------
        public EnumerateSetOnPosition(int pCapacity)
        {
            _fCurrentSet = new List<T>(pCapacity);
            while (pCapacity-- > 0)
                _fCurrentSet.Add(default(T));
            fOutQueryStop = false;
        }
        //--------------------------------------------------------------------------------------
        public EnumerateSetOnPosition()
            : this(10)
        {
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// здесь происходит перебор всех возможных наборов 
        /// </summary>
        public void Execute()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            InitialData();
            while (_fCurrentPosition >= 0)
            {
                if (IsCompleteCondition())	// если выполненно условие
                {
                    if (MakeAction())
                        break;
                    Back();					// то возвращаемся назад
                }
                else if (!Forward())		// если не покрыт то вперед
                {
                    if (IsCompleteCondition())  // если выполненно условие
                        if (MakeAction())
                            break;
                    Back();				// если нельзя вперед то назад
                }
            }
            stopwatch.Stop();
            _fElapsedTicks = stopwatch.ElapsedTicks;
            _fDurationMilliSeconds = stopwatch.ElapsedMilliseconds;
            PostAction();
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// движение назад по дереву перебора
        /// когда движение вперед невозможно - исчерпаны все возможные 
        /// продолжения по даной ветке или когда достигнут набор 
        /// который удовлетрворяет условиям и движение вперед не нужно
        /// </summary>
        private void Back()
        {
            BackAction();
            // повторяем попытку найти продолжение по другой ветке
            // пока это возможно т.е. текущая позиция не вышла за пределы
            // диапозона
            while (_fCurrentPosition >= 0)
            {
                // удалить элемент в наборе - произвеcти действия необходимые
                // при удалениии (если есть в данной реализации)
                RemoveAction(_fCurrentSet[_fCurrentPosition]);
                // пробуем вместо удаленного элемента подставить следующий по прядку
                if (NextElement(_fCurrentPosition))
                {
                    AddAction(_fCurrentSet[_fCurrentPosition]);
                    return;
                }
                _fCurrentSet[_fCurrentPosition--] = _fBreakElement;
            }
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// добавить новый элемент (если возможно)
        /// </summary>
        /// <returns>TRUE - если была добавлен новый элемент
        /// FALSE - если элемент добавить не удалось</returns>
        private bool Forward()
        {
            ForwardAction();
            // создаем первый элемент для следующей позиции
            T lCandidat = FirstElement(_fCurrentPosition + 1);
            // если нет следующего элемента
            if (lCandidat.Equals(_fBreakElement))
                return false;       // то движение вперед невозможно возращаем FALSE	

            _fCurrentSet[++_fCurrentPosition] = lCandidat;
            // произвети действия необходимые при добавлении
            // (если есть в данной реализации)
            AddAction(_fCurrentSet[_fCurrentPosition]);
            return true;
        }
        //--------------------------------------------------------------------------------------
        protected void InitialData()
        {
            // текущая позиция - самое начало
            _fCurrentPosition = 0;
            // создаем первый элемент
            T item = InitialElement();
            // добавляем в нулевую позицию 
            _fCurrentSet[0] = item;
            // инициализация вспомогательных членов класса (если будут)
            SupplementInitial();
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// создать первый элемент - в каждой реализации будет по разному
        /// </summary>
        /// <returns></returns>
        protected abstract T InitialElement();
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// инициализация вспомогательных членов класса (если будут)
        /// </summary>
        protected abstract void SupplementInitial();
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// action after excecution
        /// </summary>
        protected abstract void PostAction();
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// произвети действия необходимые при удалениии
        /// (если есть в данной реализации)
        /// </summary>
        /// <param name="p"></param>
        protected abstract void RemoveAction(T p);
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// произвети действия необходимые при добавлении
        /// (если есть в данной реализации)
        /// </summary>
        /// <param name="p"></param>
        protected abstract void AddAction(T p);
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// проверить выполнено ли условие для текущего набора
        /// </summary>		
        protected abstract bool IsCompleteCondition();
        //--------------------------------------------------------------------------------------
        protected virtual void IterationAction()
        {
        }
        //--------------------------------------------------------------------------------------
        protected virtual void TerminalAction()
        {
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// произвести необходимые действия на наборе удовлетворяющем условиям
        /// </summary>		
        protected abstract bool MakeAction();
        //--------------------------------------------------------------------------------------
        protected abstract void ForwardAction();
        //--------------------------------------------------------------------------------------
        protected abstract void BackAction();
        //--------------------------------------------------------------------------------------
        protected abstract T FirstElement(int pPosition);
        //--------------------------------------------------------------------------------------
        protected abstract bool NextElement(int pPosition);
        //--------------------------------------------------------------------------------------
        protected virtual string ShowElementAsString(T pElement)
        {
            return pElement.ToString();
        }
        //--------------------------------------------------------------------------------------
        protected virtual string ShowElementAsShortString(T pElement)
        {
            return pElement.ToString();
        }
        //--------------------------------------------------------------------------------------
        protected virtual string ShowElementAsFullString(T pElement)
        {
            return pElement.ToString();
        }
        //--------------------------------------------------------------------------------------
        public virtual string ShowString
        {
            get
            {
                if (_fCurrentSet != null && _fCurrentSet.Count > 0)
                    return string.Join(",", _fCurrentSet.Select(i => ShowElementAsString(i)));
                return "Empty";
            }
        }
        //--------------------------------------------------------------------------------------
        public virtual string ShowShortString
        {
            get
            {
                if (_fCurrentSet != null && _fCurrentSet.Count > 0)
                    return string.Join(",", _fCurrentSet.Select(i => ShowElementAsShortString(i)));
                return "Empty";
            }
        }
        //--------------------------------------------------------------------------------------
        public virtual string ShowFullString
        {
            get
            {
                if (_fCurrentSet != null && _fCurrentSet.Count > 0)
                    return string.Join(",", _fCurrentSet.Select(i => ShowElementAsFullString(i)));
                return "Empty";
            }
        }
        //--------------------------------------------------------------------------------------
        public string CurrentSetAsString
        {
            get
            {
                if (_fCurrentSet != null && _fCurrentSet.Count > 0)
                    return string.Join(",", _fCurrentSet.Select(i => i.ToString()));
                return "Empty";
            }
        }
        //--------------------------------------------------------------------------------------
        public string RouteAsString
        {
            get
            {
                return ShowString;
            }
        }
        //-----------------------------------------------------------------------------------
        public virtual string OptimalRouteAsString
        {
            get
            {
                return "";
            }
        }
        //-----------------------------------------------------------------------------------
        public virtual string OutputPresentation
        {
            get
            {
                return "";
            }
        }
        //--------------------------------------------------------------------------------------
        public virtual R OptimalValue
        {
            get
            {
                return default(R);
            }
        }
        //--------------------------------------------------------------------------------------
    }
}
