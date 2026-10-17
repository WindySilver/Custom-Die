using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace CustomDieApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    //[ObservableProperty]
    //public partial string Greeting { get; set; } = "Welcome to Avalonia!";
    public Die DefaultDie = new Die(6, new List<string> {"1", "2", "3", "4", "5", "6"});
    public string SidesText => string.Join(", ", DefaultDie.getSides());
}
