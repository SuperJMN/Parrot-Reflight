﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Reflight.Core
{
    internal class DataProvider
    {
        private readonly IList<string> headers;
        private readonly IList<IEnumerable<object>> source;

        public DataProvider(IEnumerable<List<object>> idDetailsData, IList<string> idDetailsHeaders)
        {
            source = idDetailsData.Transpose().ToList();
            headers = idDetailsHeaders;
        }

        public IEnumerable<TOut> GetData<TIn, TOut>(string header, Func<TIn, TOut> selector)
        {
            var indexOf = headers.IndexOf(header);

            if (indexOf == -1)
            {
                return Enumerable.Empty<TOut>();
            }

            var row = source[indexOf];
            return row.Select(o => selector((TIn)o));
        }
    }
}