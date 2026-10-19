using System;
using System.Collections.Generic;

//namespace CustomDieApp;

public class Die{

    //public int Size { get; set; } Sides.Count should be good enough; size can be a separate variable in the "make new die" window
    public List<string> Sides { get; set; }

    public Die(List<string> sidesC)
    {
        Sides = sidesC;
    }
}