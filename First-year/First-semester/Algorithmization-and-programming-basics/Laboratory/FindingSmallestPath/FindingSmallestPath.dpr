Program FindingSmallestPath;
//program finds smallest path from city
//A to city B, circling these cities

{$APPTYPE CONSOLE}

Uses
  SysUtils;

Const
  Inf = 1000000000;
//Inf - Infinity

Var
  Map: Array of Array of Integer;
//Map - matrix system of unilateral roads

  Distance, Detour: Array of Integer;
//Distance - Array of Distance
//Detour - Array of Detour

  Visited: Array of Boolean;
//Visited - Array of Visited

  Cities: Array of String;
//Cities - Array of Cities

  A, B, NumOfCities, CurrentCity, Min, Index, NumOfDetour, i, j: Integer;
//A - 1st city
//B - 2nd city
//NumOfCities - Num Of Cities
//CurrentCity - Current City
//Min - Min element
//Index - Index of min element
//NumOfDetour - Num Of Detours


Begin
//input and verification data
  Repeat
    Write ('input num of Cities(from 2 to 27): ');
    Readln (NumOfCities);

(*if num of cities does not match condition - data
input again, until it is entered correctly*)
    If NumOfCities < 2 then
      Writeln ('too few cities. try again');
    If NumOfCities > 27 then
      Writeln ('too many cities. try again')
  Until (NumOfCities >= 2) and (NumOfCities <= 27);

//initializing matrix
  SetLength (Map, NumOfCities + 1, NumOfCities + 1);
  For i := 1 to NumOfCities do
    For j := 1 to NumOfCities do
      Map[i,j] := Inf;

//create random Map
  For i := 1 to NumOfCities do
    For j := 1 to NumOfCities do
    Begin

//if cell is not filled - it is filled with random number from one to 100
      If (i <> j) and (Map[j,i] = Inf) then
        Map[i,j] := Random (100);

//if cell is full - it is filled with same number
      If (i <> j) and (Map[j,i] <> Inf) then
        Map[i,j] := Map[j,i];

//if cell means distance between one city - it is filled with 0
      If (i = j) then
        Map[i,j] := 0
    End;

//input and verification cities
  Repeat
    Write ('input first city(from 1 to ', NumOfCities,'): ');
    Readln (A);
    Write ('input second city(from 1 to ', NumOfCities,'): ');
    Readln (B);

(*if 1st city doesn't exist, then try again*)
    If (A < 1) or (A > NumOfCities) then
      Writeln ('first city doesn''t exist. try again');

(*if 2nd city doesn't exist, then try again*)
    If (B < 1) or (B > NumOfCities) then
      Writeln ('second city doesn''t exist. try again');

(*if 1st city is equal to 2nd, then try again*)
    If A = B then
      Writeln ('cities must be different. try again')

(*cycle does not end until cities are introduced correctly*)
  Until (1 <= A) and (A <= NumOfCities) and (1 <= B) and (B <= NumOfCities) and (A <> B);

//input and verification Num Of Detours
  Repeat
    Writeln ('input num of cities you can''t pass through(input ''0'' if there are non): ');
    Readln (NumOfDetour);

(*if Num Of Detours more then Num Of Cities - try again*)
    If NumOfDetour > NumOfCities - 2 then
      Writeln ('too many detours. try again');

(*if Num Of Detours less then 0 - try again*)
    If NumOfDetour < 0 then
      Writeln ('num of cities cann''t be negative. try again')

(*cycle does not end until detours are introduced correctly*)
  Until (NumOfDetour <= NumOfCities - 2) and (NumOfDetour >= 0);

//initializing array of Detours
  SetLength (Detour, NumOfDetour);
  If NumOfDetour = 1 then

//input cities for detiurs
    Write ('input this city: ');
  If NumOfDetour > 1 then
    Writeln ('input these cities: ');
  For i := 0 to NumOfDetour - 1 do
    Readln (Detour[i]);

//initializing array for Distance and Visited
    SetLength (Distance, NumOfCities + 1);
    SetLength (Visited, NumOfCities + 1);
    For i := 1 to NumOfCities do
    Begin
      Distance[i] := Inf;
      Visited[i] := False;
    End;

//cities that need to be bypassed are marked as visited
    For i := 0 to NumOfDetour - 1 do
      For j := 1 to NumOfCities do
        If (Detour[i] = j)then
          Visited[j] := True;

//initializing cycle and array
   Distance[A] := 0;
   SetLength(Cities, NumOfCities + 1);

//cycle for finding the crooked paths from city A to all other cities
   For i := 1 to NumOfCities do
   Begin

(*initializing cycle*)
     Min := Inf;

(*cycle for finding not Visiting city with min Distance*)
     For j := 1 to NumOfCities do
       If (not Visited[j]) and (Distance[j] <= Min) then
       Begin
         Min := Distance[j];
         Index := j;
       End;

(*initializyng cycle*)
     CurrentCity := Index;
     Visited[CurrentCity] := True;

(*cycle for finding shortest distance from city A to current city*)
     For j := 1 to NumOfCities do
       If (not Visited[j]) and (Map[CurrentCity, j]<>0) and
       (Distance[CurrentCity]<>Inf) and (Distance[CurrentCity]+
       Map[CurrentCity, j]<Distance[j]) then
       Begin
         Distance[j] := Distance[CurrentCity] + Map[CurrentCity, j];

(*address of shortest path to this point*)
         If Length (Cities [CurrentCity]) <> 0 then
           Cities[j] := Cities[CurrentCity] + ', ' + IntToStr(j)
         Else
           Cities[j] := Cities[CurrentCity] + IntToStr(j)
       End
   End;

//output Map
  For i := 1 to NumOfCities do
  Begin
    For j := 1 to NumOfCities do
      Write (Map[i,j]:2,' ');
      Writeln
  End;
  Writeln;

//output min Distance or rout unavaible
  Write ('min path length: ');
  If Distance[B] <> Inf then
  Begin
    Write (Distance[B],' ');
  End
  Else
    Write ('rout unavaible ');
  Writeln;

//output address of shortest path
  Write ('cities passed: ', Cities[B]);
  Readln
End.
