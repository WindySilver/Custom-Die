using System;
using System.Collections.Generic;

//namespace CustomDieApp;

public class Die{

    private int size;
    private List<string> sides;

    public Die(int sizeC, List<string> sidesC){
        size = sizeC;
        sides = sidesC;
    }

    public int getSize(){
        return size;
    }

    public void setSize(int change){
        size = change;
    }

    public List<string> getSides(){
        return sides;
    }

    public void setSides(List<string> change){
        sides = change;
    }
}