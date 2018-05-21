using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using ModernHttpClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace RTestAndroid
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{
        private HttpClient _httpClient;

        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

			FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClickAsync;

            _httpClient = new HttpClient(new NativeMessageHandler())
            {
                BaseAddress = new Uri("https://rtestxam.azurewebsites.net/Api/")
            };
            _httpClient.GetAsync($"Valu");
        }

		public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private async void FabOnClickAsync(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
           
            List<Contact> result = null;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            HttpResponseMessage response = await _httpClient.GetAsync($"Values");
            if (response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                         result = await Task.Run(
                          async () =>
                              JsonConvert.DeserializeObject<List<Contact>>(await response.Content.ReadAsStringAsync()));
                        watch.Stop();
                        break;
                }
            }
            if (result != null)
            {
                Snackbar.Make(view, $"{result.Count} items in {watch.ElapsedMilliseconds} ms.", Snackbar.LengthLong)
                              .SetAction("Action", (View.IOnClickListener)null).Show();
            }
          
        }
	}
    public class Contact
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
    }
}

