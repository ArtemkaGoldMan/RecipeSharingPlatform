using BaseLibrary.Entities;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WPFClient.Services;

namespace WPFClient.ViewModels
{
    public class FullInfoViewModel
    {
        private readonly IRecipeService _recipeService;

        public Recipe Recipe { get; private set; }

        public ObservableCollection<Comment> Comments { get; private set; }
        public ObservableCollection<Tag> Tags { get; private set; }

        public FullInfoViewModel(Recipe recipe, IRecipeService recipeService)
        {
            Recipe = recipe;
            _recipeService = recipeService;

            Comments = new ObservableCollection<Comment>();
            Tags = new ObservableCollection<Tag>();

            LoadAdditionalData();
        }

        private async void LoadAdditionalData()
        {
            // Fetch Comments
            var comments = await _recipeService.GetCommentsByRecipeIdAsync(Recipe.Id);
            Comments.Clear();
            foreach (var comment in comments)
            {
                Comments.Add(comment);
            }

            // Fetch Tags
            var tags = await _recipeService.GetTagsByRecipeIdAsync(Recipe.Id);
            Tags.Clear();
            foreach (var tag in tags)
            {
                Tags.Add(tag);
            }
        }
    }
}
