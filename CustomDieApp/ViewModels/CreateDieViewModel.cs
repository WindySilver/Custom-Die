using System;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CustomDieApp.ViewModels;

public partial class CreateDieViewModel : ViewModelBase
{
    private readonly Action<Die?> _onFinished;

    public string SidesText { get; set; } = "";
    [ObservableProperty]
    public string errorText = "";

    public CreateDieViewModel(Action<Die?> onFinished)
    {
        _onFinished = onFinished;
    }

    public void Create()
    {
        var sides = SidesText
            .Split(new[] { ',' },
                   StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList();

        if (sides.Count < 2) {
            ErrorText = "The die needs at least two sides";
            return;
            }
        _onFinished(new Die(sides));
    }

    public void Cancel() => _onFinished(null);
}