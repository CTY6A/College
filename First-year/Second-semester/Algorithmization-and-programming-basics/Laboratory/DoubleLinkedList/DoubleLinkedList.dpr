program DoubleLinkedList;

{$APPTYPE CONSOLE}

uses
  SysUtils;

type
  PRealNum = ^TRealNum;
  TRealNum = record
    Number: Integer;
    Value: Real;
    Next, Prev: PRealNum;
  end;

  TListState = record
    ListHead: PRealNum;
    ListTail: PRealNum;
  end;

var
  CurrNum: PRealNum;
  ListState: TListState;
  Num: Real;
  N, i: Integer;
  f1: Boolean;
  Direction: Byte;

begin
  write ('Enter the number N: ');
  readln (N);

  write ('Enter the Num: ');
  readln (Num);

  New(CurrNum);
  CurrNum^.Prev := nil;
  ListState.ListHead := CurrNum;
  CurrNum^.Number := 1;
  CurrNum^.Value := Num;

  for i := 2 to N * 2 do
  begin
    write ('Enter the Num: ');
    readln (Num);

    New(CurrNum^.Next);
    CurrNum^.Next^.Prev := CurrNum;
    CurrNum := CurrNum^.Next;

    CurrNum^.Number := CurrNum^.Prev^.Number + 1;
    CurrNum^.Value := Num;
  end;

  CurrNum^.Next := nil;
  ListState.ListTail := CurrNum;

  f1 := True;
  for i := 1 to N - 1 do
    if f1 then
    begin
      CurrNum := ListState.ListHead;
      while CurrNum^.Number <> N - i do
        CurrNum := CurrNum^.Next;
      Num := CurrNum^.Value * 2;

      CurrNum := ListState.ListHead;
      while CurrNum^.Number <> 2 * N - i + 1 do
        CurrNum := CurrNum^.Next;
      Num := Num + CurrNum^.Value;

      CurrNum := ListState.ListHead;
      while CurrNum^.Number <> i do
        CurrNum := CurrNum^.Next;
      if Num <> CurrNum^.Value then
        f1 := False
    end;


  with ListState do
  begin
    while ListHead <> nil do
    begin
      CurrNum := ListHead^.Next;
      Dispose(ListHead);
      ListHead := CurrNum
    end
  end;

  writeln (f1);
  readln
end.
