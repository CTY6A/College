program PriorityQueue;

{$APPTYPE CONSOLE}

uses
  SysUtils;

type
  Kind = (d,o,v);

  Click = ^CPU;
  CPU = record
         Order: Kind;
         NumOfTime: Integer;
         Next, Prev: Click;
       end;



const
  FirstTask: array [1..7] of integer = (9,6,3,2,8,7,4);
  SecondTask: array [1..7] of Integer = (3,7,2,4,7,3,1);
  ThirdTask: array [1..8] of Integer = (4,5,6,4,2,3,4,5);
  FourthTask: array [1..8] of Integer = (3,5,4,2,1,6,8,3);
  FifthTask: array [1..8] of Integer = (2,3,2,1,4,2,3,4);
  SixthTask: array [1..8] of Integer = (3,2,1,2,3,4,3,4);

  Tact = 3;
  Output = 5;

procedure MainTask(var Task: Integer; var Op, Listtail: Click);

var
  i: Integer;

begin
  for i:=Task downto 1 do
  begin
    New(Op^.Next);
    Op^.Next^.Prev := Op;
    Op^.Next := Op;

    Op^.Order := d;
    Op^.NumOfTime := Op^.Prev^.NumOfTime + 1
  end;

  Op^.Next := nil;
  ListTail := Op;
end;
var
  Task: Integer;

  Op, ListHead, ListTail: Click;

begin
  Task:=8;
  New(Op);
  Op^.Prev:=nil;
  Op^.Next:=nil;
  Op^.NumOfTime:=1;
  Op^.Order:=d;
  ListHead:=Op;
  ListTail:=Op;
  MainTask(Task, Op, ListTail);

  while ListHead <> nil do
  begin
    Op:=ListHead^.Next;
    Dispose(ListHead);
    ListHead:=Op
  end;

end.
