program Bacteria;

{$APPTYPE CONSOLE}

uses
  SysUtils;

var
  green,red,time,i: integer;

begin
  green := (-1);
  red := (-1);
  time := 0;
  i := 1;
  
  while (red < 0) or (green < 0) or (time <= 0) do
  begin
    write ('enter the number of red bacteria: '); 
    readln (red);
    write ('enter the number of green bacteria: ');
    readln (green);
    write ('enter the number of time: ');
    readln (time);
    
    if (red < 0) or (green < 0) or (time <= 0) then
       writeln ('incorrect input data. try again')
  end;
 
  while  (i <= time) and  (green >= 0) and (red >= 0)  do
  begin
    
    green := green + red;
    red := green - red;
    
    if (green >= 0) and (red >= 0) then
       writeln ('red: ',red,'; green: ',green,'; time: ',i);
    
    i := i + 1
  end;
  
  write ('these are the maximum possible values for your data');
  readln
end.

