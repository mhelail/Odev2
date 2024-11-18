using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;

namespace app2
{
    public partial class ToDoList : ContentPage
    {
        private List<string> tasks;

        public ToDoList()
        {
            InitializeComponent();


            if (Preferences.ContainsKey("tasks"))
            {
                string tasksJson = Preferences.Get("tasks", string.Empty);
                if (!string.IsNullOrWhiteSpace(tasksJson))
                {
                    tasks = System.Text.Json.JsonSerializer.Deserialize<List<string>>(tasksJson);
                }
            }
            else
            {
                tasks = new List<string>();
            }


            tasksCollectionView.ItemsSource = tasks;


            notEkleButton.Clicked += NotEkleButton_Clicked;


            var kaydetButton = (Button)FindByName("kaydetButton");
            if (kaydetButton != null)
            {
                kaydetButton.Clicked += KaydetButton_Clicked;   
            }
        }

        private async void NotEkleButton_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync(
                "Yeni Not",
                "Yapılacak işinizi yazın:",
                "Kaydet",
                "Iptal",
                "Örnek: Görev 1",
                100,
                Keyboard.Text);

            if (!string.IsNullOrWhiteSpace(result))
            {
                tasks.Add(result);
                tasksCollectionView.ItemsSource = new List<string>(tasks);
            }
        }

        private async void KaydetButton_Clicked(object sender, EventArgs e)
        {

            string tasksJson = System.Text.Json.JsonSerializer.Serialize(tasks);


            Preferences.Set("tasks", tasksJson);

            await DisplayAlert("Kaydedildi", "Tüm işler başarıyla kaydedildi.", "Tamam");
        }

        private async void EditTask_Clicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            if (button == null) return; 
            var item = button.BindingContext as string;
            if (item == null) return;

            int index = tasks.IndexOf(item);
            string newTask = await DisplayPromptAsync(
                "Görevi Düzenle",
                "Yeni görevi yazın:",
                "Kaydet",
                "İptal",
                item);

            if (!string.IsNullOrWhiteSpace(newTask))
            {
                tasks[index] = newTask;
                tasksCollectionView.ItemsSource = new List<string>(tasks);
            }
        }

        private async void DeleteTask_Clicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            if (button == null) return;

            var item = button.BindingContext as string;
            if (item == null) return;

            
            bool confirmDelete = await DisplayAlert("Silme Onayı", "Bu görevi silmek istediğinizden emin misiniz?", "Evet", "Hayır");

            if (confirmDelete)
            {
                
                tasks.Remove(item);
                tasksCollectionView.ItemsSource = new List<string>(tasks);
            }
        }

    }
}
