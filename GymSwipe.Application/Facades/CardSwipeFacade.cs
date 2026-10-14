using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services.SessionServices;

namespace GymSwipe.ApplicationLayer.Facades
{
    public class CardSwipeFacade : ICardSwipeFacade
    {
        //All loaded exercises
        public List<ExerciseDTO> AvailableExercises { get; set; } = new List<ExerciseDTO>();
        //Exercises user has approved
        public List<ExerciseDTO> AddedExercises { get; set; } = new List<ExerciseDTO>();
        public List<TargetAreaDTO> TargetAreas { get; set; } = new List<TargetAreaDTO>();
        private readonly IExerciseService _exerciseService;
        private readonly IGymPlaylistService _playlistService;
        private readonly ITraningAreaService _traningAreaService;
        private readonly LoggedInUser _user;
        public CardSwipeFacade(
            IExerciseService exerciseService,
            IGymPlaylistService playlistExecericseService,
            ITraningAreaService traningAreaService,
            LoggedInUser user
            )
        {
            _exerciseService = exerciseService;
            _playlistService = playlistExecericseService;
            _traningAreaService = traningAreaService;
            _user = user;
        }
        //We can pass a Id to load for example chest exercises
        //Not implemented, UI related, TODO!
        public bool HasDrawnAllCards() { return AvailableExercises.Count() == 0; } // true false om tom
        public async Task LoadExercisesAsync()
        {
            await LoadExercisesById(null);
        }
        public async Task LoadExercisesById(int? possibleId)
        {
            if (possibleId.HasValue)
            {
                AvailableExercises = await _exerciseService.GetExerciseByIdAsync(possibleId.Value);
            }
            else
            {
                AvailableExercises = await _exerciseService.GetAllExercisesAsync();
            }
            TargetAreas = await _traningAreaService.GetExerciseTargetsAsync();
        }


        //If there's no more cards, return null (something else later, a cardresult?)
        public async Task<ExerciseDTO> DrawNewExerciseCard() //Start
        {
            if (HasDrawnAllCards())
            {
                return null;
            }
            //Draw a random valid card

            var exercise = AvailableExercises[Random.Shared.Next(AvailableExercises.Count)];
            AvailableExercises.Remove(exercise);
            return exercise;
        }


        //Swipe right, add, next card
        public async Task<ExerciseDTO> ApproveCard(ExerciseDTO exercise)
        {
            AddedExercises.Add(exercise);
            return await DrawNewExerciseCard();
        }
        //Swipe left, next card
        public async Task<ExerciseDTO> DiscardCard()
        {
            return await DrawNewExerciseCard();
        }






        //Print info on the page
        public async Task<string> DeckInfo()
        {
            if (HasDrawnAllCards())
            {
                return "Last card!";
            }
            return $"There are {AvailableExercises.Count} cards remaning in the deck";
        }

        //Looks for a valid image based on a muscle area of an exercise
        public async Task<string> GetApproptiateImageForExercise(int id)
        {
            string img = await _traningAreaService.GetTargetAreaImage(id);

            return img + "_card.jpg";
        }


        //Saves a playlist to user, needs user to set name of playlist => Firstname + playlist + date
        public async Task SavePlayList()
        {
            //If there's anything relevant to save
            if (AddedExercises != null && AddedExercises.Count > 0)
            {
                await _playlistService.SavePlaylistToUser(AddedExercises, _user.CurrentUser);
            }
        }

    }
}
