using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.System.UserProfile;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CustomTextBackGround
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        BitmapImage image = new BitmapImage();
        public MainPage()
        {
            this.InitializeComponent();
           
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                this.textblock.Text = "Picked photo: " + file.Name;
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
               
                image.SetSource(stream);
                imageView.Source = image;
            }
            else
            {
                this.textblock.Text = "Operation cancelled.";
            }
        }

        async private void setbg_Click(object sender, RoutedEventArgs e)
        {
            bool x = await SetWallpaperAsync("1.jpg");
        }

        async Task<bool> SetWallpaperAsync(string iFile)
        {
            bool success = false;
            if (UserProfilePersonalizationSettings.IsSupported())
            {
                var uri = new Uri(this.BaseUri, "C:\\Users\\Deepak\\Pictures\\" + iFile);
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
                UserProfilePersonalizationSettings profileSettings = UserProfilePersonalizationSettings.Current;
                success = await profileSettings.TrySetWallpaperImageAsync(file);
            }

            return success;
        }
    }
}
