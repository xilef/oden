using Android.OS;
using System;

using Object = Java.Lang.Object;

namespace Tracker
{
    class Helper
    {
    }

    public class GenericParcelableCreator<T> : Object, IParcelableCreator
        where T : Object, new()
    {
        private readonly Func<Parcel, T> _createFunc;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParcelableDemo.GenericParcelableCreator`1"/> class.
        /// </summary>
        /// <param name='createFromParcelFunc'>
        /// Func that creates an instance of T, populated with the values from the parcel parameter
        /// </param>
        public GenericParcelableCreator(Func<Parcel, T> createFromParcelFunc)
        {
            _createFunc = createFromParcelFunc;
        }

        #region IParcelableCreator Implementation

        public Java.Lang.Object CreateFromParcel(Parcel source)
        {
            return _createFunc(source);
        }

        public Java.Lang.Object[] NewArray(int size)
        {
            return new T[size];
        }

        #endregion
    }
}