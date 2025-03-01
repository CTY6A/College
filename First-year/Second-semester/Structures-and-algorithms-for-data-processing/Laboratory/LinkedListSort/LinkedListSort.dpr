program LinkedListSort;

{$APPTYPE CONSOLE}

uses
  SysUtils;

Type
  Sub =^Profile;
  Profile=record
    FIO: string[20];
    Number: string[7];
    Next: Sub;
  end;

var
  CurSub,Listhead: Sub;
  direct: boolean;
  YN: char;

procedure SurnameSort(ListHead: Sub);
var
  name: string[20];
  exist: boolean;
  T: Sub;
begin
  write('Enter surname you needed: ');
  readln(name);
  T:=listhead;
  exist:=false;
  while T<>nil do
  begin
    if pos(name,T^.FIO)>0 then
    begin
      writeln(T^.FIO,' - ',T^.Number);
      exist:=true;
    end;

    T:=T^.Next
  end;
  if exist=false then
    writeln('There is no Profileon with such Number');
end;

procedure Numbersort(ListHead:Sub);
var
  number: string[7];
  exist:boolean;
  T:Sub;
begin
  write('Enter Number you needed: ');
  readln(number);
  T:=listhead;
  exist:=false;
  while T<>nil do
  begin
    if pos(number,T^.Number)>0 then
    begin
      writeln(T^.FIO,' - ',T^.Number);
      exist:=true;
    end;

    T:=T^.Next
  end;
  if exist=false then
    writeln('There is no Profileon with such Number');
end;

procedure Sorting(ListHead: Sub);
var
  CurSub,Max:Sub;
  T: Profile;

begin
  writeln;
  writeln('Sorting by surname:');
  CurSub:=ListHead;

  while CurSub^.Next <> nil do
  begin
    Max:= CurSub^.Next;
    while Max <> nil do
    begin
      if Max^.FIO < CurSub^.FIO then
      begin
        T.FIO:=Max^.FIO;
        T.Number:=Max^.Number;
        Max^.FIO:= CurSub^.FIO;
        Max^.Number:=CurSub^.Number;
        CurSub^.FIO:= T.FIO;
        CurSub^.Number:=T.Number;
      end;

      Max:= Max^.Next
    end;

    CurSub:= CurSub^.Next;
  end;

  CurSub := Listhead;


  repeat
    writeln(CurSub^.FIO,' - ',CurSub^.Number);
    CurSub := CurSub^.Next
  until CurSub = nil;


  writeln;
  writeln('Sorting by Numbers:');
  CurSub:=ListHead;

  while CurSub^.Next <> nil do
  begin
    Max:= CurSub^.Next;
    while Max <> nil do
    begin
      if Max^.Number < CurSub^.Number then
      begin
        T.FIO:=Max^.FIO;
        T.Number:=Max^.Number;
        Max^.FIO:= CurSub^.FIO;
        Max^.Number:=CurSub^.Number;
        CurSub^.FIO:= T.FIO;
        CurSub^.Number:=T.Number;
      end;

      Max:= Max^.Next
    end;

    CurSub:= CurSub^.Next
  end;

  CurSub := Listhead;

  repeat
    writeln(CurSub^.FIO,' - ',CurSub^.Number);
    CurSub := CurSub^.Next
  until CurSub = nil;

end;


begin
  direct:=true;
  ListHead:=nil;
  CurSub:=nil;
  while direct do
  begin
    if ListHead=nil then
    begin
      new(CurSub);
      CurSub^.Next:=nil;
      ListHead:=CurSub;
    end
    else
    begin
      new(CurSub^.Next);
      CurSub:=CurSub^.Next;
      CurSub^.Next:=nil;
    end;
    writeln;
    write('Enter tne surname: ');
    readln(CurSub^.FIO);
    write ('Enter tne Number: ');
    readln(CurSub^.Number);
    write('Do you want to continue?(Yes - Y; No - N): ');
    readln(YN);
    writeln;
    if YN='N' then
      direct:=false
  end;



  direct:=true;
  while direct do
  begin
    writeln;
    sorting(ListHead);
    writeln;
    write('Do you want to sort by surname(S) or by Number(N)? ');
    readln(YN);
    if YN='S' then
      SurnameSort(ListHead)
    else
      Numbersort(listHead);
    write('Do you want to continue?(Yes - Y; No - N): ');
    readln(YN);
    if YN='N' then
      direct:=false
  end;
  
   while ListHead <> nil do
   begin
     CurSub := ListHead^.Next;
     Dispose(ListHead);
     ListHead := CurSub
   end;

end.
