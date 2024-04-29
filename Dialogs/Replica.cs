using System;

public class Replica{
    public string Owner { get; set; }
    public string Text { get; set; }

    public Replica(string Owner, string Text){
        this.Owner = Owner;
        this.Text = Text;
    }

    public Replica(){
    }
}