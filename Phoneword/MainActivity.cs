using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;

namespace Phoneword
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
		static readonly List<string> phoneNumbers = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

			EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
			TextView translatedPhoneWord = FindViewById<TextView>(Resource.Id.TranslatedPhoneword);
			Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
			Button tanslationHistoryButton = FindViewById<Button>(Resource.Id.TranslationHistoryButton);

			string translatedNumber = string.Empty;
			translateButton.Click += (sender, e) =>
			{
				translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
				if (string.IsNullOrWhiteSpace(translatedNumber))
				{
					translatedPhoneWord.Text = string.Empty;
				}
				else
				{
					translatedPhoneWord.Text = translatedNumber;
					phoneNumbers.Add(translatedNumber);
					tanslationHistoryButton.Enabled = true;
				}
			};

			
			tanslationHistoryButton.Click += (sender, e) =>
			  {
				  var intent = new Intent(this, typeof(TranslationHistoryActivity));
				  intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
				  StartActivity(intent);
			  };

		}
	}
}