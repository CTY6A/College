Program R2_Encoding;
//program encrypts text from text file 'Text', by method described
//in file 'Encryption'

{$APPTYPE CONSOLE}

Uses
  SysUtils;

Var
  Ciphertext : array of string [255];

//Ciphertext - Ciphertext

  i, j, n, step, y, x, LineElNum, lgth : Integer;

//n - num of repetition
//step - step for matrix traversal
//y, x - coordinates
//LineElNum - Line Element Number
//lgth - length of distance traveled

  Side, Route : Char;

//Side - Side of world
//Route - direction of rotation

  CurS, Course, CurText : String ;

//CurS - Current String
//Course - encoding Course
//CurText - Current Text

  CurFile : Text;

//CurFile - Current File

  FirstChangeSign, SecondChangeSign : procedure (var i: Integer);

//FirstChangeSign, SecondChangeSign - variables to Change Sign

  FirstMove, SecondMove : procedure (var i, j: Integer);

//FirstMove, SecondMove - variables to matrix motion

(*Procedure for moves to side*)
Procedure Sideways (var x, y: Integer);
begin
//pointer moves to side
  x := x + step
end;

(*Procedure for moves rectilinearly*)
Procedure Rectilinearly (var x, y: Integer);
begin
//pointer moves rectilinearly
  y := y + step
end;

(*Procedure for Change sign of step*)
Procedure Change (var step: Integer);
begin
//Change sign of step
  step := step * (-1)
end;

(*Procedure for Keep sign of step*)
Procedure Keep (var step: Integer);
begin
end;

Begin
//program assign with a text file with the text to read it and reads it
  Assign (CurFile, 'D:\Вадик\Разминочка\2\2\Text.txt');
  Reset (CurFile);
  while not Eof(CurFile) do
  begin

//text reads to variable CurText
    Readln (CurFile, Curs);
    CurText := Concat(CurText, curs)
  end;
  Close (CurFile);

//if root of num of characters in string is integer,
//then space for array size of this root is allocated
  if Frac (Sqrt(Length(CurText))) < 0.000000001 then
    SetLength (Ciphertext, trunc(Sqrt(Length(CurText))))

//else space is allocated for 1 greater than root
  else
    SetLength (Ciphertext, trunc(Sqrt(Length(CurText))) + 1);

//if root is odd, it increases it by one
  if not Odd ( Length (Ciphertext)) then
    SetLength (Ciphertext, Length (Ciphertext) + 1);

//if extra space was allocated - aligns the string with spaces
  if Length(CurText) <> Sqr (Length (Ciphertext)) then
    for i := 1 to Sqr (Length (Ciphertext))-Length(CurText) do
      CurText := CurText + ' ';

//program assign with a text file with the encryption to read it and reads it
  Assign (CurFile, 'D:\Вадик\Разминочка\2\2\Encryption.txt');
  Reset (CurFile);

//until the entire file is encoded, the program will encode it
  while not Eof(CurFile) do
  begin

//read data from file
    Readln (CurFile, Course);
    Readln (CurFile, n);

//initializing
    Side := Course[1];
    Route := Course[2];

//cycle to retry the encoding
    while n >0 do
    begin

//initializing
      if (Side = 'S') or (Side = 'E') then
        step := 1
      else
        step := -1;
      LineElNum := 1;
      y := Length (Ciphertext) div 2 ;
      x := y + 1;
      FirstMove := Sideways;
      SecondMove := Sideways;

//if side is south or north - FirstMove is Rectilinearly
      if (Side = 'S') or (Side = 'N') then
        FirstMove := Rectilinearly

//else - SecondMove is Rectilinearly
      else
        SecondMove := Rectilinearly;

//initializing data
       FirstChangeSign := Keep;
       SecondChangeSign := Keep;

//if movement is to east or west and forward or to north or south and back -
//second change of sign changes it
      if ((Side = 'E') or (Side = 'W')) and (Route = 'F') or
         ((Side = 'S') or (Side = 'N')) and (Route = 'B') then
        SecondChangeSign := Change

//else first change of sign changes it
      else
        FirstChangeSign := Change;

//initializing
      lgth := 1;

//while lgth less then length of Ciphertext - encrypt text
      while lgth < Length(Ciphertext) do
      begin

//cycle for first move
        for i := 1 to lgth do
        begin
          Ciphertext [y,x] := CurText [LineElNum];
          FirstMove(x, y);
          inc(LineElNum)
        end;

//change sign step
        FirstChangeSign(step);

//cycle for second move
        for j := 1 to lgth do
        begin
          Ciphertext [y,x] := CurText [LineElNum];
          SecondMove (x,y);
          inc (LineElNum)
        end;

//change sign step
        SecondChangeSign (step);
        lgth := lgth + 1
      end;

//after all movements you need to walk one more time
      for i := 1 to lgth do
      begin
        Ciphertext [y,x] := CurText [LineElNum];
        FirstMove(x, y);
        inc(LineElNum)
      end;

//initializing for next loop
      CurText:='';
      for i:=0 to Length (Ciphertext) - 1 do
        for j:=1 to Length (Ciphertext) do
          CurText := Concat (CurText, Ciphertext[i,j]);
      n:= n -1;
    end;
  end;
  Close (CurFile);

//data output
  for i := 0 to Length (Ciphertext) - 1 do
      for j := 1 to Length (Ciphertext) do
        Write (Ciphertext[i,j]);
  Readln
end.
