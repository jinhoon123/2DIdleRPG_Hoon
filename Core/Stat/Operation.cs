using System.Collections.Generic;

public class Operation
{
    public Unified Zero => new Unified(0);
    public Unified One => new Unified(1);
    public Unified Add(Unified left, Unified right) => left + right;
    public Unified Multiply(Unified left, Unified right) => left * right;
    public Unified Max(Unified left, Unified right) => Comparer<Unified>.Default.Compare(left, right) > 0 ? left : right;
}