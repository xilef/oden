using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Tracker
{
    class MainUserTabFactory
    {
        private const int mCount = 3;

        public int Count { get { return mCount; } }

        public MainUserTabFactory()
        {
        }

        public MainUserTabFragment getFragment(int type)
        {
            switch(type)
            {
                case 1:
                    return MainUserFeed.newInstance(type);
                case 2:
                    return MainUserList.newInstance(type);
                case 3:
                    return MainUserStats.newInstance(type);
                default:
                    return MainUserTabFragment.newInstance(type);
            }
        }
    }
}