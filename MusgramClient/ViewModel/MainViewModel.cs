using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace MusgramClient.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        private Page cur_Page;
        public Page Cur_Page
        {
            get { return cur_Page; }
            set
            {
                cur_Page = value;
                RaisePropertyChanged();
            }
        }
        public MainViewModel()
        {
            Cur_Page = Page.Authentication;
            Messenger.Default.Register<string>(this, "Open Chat", (obj) => Cur_Page = Page.Chat);
            Messenger.Default.Register<string>(this, "Open Player", (obj) => Cur_Page = Page.Player);
            Messenger.Default.Register<string>(this, "Open Profile", (obj) => Cur_Page = Page.Profile);
            Messenger.Default.Register<string>(this, "Logout", (obj) => Cur_Page = Page.Authentication);
        }
        public enum Page
        {
            Authentication,
            Chat,
            Player,
            Profile
        }
    }
}