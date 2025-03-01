Program IterationLoop;
//find the value of the function f (x),
//at x from -0.5 to 0.5,
//with the accuracy of Eps: 10 ^ (-5), 10 ^ (-6), 10 ^ (-7)
{$APPTYPE CONSOLE}

Uses
  SysUtils;

Const
  Inf = 1000000;
//Inf - Infinity
Var
  x, f, gr, El: Real;
//x,f,f0 - function arguments

  k, fac, i: Integer;
//k - function argument
//fac - factorial

  Eps: Real;
//Eps - Epsilon

Begin
//initializing Eps
  Eps := 0.00001;

//cycle by Eps
  For i := 0 to 2 do
  Begin

(*initializing*)
    x := -0.5;
    gr := 0.5;

//cycle by x
    While x <= gr do
    Begin

(*calculating value of expression with accuracy of Eps*)
      f := sqr(x) * x / 3;
      k := 2;
      fac := k;
      Repeat
        El := exp ((k + 2) * ln(Abs(x))) / (fac * (k + 2));
        If (k mod 2 <> 0) and (x < 0) then
          El := El * (- 1);
        f := f + El;
        k := k + 1;
        fac := fac * k
      Until (Abs(El) < Eps);

//data output
      Writeln('f = ',f:12:9,' x = ',x:4:1, ' Eps = ',Eps:(6+i):(5+i),' ',k-2);

//transformation of cycle parameters
      x := x + 0.1
    End;
    Writeln;
    For k := 1 to 41 + i do
      Write (Chr(1 + i));
    Writeln;
    Writeln;

//transformation of cycle parameters
    Eps := Eps / 10
  End;
  Readln
End.
