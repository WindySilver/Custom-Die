using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using Avalonia.Interactivity;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;

namespace CustomDieApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    //[ObservableProperty]
    //public partial string Greeting { get; set; } = "Welcome to Avalonia!";
    public Die DefaultDie = new Die(new List<string> {"1", "2", "3", "4", "5", "6", "Secret"});
    public string sidesText => string.Join(", ", ActiveDie.Sides); 
    public int dieSize => ActiveDie.Sides.Count;
    [ObservableProperty]
    public string rollResults = "";
    [ObservableProperty]
    public int howManyRolls = 1;
    [ObservableProperty]
    public bool noDuplicates = false;
    
    private CreateDieViewModel? _creator;
    public CreateDieViewModel? Creator
    {
        get => _creator;
        set { _creator = value; OnPropertyChanged(); }   // use your base class's equivalent
    }

    private Die _activeDie;
    public Die ActiveDie {
        get => _activeDie;
        private set
        {
            if (SetProperty(ref _activeDie, value))
            {
                OnPropertyChanged(nameof(sidesText));
                OnPropertyChanged(nameof(dieSize));
            }
        }
    }

    public MainViewModel()
    {
        ActiveDie = DefaultDie;
    }

    public void StartCreating()
    {
        Creator = new CreateDieViewModel(die =>
        {
            if (die != null) ActiveDie = die;
            Creator = null;
        });
    }

    [RelayCommand]
    private void RollADie()
    {
        Random rnd = new Random();
        List<int> results = new List<int>{};
        int addition = 0;

        for (int i = 0; i<HowManyRolls; i++){
            // If all potential rolls have been rolled and no duplicates are allowed, stop the loop
            if(NoDuplicates && i == dieSize) break;

            addition = rnd.Next(dieSize);
            // If no duplicates are allowed, keep rolling until a new side is rolled
            while(NoDuplicates && results.Contains(addition) && i < dieSize){
                addition = rnd.Next(dieSize);
            }
            results.Add(addition);
        }

        RollResults = "Roll result: " + ActiveDie.Sides[results[0]];
        for(int i = 1; i < results.Count; i++){
            RollResults = RollResults + ", " + ActiveDie.Sides[results[i]];
        }
    }
}
