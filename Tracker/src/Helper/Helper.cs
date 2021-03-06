﻿using Android.Database;
using Android.Graphics;
using Android.OS;
using System;
using System.Net;
using Object = Java.Lang.Object;

namespace Tracker
{
    class Helper
    {
        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap image = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    image = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }
            return image;
        }
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

    public class MovieEntry: Object
    {
        public int MovieID { get; set; }
        public string Title { get; set; }

        public MovieEntry(ICursor cursor)
        {
            MovieID = cursor.GetInt(0);
            Title = cursor.GetString(1);
        }
    }
}