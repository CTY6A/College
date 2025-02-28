Program Addition;
//программа высчитывает сумму двух чисел (до 50 цифр)
//в любой системе счисления

{$APPTYPE CONSOLE}

Uses
  SysUtils;

Var
  NumInStr1,NumInStr2,Alp,Miss1,Miss2: String [50];
//NumInStr1, NumInStr1 - 1-ое, 2-ое число в строке
//ALp - алфавит системы счисления
//Miss1, Miss2 - промах по клавише

  NumInArray1,NumInArray2,Sum: Array of Integer;
//NumInArray1, NumInArray2 - число в массиве
//Sum - сумма

  i,Notation,Difference,Lgth,OneInTheMind: Integer;
//Notation - система счисления
//Difference - разница между числами
//Lgth - длина суммы
//OneInTheMind - "единица в уме"

  F1,FMiss1,FMiss2: Boolean;
//F1 - 1-ый флажок 
//FMiss1, FMiss2 - 1-ый и 2-ой флажок для промаха по клавише

Begin

//ввод и проверка основания системы счисления
  Repeat
  Write ('input Notation (more than 1, less than 37) ');
  Readln (Notation);
  If (Notation < 2) or (Notation > 36) then
    Writeln ('incorrect input data. try now ');
  Until (Notation>1) and (Notation<37);

//создание алфавита для выбранной системы счисления
  For i := 1 to Notation do
    If i <= 10 then
      Alp :=Alp + Char(47 + i)
    Else Alp := Alp + Char(54 + i);

//ввод чисел для суммы
  Write ('input 1st num ');
  Readln (NumInStr1);
  Write ('input 2nd num ');
  Readln(NumInStr2);

//инициализация параметров цикла
  FMiss1 := False;
  FMiss2 := False;

//циклы для поиска, удаления из строк и запоминания 
//ненужных символов
  i:=1;     
  While i <= Length(NumInStr1) do
    If Pos(NumInStr1[i],Alp) = 0 then
    Begin
      Miss1 := Miss1 + NumInStr1[i] + ' ';
      Delete (NumInStr1,i,1);
      FMiss1 := True
    End
    Else 
      i := i + 1;
  
  i := 1;    
  While i <= Length(NumInStr2) do
    If Pos(NumInStr2[i],Alp) = 0  then
    Begin
      Miss2 := Miss2 + NumInStr2[i] + ' ';
      Delete (NumInStr2,i,1);
      FMiss2 := true
    End
  Else i := i + 1;

//нахождение большей по размеру строки и разницы между ними
  If Length(NumInStr1) > Length(NumInStr2) then
    F1 := true
  Else F1 := false;
  Difference := Abs(Length(NumInStr1) - Length(NumInStr2));
  If F1 then
    Lgth := Length(NumInStr1)
  Else Lgth := Length(NumInStr2);

//уравнивание строк по числу символов (для подсчетов)
  For i := 1 to Difference do
    If F1 then
      Insert ('0',NumInStr2,1)
    Else Insert('0',NumInStr1,1);

//введение динамических массивов для чисел и присваивание //первого элемента
  SetLength(NumInArray1,Lgth+1);
  SetLength(NumInArray2,Lgth+1);
  NumInArray1[0] := 0;
  NumInArray2[0] := 0;

//цикл для перевода каждого элемента строки в номер его 
//позиции
// в созданном ранее алфавите для арифметических операций
  For i := 1 to Lgth do
  Begin
    NumInArray1[i] := pos(NumInStr1[i],Alp) - 1;
    NumInArray2[i] := pos(NumInStr2[i],Alp) - 1
  End;

//создание динамического массива для суммы и инициализация //цикла
  SetLength(Sum,Lgth + 1);
  OneInTheMind := 0;

//цикл для складывания каждого элемента числа по 
//отдельности
  For i := Lgth downto 0 do
  Begin
    Sum[i]:=(NumInArray1[i]+NumInArray2[i]+OneInTheMind) mod Notation;
    If ((NumInArray1[i] + NumInArray2[i]+OneInTheMind) div Notation) <> 0 then
      OneInTheMind := 1
    Else OneInTheMind := 0;
  End;

//устранение первого нуля (если таковой имеется) в сумме
  If Sum[0] = 0 then
  Begin
    For i := 0 to Lgth do
    Begin
     Sum[i] := Sum[i+1];
    End;
    Lgth := Lgth - 1 ;
  End;

//вывод результата
  Write ('sum: ');
  For i := 0 to Lgth  do
  Begin
  Write (Alp[Sum[i]+1]);
  End;
  Writeln;

//вывод ненужных символов
  If FMiss1 then
    Writeln('misspelling in 1st num: ', Miss1);
  If FMiss2 then
    Writeln('misspelling in 2nd num: ', Miss2);
  Readln
End.