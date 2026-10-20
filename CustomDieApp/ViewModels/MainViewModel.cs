using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Avalonia;
using Avalonia.Interactivity;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Avalonia.Controls;

namespace CustomDieApp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
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
        set { _creator = value; OnPropertyChanged(); }
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

    // Creating a new die
    public void StartCreating()
    {
        Creator = new CreateDieViewModel(die =>
        {
            if (die != null) {
                ActiveDie = die;
                RollResults = "";
            }
            Creator = null;
        });
    }

    public async Task SaveDie(IStorageProvider storageProvider)
    {
        if (storageProvider == null) return;

        var file = await storageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Die Configuration",
            FileTypeChoices = new[]
            {
                new FilePickerFileType("Text File") { Patterns = new[] { "*.txt" } },
            }
        });

        if (file != null)
        {
            await using var stream = await file.OpenWriteAsync();
            using var streamWriter = new StreamWriter(stream);
            // Save the list of sides as it's displayed on the screen
            await streamWriter.WriteLineAsync(sidesText);
        }
    }

    public async Task LoadDie(IStorageProvider storageProvider)
    {
        if (storageProvider == null) return;

        var files = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Load Die Configuration",
            AllowMultiple = false,
            FileTypeFilter = new[] { FilePickerFileTypes.TextPlain }
        });

        if (files.Count > 0)
        {
            var file = files[0];
            await using var stream = await file.OpenReadAsync();
            using var reader = new StreamReader(stream);
            string fileContent = await reader.ReadToEndAsync();
            // Make a new die to replace ActiveDie based on the side list from the file
            ActiveDie = new Die(fileContent.Split(',').ToList());
            // Empty roll result text upon loading
            RollResults = "";
        }
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
