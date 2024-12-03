using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public class SourceManager : Singleton<SourceManager>
    {

        [Header("Sources")]
        public Source[] sources;

        /// <summary>
        /// get count of source with given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public float GetSource(string name)
        {
            if(CheckContains(name) == false)
            {
                return 0f;
            }

            float value = 0f;

            foreach(Source s in sources)
            {
                if(s.sourceName == name)
                {
                    value = s.currentValue;
                    break;
                }
            }

            return value;
        }

        /// <summary>
        /// add to the source
        /// 
        /// and call an update event
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        public void AddSource(string name, float amount)
        {
            foreach (Source s in sources)
            {
                if (s.sourceName == name)
                {
                    s.currentValue += amount;

                    SourceEvents.OnUpdatedSource?.Invoke(s);

                    break;
                }
            }
        }

        /// <summary>
        /// try spend source 
        /// 
        /// and if we are able spend, update event
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool TrySpendSource(string name, float amount)
        {
            bool canSpend = false;

            foreach (Source s in sources)
            {
                if (s.sourceName == name)
                {
                    if(s.currentValue >= amount)
                    {
                        s.currentValue -= amount;
                        canSpend = true;

                        SourceEvents.OnUpdatedSource?.Invoke(s);


                        break;
                    }
                    else
                    {
                        canSpend = false;
                        break;
                    }
                }
            }

            return canSpend;
        }

        /// <summary>
        /// check for the given source name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool CheckContains(string name)
        {
            bool contains = false;

            foreach(Source s in sources)
            {
                if(s.sourceName == name)
                {
                    contains = true;
                    break;
                }
            }

            if(contains == false)
            {
                Debug.LogWarning("Source with given name: - " + name + " - does not exist!");
            }

            return contains;
        }

    }
}