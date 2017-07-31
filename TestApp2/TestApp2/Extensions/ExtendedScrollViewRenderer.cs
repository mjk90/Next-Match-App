using Android.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ScrollView), typeof(TestApp2.Extensions.ExtendedScrollViewRenderer))]
namespace TestApp2.Extensions
{
    public class ExtendedScrollViewRenderer : Xamarin.Forms.Platform.Android.ScrollViewRenderer
    {
        public bool Scrollable = true;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;

            e.NewElement.PropertyChanged += OnElementPropertyChanged;

        }

        protected void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ChildCount > 0)
            {
                GetChildAt(0).HorizontalScrollBarEnabled = false;
                GetChildAt(0).VerticalScrollBarEnabled = false;
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            switch (e.Action)
            {
                case MotionEventActions.Down:
                    // if we can scroll pass the event to the superclass
                    if (Scrollable) return base.OnTouchEvent(e);
                    // only continue to handle the touch event if scrolling enabled
                    return Scrollable; // mScrollable is always false at this point
                default:
                    return base.OnTouchEvent(e);
            }
        }

        public override bool OnInterceptTouchEvent(MotionEvent e)
        {
            // Don't do anything with intercepted touch events if 
            // we are not scrollable
            if (!Scrollable) return true;
            else return base.OnInterceptTouchEvent(e);
        }
    }
}
