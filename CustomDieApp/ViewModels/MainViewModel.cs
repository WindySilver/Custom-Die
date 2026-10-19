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
    public Die DefaultDie = new Die(new List<string> {"1", "2", "3", "4", "5", "6"});
    public string SidesText => string.Join(", ", DefaultDie.Sides);
    public int dieSize => DefaultDie.Sides.Count;
    [ObservableProperty]
    public string rollResults = "-";
    public int howManyRolls = 1;
    public bool noDuplicates = false;
    

    [RelayCommand]
    private void RollADie() // TODO: Implement howManyRolls and noDuplicates here
    {
        Random rnd = new Random();
        RollResults = "Roll result: " + rnd.Next(dieSize).ToString();
    }
}
