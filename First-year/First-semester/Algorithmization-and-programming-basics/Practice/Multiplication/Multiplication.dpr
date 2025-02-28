Program Multiplication;
//program calculates multiplication of two numbers (up to 50 digits)
//in any notation

{$APPTYPE CONSOLE}

Uses
  SysUtils;

Var
  NumInStr1,NumInStr2,Alp,Miss1,Miss2: String [50];
//NumInStr1, NumInStr1 - 1st, 2nd Num In Str
//ALp - Alphabet for notation
//Miss1, Miss2 - Misspelling

  NumInArray1,NumInArray2,Mul: Array of Integer;
//NumInArray1, NumInArray2 - Num In Array
//Mul - Multiplication

  i,j,k,Notation,Lgth1,Lgth2,InMind,Step: Integer;
//Notation - Notation
//Lgth1, Lgth2 - Length of 1st and 2nd num
//InMind - num 'In Mind'

  FMiss1,FMiss2,FSign1,Fsign2: Boolean;
//FMiss1, FMiss2 - 1st, 2nd FLag for Misspeling
//FSign1, FSign2 - 1st, 2nd Flag dor Sign

Begin
//input and verification notation
  Repeat
  Write ('input Notation (from 2 to 36): ');
  Readln (Notation);

(*if Notation is not within a 'reasonable framework' - output error and repeat*)
  If (Notation < 2) or (Notation > 36) then
   Writeln ('incorrect input data. try now ');
  Until (Notation>1) and (Notation<37);

//creation of Alphabet for chosen Notation
  For i := 1 to Notation do
    If i <= 10 then
      Alp :=Alp + Char(47 + i)
    Else Alp := Alp + Char(54 + i);

//input nums
  Write ('input 1st num: ');
  Readln (NumInStr1);
  Write ('input 2nd num: ');
  Readln(NumInStr2);

//initializing cycle
  FMiss1 := False;
  FMiss2 := False;
  FSign1 := False;
  FSign2 := False;

//cycles for searching, deleting from strings
//and storing unnecessary characters:
(*initializing*)
  i:=1;
  While i <= Length(NumInStr1) do
  Begin

//if the first character "-" - remembers this and deletes it
    If not FSign1 and (NumInStr1[1] = '-') then
    Begin
      FSign1 := True;
      Delete (NumInStr1,1,1)
    End;

(*if character isn't in Alp - delete and storing it in Mis-spelling*)
    If Pos(NumInStr1[i],Alp) = 0 then
    Begin
      Miss1 := Miss1 + NumInStr1[i] + ' ';
      Delete (NumInStr1,i,1);

(*if there is at least one misspelling - error is display*)
      FMiss1 := True;
    End
    Else
      i := i + 1
  End;

(*initializing*)
  i := 1;
  While i <= Length(NumInStr2) do
  Begin

//if the first character "-" - remembers this and deletes it
    If not FSign2 and (NumInStr2[1] = '-') then
    Begin
      FSign2 := True;
      Delete (NumInStr2,1,1)
    End;

(*if character isn't in Alp - delete and storing it in Mis-spelling*)
    If Pos(NumInStr2[i],Alp) = 0  then
    Begin
      Miss2 := Miss2 + NumInStr2[i] + ' ';
      Delete (NumInStr2,i,1);

(*if there is at least one misspelling - error is display*)
      FMiss2 := true
    End
    Else
      i := i + 1
  End;

//introduction of dynamic arrays for Num
  Lgth1 := Length(NumInStr1);
  Lgth2 := Length(NumInStr2);
  SetLength(NumInArray1,Lgth1);
  SetLength(NumInArray2,Lgth2);

//cycles for translating each line element to the number of
//its position in the alphabet created earlier for
//arithmetic operations:
  For i := 0 to Lgth1 - 1 do
    NumInArray1[i] := Pos(NumInStr1[i + 1],Alp) - 1;
  For i := 0 to Lgth2 - 1 do
    NumInArray2[i] := Pos(NumInStr2[i + 1],Alp) - 1;


//create dynamic array for the mul and initialize cycle:
  SetLength(Mul,Lgth1 + Lgth2);
  For i := 0 to Length (Mul) - 1 do
    Mul[i] := 0;
  InMind := 0;

//cycle for Multiplication:
  For j := Lgth2 - 1 downto 0 do
  Begin

(*each element of 1st numb is multiplied by one
element of 2nd num and added to result element.
repeats with all elements of the second number:*)
    For i := Lgth1 - 1 downto 0 do
    Begin
      Step := NumInArray1[i] * NumInArray2[j] + Mul[i + j + 1] + InMind;
      Mul[i + j + 1] := Step mod Notation;
      InMind := Step div Notation;

(*last index number is stored, InMind is added to the result and is reset*)
      Step := i
    End;
    Mul[Step + j] := InMind;
    InMind := 0
  End;

//elimination of an insignificant zero
  While (Mul[0] = 0) and (Length(Mul) >  1) do
  Begin
    For i := 0 to Length(Mul) - 2 do
      Mul[i] := Mul[i+1];
    SetLength(Mul,Length(Mul) - 1)
  End;

//data output
  Write ('Mul: ');

//if signs are different - write '-'
  If FSign1 xor FSign2 then
    Write ('-');
  For i := 0 to Length(Mul) - 1  do
  Write (Alp[Mul[i]+1]);
  Writeln;

//output unnecessary characters
  If FMiss1 then
    Writeln('misspelling in 1st num: ', Miss1);
  If FMiss2 then
    Writeln('misspelling in 2nd num: ', Miss2);
  Readln
End.
