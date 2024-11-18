using System;
using Microsoft.Maui.Controls;
using System.Globalization;

namespace app2
{
    public partial class RenkSecici : ContentPage
    {
        public RenkSecici()
        {
            InitializeComponent();

            RedSlider.Value = 0;
            GreenSlider.Value = 0;
            BlueSlider.Value = 0;

            
            UpdateColorPreview();
        }

        
        private void OnColorSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            
            RedValueLabel.Text = ((int)RedSlider.Value).ToString();
            GreenValueLabel.Text = ((int)GreenSlider.Value).ToString();
            BlueValueLabel.Text = ((int)BlueSlider.Value).ToString();

            
            UpdateColorPreview();
        }

        
        private void UpdateColorPreview()
        {
            int red = (int)RedSlider.Value;
            int green = (int)GreenSlider.Value;
            int blue = (int)BlueSlider.Value;

            Console.WriteLine($"Red: {red}, Green: {green}, Blue: {blue}");

            Color color = Color.FromRgb(red, green, blue);

            ColorPreviewBox.Color = color; 

            HexCodeEntry.Text = $"#{red:X2}{green:X2}{blue:X2}";
        }



        private async void OnCopyHexCodeClicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(HexCodeEntry.Text);
            await DisplayAlert("Kopyalandı", "Hex kod panoya kopyalandı!", "Tamam");
        }

        private void OnRandomColorClicked(object sender, EventArgs e)
        {
            Random random = new Random();
            RedSlider.Value = random.Next(0, 256);
            GreenSlider.Value = random.Next(0, 256);
            BlueSlider.Value = random.Next(0, 256);

            UpdateColorPreview();
        }
    }
}
