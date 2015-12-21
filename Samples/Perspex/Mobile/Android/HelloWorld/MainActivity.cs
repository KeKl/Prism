using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Perspex;
using Perspex.Android.Platform.Specific;
using Perspex.Animation;
using Perspex.Controls;
using Perspex.Controls.Primitives;
using Perspex.Media;

namespace HelloWorld
{
    [Activity(Label = "HelloWorld", MainLauncher = true, Icon = "@drawable/icon", LaunchMode = LaunchMode.SingleInstance)]
    public class MainActivity : PerspexActivity
    {
        public MainActivity() : base(typeof (App))
        {

        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var app = new App
            {

            };

            app.Run();
        }
    }
}

