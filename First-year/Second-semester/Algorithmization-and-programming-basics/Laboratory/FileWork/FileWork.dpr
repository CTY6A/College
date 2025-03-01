program FileWork;

{$APPTYPE CONSOLE}

uses
  SysUtils;

var
  x: Real = (-3) - 0.1;
  table: TextFile;

begin
  assign (table, 'D:\�����\����\2\5\Table.txt');
  rewrite (table);
  writeln (table, 'x:    cos(x):  exp(x):');
  repeat
    x := x + 0.1;
    writeln (table, x:5:2,' ', cos(x):7:4, ' ', exp(x))
  until int(x) = 3;
  close (table);
  readln
end.

