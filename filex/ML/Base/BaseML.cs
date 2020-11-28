using System;
using System.Collections.Generic;
using System.Linq;

using filex.Parsers.Base;

namespace filex.ML.Base
{
    public class BaseML
    {
        protected readonly List<BaseParser> _parsers;

        public BaseML()
        {
            _parsers = GetType().Assembly.GetTypes().Where(a => a.BaseType == typeof(BaseParser) && !a.IsAbstract)
                .Select(b => (BaseParser)Activator.CreateInstance(b)).ToList();
        }
    }
}
