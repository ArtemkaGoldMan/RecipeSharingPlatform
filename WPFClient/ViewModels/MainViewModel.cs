using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFClient.Services;
using WPFClient.Commands;
using WPFClient.Models;
using System.Windows;

namespace WPFClient.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IRecipeService _recipeService;

        public ObservableCollection<Recipe> Recipes { get; set; }
        public Recipe SelectedRecipe { get; set; }

        public ICommand AddRecipeCommand { get; }
        public ICommand EditRecipeCommand { get; }
        public ICommand DeleteRecipeCommand { get; }

        public MainViewModel(IRecipeService recipeService)
        {
            _recipeService = recipeService;
            Recipes = new ObservableCollection<Recipe>();

            AddRecipeCommand = new RelayCommand(OpenAddRecipeDialog);
            EditRecipeCommand = new RelayCommand(OpenEditRecipeDialog, _ => SelectedRecipe != null);
            DeleteRecipeCommand = new RelayCommand(DeleteRecipe, _ => SelectedRecipe != null);

            LoadRecipes();
        }

        private async void LoadRecipes()
        {
            try
            {
                var recipes = await _recipeService.GetAllRecipesAsync();
                Recipes.Clear();
                foreach (var recipe in recipes)
                {
                    Recipes.Add(recipe);
                }
                if (!recipes.Any())
                {
                    System.Diagnostics.Debug.WriteLine("No recipes found from API.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading recipes: {ex.Message}");
            }
        }

        private void OpenAddRecipeDialog(object obj)
        {
            var addViewModel = new RecipeDetailsViewModel();
            var addWindow = new RecipeDetailsView { DataContext = addViewModel };

            addViewModel.SaveRecipe += async (newRecipe) =>
            {
                try
                {
                    await _recipeService.AddRecipeAsync(newRecipe); // Save via service
                    Recipes.Add(newRecipe); // Update ObservableCollection
                    addWindow.Close(); // Close dialog after saving
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding recipe: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            addWindow.ShowDialog();
        }


        private void OpenEditRecipeDialog(object obj)
        {
            if (SelectedRecipe == null) return;

            var editViewModel = new RecipeDetailsViewModel(SelectedRecipe); // Pass existing recipe
            var editWindow = new RecipeDetailsView { DataContext = editViewModel };

            editViewModel.SaveRecipe += async (updatedRecipe) =>
            {
                try
                {
                    await _recipeService.UpdateRecipeAsync(updatedRecipe); // Save changes via API
                    LoadRecipes(); // Reload recipes to reflect the update
                    editWindow.Close(); // Close dialog after saving
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating recipe: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            editWindow.ShowDialog();
        }


        private async void DeleteRecipe(object obj)
        {
            await _recipeService.DeleteRecipeAsync(SelectedRecipe.Id);
            Recipes.Remove(SelectedRecipe);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
