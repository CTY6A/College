Program MathFunction;
//расчет заданной функции для
//n := 10 .. 15; x := 0.6 (0.25) 1.1

{$APPTYPE CONSOLE}

Uses
  SysUtils;

Var
  m1, m2 , n: integer;             
          {m1,m2 - переменные для вычисления степени}
          {n - n из постановки}
 
  f, x, t1, t2, gr, sum: real;         
          {f,х - f,х из постановки}
          {t1,t2 - временные переменные для х}
          {sum - алгебраическая сумма}

Begin
          (*инициализация цикла А1*)
  x := 0.6; gr := 1.1                      
          
          (*цикл с предусловием А1 для перебора х*)
while (x <= gr) do             
  begin
    sum := 0;
         
          (*инициализация параметров цикла*)
    t1 := x;
    t2 := 1;
          
          (*цикл для перебора n*)
    for n := 1 to 9 do          
    begin
          
          (*возведение в степень без отдельного цикла*)
      if n > 1 then    
      begin
        t1 := t1 * sqr(x);
        t2 := t2 * x;
      end;  
      sum := sum + (t1 +1 / 7)/sqrt(exp(x / n) + exp(1 / n * ln(t2)))
    end;
    for n : =10 to 15 do
    begin
      t1 := 1;
      t2 := 1;                     
          
          (*возведение в степень с помощью цикла*)
      for m1 := 1 to (2 * n) - 1 do
      t1 := t1 * x;
      for m2 := 1 to (n - 1) do
      t2 := t2 * x;
      sum := sum +(t1 + 1 / 7)/sqrt(exp(x / n) + exp(1 / n * ln(t2)));
      f := arctan(x) - sum;
        
          (*вывод результатов*)
      writeln ('n=', n, ' x=', x:0:2,' f=', f:0:7)
    end;
         
          (*модификация параметра цикла*)
    x := x + 0.25                  
  end;
  readln
End.
