using Android.App;

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
                    return MainUserFeed.NewInstance(type);
                case 2:
                    return MainUserList.NewInstance(type);
                case 3:
                    return MainUserStats.NewInstance(type);
                default:
                    return MainUserTabFragment.newInstance(type);
            }
        }
    }
}