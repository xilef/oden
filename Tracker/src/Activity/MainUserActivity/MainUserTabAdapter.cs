using Android.App;
using Android.Support.V4.App;

namespace Tracker
{
    class MainUserTabAdapter : FragmentPagerAdapter
    {
        private MainUserTabFactory mTabFactory;
    
        public MainUserTabAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
            mTabFactory = new MainUserTabFactory();
        }

        public override int Count
        {
            get { return mTabFactory.Count; }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return mTabFactory.getFragment(position + 1);
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            MainUserTabFragment tab = mTabFactory.getFragment(position + 1);
            return new Java.Lang.String(tab.mTabTitle);
        }
    }
}