Program MatrixCalculations;
//this program performs calculations by formula where
//A, B are matrices of dimension 3x3 with integer elements

{$APPTYPE CONSOLE}

uses
  SysUtils,
  UnitMatrixArithmetic in 'UnitMatrixArithmetic.pas';

Const
  A : TMatrix = ((2,3,1),(-1,2,4),(5,3,0));
  B : TMatrix = ((2,7,13),(-1,0,5),(5,13,21));

Var
  C : TMatrix;

//C - resulting matrix

  i, j : Integer;

Begin
//formula from condition
  C := Sub(MulArr(A,Sub(MulArr(A,A),B)),MulConst(2,MulArr(Sum(B,A),B)));

//output data
  For i := 1 to 3 do
  Begin
    For j := 1 to 3 do
      Write (C[i,j]:3,' ');
    Writeln
  End;
  Readln
End.

