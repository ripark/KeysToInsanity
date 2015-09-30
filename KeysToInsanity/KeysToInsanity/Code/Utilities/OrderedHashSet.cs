﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace KeysToInsanity.Code.Utilities
{
    // Easy OrderedHashSet
    class OrderedHashSet<T> : KeyedCollection<T, T>
    {
        protected override T GetKeyForItem(T item)
        {
            return item;
        }
    }
}
