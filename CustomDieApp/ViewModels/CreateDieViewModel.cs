using System;
using System.Linq;

namespace CustomDieApp.ViewModels;

public class CreateDieViewModel : ViewModelBase
{
    private readonly Action<Die?> _onFinished;

    public string SidesText { get; set; } = "";

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

        if (sides.Count < 2) return;   // a die needs at least two sides
        _onFinished(new Die(sides));
    }

    public void Cancel() => _onFinished(null);
}