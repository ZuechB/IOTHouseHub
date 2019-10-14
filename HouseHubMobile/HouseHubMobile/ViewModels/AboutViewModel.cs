using Authsome;
using System.Windows.Input;

using Xamarin.Forms;

namespace HouseHubMobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Welcome";

            UnlockCommand = new Command(async () => {
                var authsomeService = new AuthsomeService();
                await authsomeService.PostAsync<object>("http://[yourserver]:84/api/Message", new IOTDevice.Models.Notification()
                {
                    //DeviceId = 10,
                    Message = "Open the door!",
                    ShouldOpen = true
                });
            });



            LockCommand = new Command(async () => {
                var authsomeService = new AuthsomeService();
                await authsomeService.PostAsync<object>("http://[yourserver]:84/api/Message", new IOTDevice.Models.Notification()
                {
                    //DeviceId = 10,
                    Message = "Lock the door!",
                    ShouldOpen = false
                });
            });




        }

        public ICommand UnlockCommand { get; }
        public ICommand LockCommand { get; }
    }
}