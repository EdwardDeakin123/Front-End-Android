using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DragAndDropDemo
{
    [Activity(Label = "Drag and Drop", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        TextView result;
		TextView result2;
		string color;

        protected override void OnCreate(Bundle bundle)
        {
            // Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

            // Get UI elements out of the layout
            result = FindViewById<TextView>(Resource.Id.result);
			result2 = FindViewById<TextView>(Resource.Id.result2);
            var button1 = FindViewById<Button>(Resource.Id.button1);
            button1.LongClick += Button1_LongClick;
            var button2 = FindViewById<Button>(Resource.Id.button2);
            button2.LongClick += Button2_LongClick;
			var dropZone1 = FindViewById<RelativeLayout>(Resource.Id.dropz);
			var dropZone2 = FindViewById<RelativeLayout>(Resource.Id.dropz1);


            // Attach event to drop zone
            dropZone1.Drag += DropZone_Drag;
			dropZone2.Drag += DropZone_Drag2;

            base.OnCreate(bundle);
        }
            
        void Button1_LongClick (object sender, View.LongClickEventArgs e)
        {
            // Generate clip data package to attach it to the drag
            var data = ClipData.NewPlainText("name", "Element 1");

            // Start dragging and pass data
            ((sender) as Button).StartDrag(data, new View.DragShadowBuilder(((sender) as Button)), null, 0);
        }

        void Button2_LongClick (object sender, View.LongClickEventArgs e)
        {
            // Generate clip data package to attach it to the drag
            var data = ClipData.NewPlainText("name", "Element 2");

            // Start dragging and pass data
            ((sender) as Button).StartDrag(data, new View.DragShadowBuilder(((sender) as Button)), null, 0);
            ((sender) as Button).StartDrag(data, new View.DragShadowBuilder(((sender) as Button)), null, 0);
        }

        void DropZone_Drag (object sender, View.DragEventArgs e)
        {
            // React on different dragging events
            var evt = e.Event;
            switch (evt.Action) 
            {
                case DragAction.Ended:  
                case DragAction.Started:
                    e.Handled = true;
                    break;                
                // Dragged element enters the drop zone
                case DragAction.Entered:                   
                    result.Text = "Drop it like it's hot!";
                    break;
                // Dragged element exits the drop zone
                case DragAction.Exited:                   
                    result.Text = "1";

                    break;
                // Dragged element has been dropped at the drop zone
                case DragAction.Drop:
                    // You can check if element may be dropped here
                    // If not do not set e.Handled to true
                    e.Handled = true;

                    // Try to get clip data
                    var data = e.Event.ClipData;
					if (data != null)

						result.Text = "1";
						
                        color = data.GetItemAt(0).Text;
					var image = FindViewById<ImageView>(Resource.Id.image2);

					if ( color == "Element 1"  )
					image.SetImageResource(Resource.Drawable.red);

					else if ( color == "Element 2")
						image.SetImageResource(Resource.Drawable.blue);
                    break;
            }
        }

		void DropZone_Drag2(object sender, View.DragEventArgs e)
		{
			// React on different dragging events
			var evt = e.Event;
			switch (evt.Action)
			{
				case DragAction.Ended:
				case DragAction.Started:
					e.Handled = true;
					break;
				// Dragged element enters the drop zone
				case DragAction.Entered:
					result2.Text = "Drop it like it's hot!";
					break;
				// Dragged element exits the drop zone
				case DragAction.Exited:
					result2.Text = "2";

					break;
				// Dragged element has been dropped at the drop zone
				case DragAction.Drop:
					// You can check if element may be dropped here
					// If not do not set e.Handled to true
					e.Handled = true;

					// Try to get clip data
					var data = e.Event.ClipData;
					if (data != null)
						result2.Text = data.GetItemAt(0).Text + " what is up";
					break;
			}
		}
    }
}    