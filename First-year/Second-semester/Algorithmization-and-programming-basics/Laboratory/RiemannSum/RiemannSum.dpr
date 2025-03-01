Program RiemannSum;
{Программа для нахождения заданных интегралов
методом левых и центральных прямоугольников}

{$APPTYPE CONSOLE}

Uses
  SysUtils,Windows;

Type
  TIntegral = Function (const x:Real):Real;
  TGetIntegral =procedure (Integral:TIntegral;
                           const min,max,eps:Real);

Const
  min1=0.7;
  max1=2.1;
  min2=1.2;
  max2=2.6;  
  eps:array [1..3] of Real = (0.001,0.0001,0.00001);

Var
  i:Byte;

{Функция, задающая 1-ый интеграл}
Function Integral_1(const x:Real):Real;
begin
  Result:=Sqrt(0.6*x+1.5)/(2*x+sqrt(x*x+3))
end;

{Функция, задающая 2-ой интеграл}
Function Integral_2(const x:Real):Real;
begin
  Result:=1/sqrt(x*x+0.6)
end;

{Процедура для нахождения интеграла
 методом левых прямоугольников}
procedure GetIntegralLeft(Integral:TIntegral;
                          const min,max,eps:Real);
var
  h,Intg,Temp,R,j:Real;
  n,i:Integer;
begin
  n:=1;
  h:=(max-min)/n;   //Интервал разбиения
  Intg:=h*Integral(min+0*h);
  Temp:=Intg;
  R:=eps+1;
  while R >eps do     //вычисление до заданной точности
  begin
    n:=n*2;
    h:=(max-min)/n;
    Intg:=0;

    {Вычисление интеграла}
    for i:=0 to n-1 do
      Intg:=Intg+Integral(min+i*h);

    Intg:=Intg*h;
    R:=Abs(Temp-Intg);
    Temp:=Intg;
  end;

  {Вывод значения и числа отрезков разбиения}
  write('|',Intg:10:5,'|',n:5,'|');
end;

{Процедура для нахождения интеграла 
методом центральных прямоугольников}
procedure GetIntegralCenter(Integral:TIntegral ;
                            const min,max,eps:Real);
var
  h,Intg,Temp,R:Real;
  n,i:Integer;
begin
  n:=1;
  h:=(max-min)/n;     //Интервал разбиения
  Intg:=h*Integral(min+0*h+h/2);
  Temp:=Intg;
  R:=eps+1;
  while R > eps do //вычисление до заданной точности
  begin
    n:=n*2;
    h:=(max-min)/n;
    Intg:=0;

    {Вычисление интеграла}
    for i:=0 to n-1 do
     Intg:=Intg+Integral(min+i*h+h/2);

    Intg:=Intg*h;
    R:=Abs(Temp-Intg);
    Temp:=Intg;
  end;

  {Вывод значения и числа отрезков разбиения}
  write('|',Intg:10:5,'|',n:5,'|');
end;

//Вычисление первого интеграла заданным методом
procedure FirstIntegral(Proc:TGetIntegral;eps:real);
begin
  Proc(Integral_1,min1,max1,eps);
end;

//Вычисление второго интеграла заданным методом
procedure SecondIntegral(Proc:TGetIntegral;eps:real);
begin
  Proc(Integral_2,min2,max2,eps);
end;

Begin
  SetConsoleCP(1251);
  SetConsoleOutputCP(1251);
{Вывод ''шапки'' таблицы}
  writeln('|-----------------------------------------------               
            ---------------------------------|');
  writeln('|     |                       Метод левых 
          прямоугольников                        |');
  writeln('|  N  |------------------------------------------
           --------------------------------|');
  writeln('|     |  Eps  | Значение |  n  |  Eps  | Значение 
          |  n  |  Eps  | Значение |  n  |');
  writeln('|-----|----------------------------------------- 
            ---------------------------------|');
  write('|  1  |' );
{Расчет 1-го интеграла методом левых
прямоугольников для 3х точностей}
  for i:=1 to 3 do
  begin
    Write(eps[i]:7:5);
    FirstIntegral(GetIntegralLeft,eps[i]);
  end;
  writeln;
  write('|  2  |' );
{Расчет 2-го интеграла методом левых
прямоугольников для 3х точностей}
  for i:=1 to 3 do
  begin
    Write(eps[i]:7:5);
    SecondIntegral(GetIntegralLeft,eps[i]);
  end;
  writeln;
  writeln('|-----------------------------------------------
            ---------------------------------|');
  writeln('|     |                     Метод центральных 
           прямоугольников                    |');
  writeln('|  N  |------------------------------------------
            --------------------------------|');
  writeln('|     |  Eps  | Значение |  n  |  Eps  | Значение 
           |  n  |  Eps  | Значение |  n  |');
  writeln('|-----|-----------------------------------------
            ---------------------------------|');
  write('|  1  |' );
{Расчет 1-го интеграла методом центральных
прямоугольников для 3х точностей}
  for i:=1 to 3 do
  begin
    Write(eps[i]:7:5);
    FirstIntegral(GetIntegralCenter,eps[i]);
  end;
  writeln;
  write('|  2  |' );
{Расчет 2-го интеграла методом центральных
прямоугольников для 3х точностей}
  for i:=1 to 3 do
  begin
    Write(eps[i]:7:5);
    SecondIntegral(GetIntegralCenter,eps[i]);
  end;
  writeln;
  write('|-------------------------------------------------
          -------------------------------|');
  Readln;
End.
