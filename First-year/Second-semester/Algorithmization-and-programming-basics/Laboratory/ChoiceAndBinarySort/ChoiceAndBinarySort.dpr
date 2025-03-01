program ChoiceAndBinarySort;
//The following program analyzes by the number of permutations
//two sorting methods: by choice and by binary inserts

{$APPTYPE CONSOLE}

uses
  SysUtils;

const
  N = 5000;
//N - Num of elements

type
  TArray = array [1..N] of Integer;
//TArray - array with N elements of Integer

(*sorting procedure by choice*)
procedure SelectionSort(CurrArray: TArray; var Reshuffle: Integer);

var
  CurrEl, MinId: Integer;
//CurrEl - Current Element
//MinId - Min Identifier

  i, j: Integer;

begin
{initializing Reshuffle}
  Reshuffle := 0;

{cycle for the passage of all elements}
  for i := 1 to N - 1 do
  begin
{initializng MinId}
    MinId := i;

{cycle of finding the minimum number in an array}
    for j := i + 1 to N do
    if CurrArray[MinId] > CurrArray[j] then
      MinId := j;

{if the number is not necessary - we rearrange it}
    CurrEl := CurrArray[MinId];
    CurrArray[MinId] := CurrArray[i];
    CurrArray[i] := CurrEl;

{increment Reshuffle}
    Reshuffle := Reshuffle + 3
  end
end;

(*binary insert sorting procedure*)
procedure BinarySort (CurrArray: TArray; var Reshuffle: Integer);

var
  CurrEl, LeftBord, RightBord, Center: Integer;
//CurrEl - Current Element
//LeftBord - LeftBord
//RightBord - RightBord
//Center - Center

  i, j: Integer;

begin
{initializng Reshuffle}
  Reshuffle := 0;

{the cycle passes the elements. all elements before the current sorted}
  for i := 2 to N do
  begin
{initializing data}
    CurrEl := CurrArray[i];
    inc (Reshuffle);
    LeftBord := 1;
    RightBord := i;
    Center := (LeftBord + RightBord) div 2;

{the center is not aligned with the left border - do}
    while Center <> LeftBord do
    begin
{if the number is greater than the central number, we go to the right, otherwise - to the left}
      if CurrArray[Center] > CurrEl then
      begin
        RightBord := Center;
        Center := (LeftBord + RightBord) div 2
      end

{repeat until blue}
      else
      begin
        LeftBord := Center;
        Center := (LeftBord + RightBord) div 2
      end
    end;

{when we found the point in the array, we move the entire array}
    for  j := i downto Center + 2 do
    begin
      CurrArray[j] := CurrArray[j - 1];
      inc(Reshuffle)
    end;

{see what to do with the last element}
    if CurrEl >= CurrArray[Center] then
    begin
      CurrArray[Center + 1] := CurrEl;
      inc(Reshuffle)
    end

{if the element is smaller, then one permutation is more}
    else
    begin
      CurrArray[Center + 1] := CurrArray[Center];
      CurrArray[Center] := CurrEl;
      Reshuffle := Reshuffle + 2
    end
  end
end;

var
  SourceArray: TArray;
//SourceArray- Source Array

  NumOfMov: Integer;
//NumOfMov - Num OF Mov

  Table: Text;
//Table - text Table

  i: Integer;

begin
  randomize;

//assign file and variable
  assign (Table, 'D:\�����\����\2\2\Table.txt');

//initializing
  for i := 1 to N do
    SourceArray[i] := random (1000000000000000000);

//call the procedure and output the results
  SelectionSort (SourceArray, NumOfMov);
  writeln (NumOfMov, '     |     ', (N - 1) * 3);

//call the procedure and output the results
  BinarySort (SourceArray, NumOfMov);
  writeln (NumOfMov, '    |   ', ((N * N + 7 * N) div 4 - 2));

  readln
end.

