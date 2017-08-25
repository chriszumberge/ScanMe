using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace ScanMe
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ScanButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var scanOptions = new MobileBarcodeScanningOptions
                {
                    AutoRotate = false,
                    UseFrontCameraIfAvailable = false,
                    TryHarder = true,
                    PossibleFormats = new List<ZXing.BarcodeFormat>
                    {
                        ZXing.BarcodeFormat.QR_CODE
                    }
                };

                var scanPage = new ZXingScannerPage(scanOptions)
                {
                    DefaultOverlayTopText = "Align the QR code within the frame",
                    DefaultOverlayBottomText = String.Empty,
                    DefaultOverlayShowFlashButton = true
                };

                scanPage.OnScanResult += (ZXing.Result result) =>
                {
                    // Stop scanning
                    scanPage.IsScanning = false;

                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread(async () =>
                       {
                           await Navigation.PopAsync();
                           lblScanResult.Text = result.Text;
                       });
                };

                // Navigate to scanner page
                await Navigation.PushAsync(scanPage);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
