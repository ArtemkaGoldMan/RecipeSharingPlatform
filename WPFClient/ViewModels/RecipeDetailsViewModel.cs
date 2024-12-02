using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFClient.Commands;
using WPFClient.Models;

namespace WPFClient.ViewModels
{
    public class RecipeDetailsViewModel : INotifyPropertyChanged
    {
        public Recipe Recipe { get; set; }
        public ICommand SaveCommand { get; }

        public event Action<Recipe> SaveRecipe;

        public RecipeDetailsViewModel()
        {
            Recipe = new Recipe
            {
                Title = string.Empty,
                Description = string.Empty,
                Creator = "DefaultCreator", // Default or user input
                Category = string.Empty,
                Ingredients = string.Empty,
                Instructions = string.Empty
            };
            SaveCommand = new RelayCommand(Save);
        }

        public RecipeDetailsViewModel(Recipe recipe)
        {
            Recipe = recipe;
            SaveCommand = new RelayCommand(Save);
        }

        private void Save(object obj)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(Recipe.Title) || string.IsNullOrWhiteSpace(Recipe.Category))
            {
                MessageBox.Show("Title and Category are required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveRecipe?.Invoke(Recipe); // Raise the event to notify the parent ViewModel
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
