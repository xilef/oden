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
        private MainUserTabFragment[] mTabs = new MainUserTabFragment[mCount];

        public int Count { get { return mCount; } }

        public MainUserTabFactory()
        {
            mTabs[0] = MainUserFeed.newInstance(1);
            mTabs[1] = MainUserList.newInstance(2);
            mTabs[2] = MainUserStats.newInstance(3);
        }

        public MainUserTabFragment getFragment(int type)
        {
            return mTabs[type - 1];
        }
    }
}