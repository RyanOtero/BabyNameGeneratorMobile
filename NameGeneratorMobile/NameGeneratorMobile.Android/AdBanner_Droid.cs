﻿using AdMob.CustomRenders;
using AdMob.Droid.CustomRenders;
using Android.Content;
using Android.Gms.Ads;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdBanner), typeof(AdBanner_Droid))]
namespace AdMob.Droid.CustomRenders {
    public class AdBanner_Droid : ViewRenderer {
        Context context;
        public AdBanner_Droid(Context _context) : base(_context) {
            context = _context;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<View> e) {
            base.OnElementChanged(e);
            if (e.OldElement == null) {
                var adView = new AdView(Context);
                switch ((Element as AdBanner).Size) {
                    case AdBanner.Sizes.Standardbanner:
                        adView.AdSize = AdSize.Banner;
                        break;
                    case AdBanner.Sizes.LargeBanner:
                        adView.AdSize = AdSize.LargeBanner;
                        break;
                    case AdBanner.Sizes.MediumRectangle:
                        adView.AdSize = AdSize.MediumRectangle;
                        break;
                    case AdBanner.Sizes.FullBanner:
                        adView.AdSize = AdSize.FullBanner;
                        break;
                    case AdBanner.Sizes.Leaderboard:
                        adView.AdSize = AdSize.Leaderboard;
                        break;
                    case AdBanner.Sizes.SmartBannerPortrait:
                        adView.AdSize = AdSize.SmartBanner;
                        break;
                    default:
                        adView.AdSize = AdSize.Banner;
                        break;
                }
                // TODO: change this id to your admob id  
                adView.AdUnitId = "ca-app-pub-2720261017079957/9845641562";
                var requestbuilder = new AdRequest.Builder();
                adView.LoadAd(requestbuilder.Build());
                SetNativeControl(adView);
            }
        }
    }
}