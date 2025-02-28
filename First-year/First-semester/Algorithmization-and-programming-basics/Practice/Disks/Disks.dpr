program Disks;
//Программа выполняет следущее:
//Пользователь вводит количество дискет,
// а программа высчитывает три цены:
// 1) максимальная
// 2) "обычная"
// 3) "умная"цена с подсчетом бонусов и сэкономленных денег

{$APPTYPE CONSOLE}

Uses
  SysUtils;

Const
  kitprice = 1255;
  boxprice = 114.5;
  diskprice = 11.5;

Var
  kit, box, num_disk, disk, bonus: integer;
  price1, price2, price3, save_money: real;

Begin
  
//ввод и проверка вводимых данных
  repeat
    writeln('enter num disk: ');
    readln(num_disk);
    if num_disk < 0 then
      writeln('incorrect input data. try again');
  until num_disk > 0;

//вычислкние первой цены:                 1
  price1 := num_disk * diskprice;
  writeln('max price ', price1: 0: 2, ':');
  writeln('disk - ', num_disk);
  writeln;

//вычисление второй цены с помощью
//целочисленного деления:
  kit := num_disk div 144;
  box := (num_disk - kit * 144) div 12;
  disk := (num_disk - kit * 144) mod 12;
  price2 := kit * kitprice + box * boxprice + disk * diskprice;
  writeln('simple price ', price2: 0: 2, ':');
  writeln('kit - ', kit);
  writeln('box - ', box);
  writeln('disk - ', disk);
  writeln;

//вычисление третье цены:
//инициализация параметров циклов
  kit := 0;
  box := 0;
  disk := 0;

//цикл высчитывает количество ящиков
//котрое нужно купить так, чтобы оставалось
//не более 10 коробок
  while num_disk > 120 do
  begin
    num_disk := num_disk - 144;
    kit := kit + 1
  end;

//цикл высчитывает количество коробок
//котрое нужно купить так, чтобы оставалось
//не более 9 дискет
  while num_disk > 9 do
  begin
    num_disk := num_disk - 12;
    box := box + 1
  end;

//если дискет купленно больше чем нужно -
//это бонус. Иначе это количество дискет
  if num_disk < 0 then
    bonus := abs(num_disk)
  else
    disk := num_disk;
  price3 := kit * kitprice + box * boxprice + disk * diskprice;

//вычисление сэкономленных денег
  save_money := abs(price3 - price2);
  writeln('''smart'' price ', price3: 0: 2, ':');
  writeln('kit - ', kit);
  writeln('box - ', box);
  writeln('disk - ', disk);
  writeln('bonus - ', bonus);
  writeln('save money - ', save_money: 0: 2);
  readln
End.

