Program MatrixSort;
//given matrix X [9,9]. the program arranges the elements of the rows
//of the matrix by an increase, and the rows themselves by the increase
//of the sum of the elements of the rows

{$APPTYPE CONSOLE}

Uses
  SysUtils;

Const
  LengNums = 3;
//LengNums - Leng Nums

Type
  TArray = Array [1..9,1..9] of Integer;
//TArray - the type for the matrix

Var
  i, j, k, Bank, IdMin, Sum: Integer;
//Bank - 'Bank' used for transfers elements
//IdMin - Id of the Min num
//Sum - Sum

  X: TArray;
//X - given mftrix

  F1: Boolean;
//F1 - Flag

Begin
//initializing matrix
  For i := 1 to 9 do
  Begin

{initializing sum}
    Sum := 0;
    For j := 1 to 8 do
    Begin
      X[i,j] := Random (1000);

//calculating the sum and recording it
      Sum := Sum + X[i,j]
    End;
    X[i,9] := Sum
  End;

//output of the source matrix
  For i := 1 to 9 do
  Begin
    For j := 1 to 8 do
      Write (X[i,j]:LengNums,' ');
    Writeln
  End;
  Writeln ('----------------------------------');

(*Sorting by choice*)

//cycle for arranges the elements of the rows by an increase:
  For i := 1 to 9 do
    For k := 1 to 8 do
    Begin

//cycle for finding the min element of the row, beginning with 'k'
      IdMin := k;
      For j := k to 8 do
        If X[i,IdMin] > X[i,j] then
          IdMin := j;

//transfer min element to the beginning
      Bank := X[i,IdMin];
      For j := IdMin downto k + 1 do
        X[i,j] := X[i,j-1];
      X[i,k] := Bank
    End;

//output of the result after the first step
  For i := 1 to 9 do
  Begin
    For j := 1 to 8 do
      Write (X[i,j]:LengNums,' ');
    Writeln
  End;
  Writeln ('----------------------------------');

(*Sorting by 'bubble' with a Flag*)
//initializing data
  F1 := True;

//cycle for arranges the rows by the increase
//of the sum of the elements of the rows:
  While F1 do
  Begin
    F1 := False;

//cycle for transferring larger elements to the end of an array
    For i := 2 to 9 do

//if the element on the left is greater than the element
//on the right - these elements are rearranged
      If X[i - 1,9] > X[i,9] then
        For k := 1 to 9 do
        Begin
          Bank := X[i - 1,k];
          X[i - 1, k] := X[i,k];
          X[i,k] := Bank;

//if there has been at least one permutation - the cycle will continue,
//else - it stopping
          F1 := True
        End
  End;

//output of the result after the second step
  For i := 1 to 9 do
  Begin
    For j := 1 to 8 do
      Write (X[i,j]:LengNums,' ');
    Writeln ('   ',X[i,9]:(LengNums + 1))
  End;
  Readln
End.
