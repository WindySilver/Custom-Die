using System;
using System.Collections.Generic;

public class Die{

    public List<string> Sides { get; set; }

    public Die(List<string> sidesC)
    {
        Sides = sidesC;
    }
}