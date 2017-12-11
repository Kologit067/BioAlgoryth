﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNAMapping
{
    //--------------------------------------------------------------------------------------
    // class EnumerateSetOnPosition
    //--------------------------------------------------------------------------------------
    public abstract class EnumerateSetOnPosition<T>
    {
        protected List<T> fCurrentSet;		// текущий набор элементов
        protected int fCurrentPosition;		// текущая глубина при обходе дерева
        protected T fBreakElement = default(T);
        //--------------------------------------------------------------------------------------
        public EnumerateSetOnPosition(int pCapacity)
        {
            fCurrentSet = new List<T>(pCapacity);
            while (pCapacity-- > 0)
                fCurrentSet.Add(default(T));
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
            InitialData();
            while (fCurrentPosition >= 0)
            {
                if (IsCompleteCondition())	// если выполненно условие
                {
                    MakeAction();
                    Back();					// то возвращаемся назад
                }
                else if (!Forward())		// если не покрыт то вперед
                {
                    if (IsCompleteCondition())	// если выполненно условие
                        MakeAction();
                    Back();				// если нельзя вперед то назад
                }
            }
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
            while (fCurrentPosition >= 0)
            {
                // удалить элемент в наборе - произвеcти действия необходимые
                // при удалениии (если есть в данной реализации)
                RemoveAction(fCurrentSet[fCurrentPosition]);
                // пробуем вместо удаленного элемента подставить следующий по прядку
                if (NextElement(fCurrentPosition))
                {
                    AddAction(fCurrentSet[fCurrentPosition]);
                    return;
                    //                        }
                    //                    } while (fCurrentSet[fCurrentPosition].Next());
                }
                fCurrentSet[fCurrentPosition--] = fBreakElement;
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
            T lCandidat = FirstElement(fCurrentPosition + 1);
            // если нет следующего элемента
            if (lCandidat.Equals(fBreakElement))
                return false;       // то движение вперед невозможно возращаем FALSE	

            // если следующий элемент существует
            //            do
            //            {
            //                if (TestAddition(lCandidat))
            //                {
            // если продвинулись вперед добавляем в набор 
            //            fCurrentSet.Add(lCandidat);
            //            fCurrentPosition++;		// на одну позицию вперед (вниз по дереву)
            fCurrentSet[++fCurrentPosition] = lCandidat;
            // произвети действия необходимые при добавлении
            // (если есть в данной реализации)
            AddAction(fCurrentSet[fCurrentPosition]);
            return true;
            //                }
            //            } while (lCandidat.Next());


            //            return false;
        }
        //--------------------------------------------------------------------------------------
        protected void InitialData()
        {
            // текущая позиция - самое начало
            fCurrentPosition = 0;
            // создаем первый элемент
            T item = InitialElement();
            // добавляем в нулевую позицию 
            fCurrentSet[0] = item;
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
        /// <summary>
        /// произвести необходимые действия на наборе удовлетворяющем условиям
        /// </summary>		
        protected abstract void MakeAction();
        //--------------------------------------------------------------------------------------
        protected abstract void ForwardAction();
        //--------------------------------------------------------------------------------------
        protected abstract void BackAction();
        //--------------------------------------------------------------------------------------
        protected abstract T FirstElement(int pPosition);
        //--------------------------------------------------------------------------------------
        protected abstract bool NextElement(int pPosition);
        //--------------------------------------------------------------------------------------
        protected abstract string ShowElementAsString(T pElement);
        //--------------------------------------------------------------------------------------
        protected abstract string ShowElementAsShortString(T pElement);
        //--------------------------------------------------------------------------------------
        protected abstract string ShowElementAsFullString(T pElement);
        //--------------------------------------------------------------------------------------
        public virtual string ShowString
        {
            get
            {
                string rr = string.Empty;
                for (int i = 0; i < fCurrentSet.Count; i++)
                    rr += ShowElementAsString(fCurrentSet[i]) + "  *  ";
                return rr;
            }
        }
        //--------------------------------------------------------------------------------------
        public virtual string ShowShortString
        {
            get
            {
                string rr = string.Empty;
                for (int i = 0; i < fCurrentSet.Count; i++)
                    rr += ShowElementAsShortString(fCurrentSet[i]) + " # ";
                return rr;
            }
        }
        //--------------------------------------------------------------------------------------
        public virtual string ShowFullString
        {
            get
            {
                string rr = string.Empty;
                for (int i = 0; i < fCurrentSet.Count; i++)
                    rr += ShowElementAsFullString(fCurrentSet[i]) + " # ";
                return rr;
            }
        }
        //--------------------------------------------------------------------------------------
    }
}
