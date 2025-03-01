Program JIAbPOTOPHA9I_2;
//the program finds the Lest Common Multiple of N numbers

{$APPTYPE CONSOLE}

Uses
  SysUtils;

Var
  LCM, NumOfNum, Num, NumN, i, j: Integer;
//LCM - the Lest Common Multiple
//NumOfNum - the Number Of Numbers
//Num - the Number that will be added to the LCM to find it
//NumN - Numbers for LCM

  F1: Boolean;
//F1 - Flag

Begin
//data input and verification
  Repeat
    Write ('input num of num: ');
    Readln (NumOfNum);
    Write ('input 1st num: ');
    Readln (LCM);
    If (NumOfNum <= 1) and (LCM <= 0) then
      Writeln ('incorrect input data. try again')
  Until (NumOfNum > 1) and (LCM > 0);

//cycle for input and verification multiple numbers and find LCM
  For i := 2 to NumOfNum do
  Begin
    Repeat
      Write ('input ',i,'th num: ');
      Readln (NumN);
      If (NumN <= 0) then
      Writeln ('incorrect input data. try again')
    Until (NumN > 0);

//initializing cycle parameters
    Num := LCM;
    F1 := True;

//if LCM not LCM yet, then cycle are finding it
    If LCM mod NumN <> 0 then
      For j := 1 to NumN do
        If F1 then
        Begin
          LCM := LCM + Num;
          If LCM mod NumN = 0 then
            F1 := False
        End
  End;

//data output
  Write ('Least Common Multiple: ', LCM);
  Readln
End.
