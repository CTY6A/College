program Monks;

{$APPTYPE CONSOLE}

uses SysUtils;

var
  M,y: integer;       //M - Общее количество монахов 
                      //y - Временная переменная
 
  M1,M2,M3: integer;  //M1,M2,M3 - Количество монахов в
                      //каждой из групп
 
  N1,N2,N3, N,x: real;//N1,N2,N3 - Количество еды 
                      //съедаемой монахами
                      //первой, второй и третье группы
                      //N - Общее количество пирогов
                      //x - Временная переменная
  f1: boolean;        //f1 - 'флажок'

begin
    
                    {инициализация параметров циклов}
  f1 := false;
              
                    {ввод и одновременно проверка данных}
  repeat
    write ('Monks := ');
    Readln (M);
    write ('Cakes := ');
    readln (N);
               
                  {выведение на экран оповещения об ошибке, если таковая есть}
    if (M <= 0) or (N <= 0) then
      writeln ('incorrect input data');
  until (M > 0) and (N > 0);
  repeat
    write ('leading monks eat := ');
    readln (N1);
    write ('simple monks eat := ');
    readln (N2);
    write ('students eat := ');
    readln (N3);
                   
                   {выведение на экран оповещения об ошибке, если таковая есть}
    if (N1 < 0)or(N2<0)or(N3<0)or (N1 + N2 + N3 = 0) then
      writeln ('incorrect input data');
  until (N1 >= 0) and (N2 >= 0) and (N3 >= 0) and    (N1 + N2 + N3 <> 0);
                    
{основной цикл для поиска нескольких вариантов решения}
  for M1:=0 to M do
  begin
    if N3<>N2 then
    begin
                   
{формула для поиска количества монахов при N2 <> N3}
      x := ((N - (N1 * M1)) - (N2 * (M - M1))) /     (N3 - N2);
     
      //если перебираемое число подходит:
      //досчитываются необходимые значения и выводится //результат
      if (x - trunc(x) = 0) and (x >=0 ) and (M - x - M1 >= 0) then
      begin
        M3 := trunc(x);
        M2 := M - M1 - M3;
        
        //если есть хоть один возможный ответ:
        //'поднимается флажок'
        f1 := true;
        writeln ('leading monks := ',M1,' simple monks := ',M2,' students := ',M3)
      end
    end;
    if (N3 = N2) and (N1 <> N3) then
    begin
                  
                   {формула для поиска количества монахов при N1 <> N3 = N2}
      M2 := M1;
      x := ((N - (N2 * M2)) - (N1 * (M - M2))) / (N3 - N1);
      if (x - trunc(x) = 0) and (x >= 0) and (M - x - M2 >= 0) then
      begin
        M3 := trunc(x);
        y := M - M2 - M3;
        f1 := true;
        writeln ('leading monks := ',y,' simple monks := ',M2,' students := ',M3)
      end
    end;
    if (N3 = N2) and (N1 = N3) and (N1 > 0) then
    begin
                 
                    {формула для поиска количества монахов при N1 = N3 = N2 > 0}
      x := (N - M1 * (N2 + N3)) / N1;
      if (x - trunc(x) = 0) and (x >= 0) and (M - x - M1 >= 0) then
      begin
        M2 := trunc(x);
        M3 := M - M1 - M2
                   
                    {проверка результатов}
        if N = N1 * M1 + N2 * M2 + N3 * M3 then
        begin
          f1 := true;
          writeln('leading monks:= ',M1,' simple monks:= ',M2,' students:= ',M3)
        end
      end
    end
  end;
 
  //если нет ни одного варианта решения:
  //ошибка в вводимых данных(об этом сообщается 
//пользователю)
  //если же есть:
  //выводится напоминание о конце расчетов
  if f1 then
    write ('this and all')
  else
    write ('incorrect input data');
  readln
end.
