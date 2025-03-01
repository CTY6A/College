Program MinRadius;
//program finds min Radius of circle with center on abscissa
// covering all specified points

{$APPTYPE CONSOLE}

Uses
  SysUtils;

Type
  TCoordinates = Record
    X, Y : Integer;
  End;

//TCoordinates - type of record for storing Coordinates

(*Procedure for selecting direction*)
Procedure FirstStep (LeftBorder, RightBorder, High : Integer;

//LeftBorder - leftmost point
//RightBorder - righmost poin
//High - point of OX axis with largest Y

                     Var Step : Integer);

//Step - Step

begin

//if distance from highest point to Left Border is less than to Right Border -
//then program goes to Right Border, else - to Left Border.
//step size is equal to half the distance
  if High - LeftBorder < RightBorder - High then
    Step := (RightBorder - High) div 2
  else
    Step := (High - LeftBorder) div 2 * (-1);
end;

(*Function for 'turning' and decreasing Step*)
Function Turn (Flag : Boolean; Step : Integer) : Integer;

//Flag - Flag responsible for turn
//Step - Step

begin

//if Flag true - Step just decreases by half, else - also sign changes
  if Flag then
      Result := Step div 2
  else
      Result := Step div 2 * (-1);
end;

Var
  Points : Array of TCoordinates;

//Points - Array for Points

  LB, RB, i, MinPoint, NumOfPoints, H, S, Act : Integer;

//LB - Left Border
//RB - Righ Border
//MinPoint - center Point of circle
//NumOfPoints - Num Of Points
//H - point of OX axis with largest Y
//S - Step
//Act - num of Act's

  R, MinR : Real;

//R - current Radius
//MinR - Min Radius

  F1 : Boolean;

//F1- Flag


Begin

//input and verification of NumOfPoints
  Repeat
    Write ('input num of points: ');
    Readln(NumOfPoints);
    If NumOfPoints <= 0 then
      Writeln ('are you that stupid? num of points can''t be negative or zero. try again')
  Until (NumOfPoints > 0);

//creating array with Points
  SetLength(Points, NumOfPoints);

//filling array with Points
  For i := 0 to NumOfPoints - 1 do
    With Points[i] do
    Begin
      Writeln ('input points coordinates');
      Write ('X: ');
      Readln (X);
      Write ('Y: ');
      Readln (Y)
    End;

//initializing data
  LB := Points[0].X;
  RB := LB;
  H := Points[0].Y;

//cycle for finding LB and RB and H
  For i := 1 to NumOfPoints - 1 do
  Begin
    If LB > Points[i].X then
      LB := Points[i].X;
    If RB < Points[i].X then
      RB := Points[i].X;
    If H < Points[i].Y then
      H := Points[i].X
  End;

//call procedure FirstStep
  FirstStep (LB, RB, H, S);

//initializing cycle
  R := Sqrt (Sqr (Points[0].X - H) + Sqr (Points[0].Y));

//finding radius
  For i := 1 to NumOfPoints - 1 do
  If R < Sqrt (Sqr (Points[i].X - H) + Sqr (Points[i].Y)) then
    R := Sqrt (Sqr (Points[i].X - H) + Sqr (Points[i].Y));
  MinR := R;
  MinPoint := H;
  Act := 1;

//cycle for finding MinR of circle
  While S <> 0 do
  Begin
    H := H + S;
    R := Sqrt (Sqr (Points[0].X - H) + Sqr (Points[0].Y));

//finding radius

    For i := 1 to NumOfPoints - 1 do
    If R < Sqrt (Sqr (Points[i].X - H) + Sqr (Points[i].Y)) then
      R := Sqrt (Sqr (Points[i].X - H) + Sqr (Points[i].Y));

//if R less MinR then it is MinR, else no
    If R < MinR then
    Begin
      MinR := R;
      MinPoint := H;
      F1 := True
    End
    Else
      F1 := False;

//call function Turn
    S := Turn (F1, S);

//increment Act
    Act := Act + 1
  End;

//output data
  Write ('Radius: ', MinR:5:2,'; ', 'Axis Point: ', MinPoint, '; ', 'Num OF Act''s: ', Act);
  Read (Act)
End.
