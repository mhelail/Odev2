using System;
using Microsoft.Maui.Controls;

namespace app2
{
    public partial class KrediHesaplama : ContentPage
    {
        public KrediHesaplama()
        {
            InitializeComponent();

            
            HesaplaButton.Clicked += OnHesaplaClicked;
        }

        
        private void OnHesaplaClicked(object sender, EventArgs e)
        {
            string krediTuru = KrediTuruPicker.SelectedItem?.ToString()?.Trim();

            bool krediTutarValid = double.TryParse(KrediTutarEntry.Text, out double krediTutar);
            bool faizOraniValid = double.TryParse(FaizOraniEntry.Text, out double faizOrani);
            bool vadeValid = int.TryParse(VadeEntry.Text, out int vade);

            
            if (string.IsNullOrEmpty(krediTuru) || !krediTutarValid || !faizOraniValid || !vadeValid)
            {
                DisplayAlert("Hata", "Lütfen geçerli girişler girin.", "Tamam");
                return;
            }

          
            double brutFaiz;
            if (string.Equals(krediTuru, "İhtiyaç Kredisi", StringComparison.InvariantCultureIgnoreCase))
            {
                brutFaiz = ((faizOrani + (faizOrani * 0.1) + (faizOrani * 0.15)) / 100);
            }
            else if (string.Equals(krediTuru, "Taşıt Kredisi", StringComparison.InvariantCultureIgnoreCase))
            {
                brutFaiz = ((faizOrani + (faizOrani * 0.15) + (faizOrani * 0.05)) / 100);
            }
            else if (string.Equals(krediTuru, "Konut Kredisi", StringComparison.InvariantCultureIgnoreCase))
            {
                brutFaiz = (faizOrani / 100);
            }
            else if (string.Equals(krediTuru, "Ticari Kredisi", StringComparison.InvariantCultureIgnoreCase))
            {
                brutFaiz = ((faizOrani + (faizOrani * 0.5) + (faizOrani * 0.05)) / 100);
            }
            else
            {
                DisplayAlert("Hata", "Geçerli bir kredi türü seçiniz.", "Tamam");
                return;
            }

            double aylikTaksit = ((Math.Pow(1 + brutFaiz, vade) * brutFaiz) / (Math.Pow(1 + brutFaiz, vade) - 1)) * krediTutar;
            double toplamOdeme = aylikTaksit * vade;
            double toplamFaiz = Math.Round(toplamOdeme - krediTutar);

            AylikTaksitLabel.Text = $"Aylık Taksit: {aylikTaksit:F2} TL";
            ToplamOdemeLabel.Text = $"Toplam Ödeme: {toplamOdeme:F2} TL";
            ToplamFaizLabel.Text = $"Toplam Faiz: {toplamFaiz:F2} TL";
        }

        private void OnVadeSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            double newValue = e.NewValue;
            VadeEntry.Text = newValue.ToString("0");
        }

    }
}
