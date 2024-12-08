using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFClient.Commands;
using WPFClient.Services;

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
        public ICommand ViewFullInfoCommand { get; }

        public MainViewModel(IRecipeService recipeService)
        {
            _recipeService = recipeService;
            Recipes = new ObservableCollection<Recipe>();

            AddRecipeCommand = new RelayCommand(OpenAddRecipeDialog);
            EditRecipeCommand = new RelayCommand(OpenEditRecipeDialog, _ => SelectedRecipe != null);
            DeleteRecipeCommand = new RelayCommand(DeleteRecipe, _ => SelectedRecipe != null);
            ViewFullInfoCommand = new RelayCommand(OpenFullInfoDialog, _ => SelectedRecipe != null);

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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading recipes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    await _recipeService.AddRecipeAsync(newRecipe);
                    Recipes.Add(newRecipe);
                    addWindow.Close();
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

            var editViewModel = new RecipeDetailsViewModel(SelectedRecipe);
            var editWindow = new RecipeDetailsView { DataContext = editViewModel };

            editViewModel.SaveRecipe += async (updatedRecipe) =>
            {
                try
                {
                    await _recipeService.UpdateRecipeAsync(updatedRecipe);
                    LoadRecipes();
                    editWindow.Close();
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
            if (MessageBox.Show("Are you sure you want to delete this recipe?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    await _recipeService.DeleteRecipeAsync(SelectedRecipe.Id);
                    Recipes.Remove(SelectedRecipe);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting recipe: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void OpenFullInfoDialog(object obj)
        {
            if (SelectedRecipe == null) return;

            var fullRecipe = await _recipeService.GetRecipeByIdAsync(SelectedRecipe.Id);
            var fullInfoViewModel = new FullInfoViewModel(fullRecipe, _recipeService);
            var fullInfoWindow = new FullInfoView { DataContext = fullInfoViewModel };
            fullInfoWindow.ShowDialog();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
