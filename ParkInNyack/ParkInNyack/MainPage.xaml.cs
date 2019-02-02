using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ParkInNyack
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            SetText();

            //Run the calculation every 30 seconds
            Device.StartTimer(TimeSpan.FromSeconds(30), () => {
                SetText();
                return true;
            });
        }

        public void Resume()
        {
            SetText();
        }

        private void SetText()
        {
            if(SafeToPark())
            {
                OutputText.Text = "You may park without paying.";
            }
            else
            {
                OutputText.Text = "You MUST PAY to park";
            }
        }

        private bool SafeToPark()
        {
            bool retval = true;
            DateTime now = DateTime.Now;
            DateTime midnight = DateTime.Parse("00:00");
            DateTime eleven5959PM = DateTime.Parse("23:59:59");
            DateTime sixAM = DateTime.Parse("06:00");
            DateTime tenAM = DateTime.Parse("10:00");
            DateTime sixPM = DateTime.Parse("18:00");
            DateTime elevenPM = DateTime.Parse("23:00");

            if (now.DayOfWeek != DayOfWeek.Sunday)
            {
                //Any day other than Sunday
                if (now.CompareTo(tenAM) >= 0 && now.CompareTo(sixPM) <= 0)
                {
                    //10AM to 6PM
                    retval = false;
                }
            }

            if (now.CompareTo(midnight) >= 0 && now.CompareTo(sixAM) <= 0)
            {
                //Midnight to 6AM every day
                retval = false;
            }
            else if(now.CompareTo(elevenPM) >= 0 && now.CompareTo(eleven5959PM) <= 0)
            {
                //11PM to Midnight every day
                retval = false;
            }

            return retval;
        }

        private void RefreshButton_Clicked(object sender, EventArgs e)
        {
            SetText();
        }
    }
}
