using System;
using Microsoft.Maui.Controls;

namespace app2
{
    public partial class VucutKitleIndexi : ContentPage
    {
        public VucutKitleIndexi()
        {
            InitializeComponent();

            CalculateBMI();
        }

        private void OnKiloSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            int kilo = (int)e.NewValue;
            KiloLabel.Text = kilo.ToString();
            CalculateBMI(); 
        }

        
        private void OnBoySliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            int boy = (int)e.NewValue;
            BoyLabel.Text = boy.ToString();
            CalculateBMI(); 
        }

       
        private void CalculateBMI()
        {
            bool kiloValid = int.TryParse(KiloLabel.Text, out int kilo);
            bool boyValid = int.TryParse(BoyLabel.Text, out int boy);

            if (kiloValid && boyValid && boy > 0)
            {
                double heightInMeters = boy / 100.0;
                double vki = kilo / (heightInMeters * heightInMeters);
                VkiLabel.Text = vki.ToString("F2");

               
                string status = GetBMIStatus(vki);
                StatusLabel.Text = status;
            }
        }

        
        private string GetBMIStatus(double vki)
        {
            if (vki < 16)
                return "İleri Düzeyde Zayıf";
            else if (vki >= 16 && vki <= 16.99)
                return "Orta Düzeyde Zayıf";
            else if (vki >= 17 && vki <= 18.49)
                return "Hafif Düzeyde Zayıf";
            else if (vki >= 18.5 && vki <= 24.99)
                return "Normal Kilolu";
            else if (vki >= 25 && vki <= 29.99)
                return "Hafif Şişman / Fazla Kilolu";
            else if (vki >= 30 && vki <= 34.99)
                return "1. Derecede Obez";
            else if (vki >= 35 && vki <= 39.99)
                return "2. Derecede Obez";
            else
                return "3. Derecede Obez / Morbid Obez";
        }
    }
}
