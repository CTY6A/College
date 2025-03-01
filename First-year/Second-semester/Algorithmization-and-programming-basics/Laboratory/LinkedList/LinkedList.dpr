program LinkedList;
//This program personifies the restaurant's menu.
//The menu consists of soups, snacks and desserts

{$APPTYPE CONSOLE}

uses
  SysUtils;

type
  TDish = record
    Name: String;
    Price: Real;
  end;
//TDish - this is a record consisting of
//the name of the dish and its price

  TNumAndPriceOfOrder = record
    NumOfOrder: Integer;
    Price: Real;
  end;
//TNumAndPriceOfOrder - this is a record consisting
//of the num of the order and its price

  TCategories = array [1..3] of TDish;
//TCategories - this is array of dishes

  TMenu = array [1..3] of TCategories;
//TMenu - this array of categories

  POrder = ^TOrder;
  TOrder = record
    OrderNum, TableNum: Integer;
    Dish: TDish;
    NumOfDishes: Integer;
    TotalPayment: Real;
    Next, Prev: POrder;
  end;
//dynamic chain consisting of links.
//the link, called the Order, consists of its number,
//the number of the table, the dish and the total amount

  TOrderList = record
    ListHead: POrder;
    ListTail: POrder;
  end;
//TOrderList - entry storing the beginning and end of the list

const
  Categories: array [1..3] of String = ('Soups', 'Cold Snacks', 'Desserts');
//Categories - array of Categories

  Menu: TMenu = (((Name: 'Borscht        '; Price: 1.69), (Name: 'Shchi          '; Price: 1.23), (Name: 'Rassolnik      '; Price: 1.70)),
                 ((Name: 'lettuce Olivier'; Price: 1.11), (Name: 'Russian salad  '; Price: 0.78), (Name: 'Greek salad    '; Price: 2.15)),
                 ((Name: 'Ice Cream      '; Price: 1.28), (Name: 'Creme brulee   '; Price: 1.99), (Name: 'Rainbow        '; Price: 1.56)));
//Menu - array of Menu

var
  CurrOrder: POrder;
//CurrOrder - Current Order

  NumOfCategories, NumOfDish, NumOfServings, TopDish, NumOfOrder: Integer;
//NumOfCategories - Num Of Categories
//NumOfDish - Num Of Dish
//TopDish - Top Dish
//NumOfServings - Num Of Servings
//NumOfOrder - Num Of Order

  OrderList: TOrderList;
//OrderList - Order List

  Direction: Byte;
//Direction - Direction

  RatingOfDishes: array [1..3] of array [1..3] of Integer;
//RatingOfDishes - Rating Of Dishes

  ProfitableOrder: Real;
//ProfitableOrder - Profitable Order

  NumAndPricesOfOrders: array of TNumAndPriceOfOrder;
//NumAndPricesOfOrders - array of Num And Price Of Order

  NumAndPriceOfOrder: TNumAndPriceOfOrder;
//NumAndPriceOfOrder - Num And Price Of Order

  f1: Boolean;
//f1 - Flag

  i, j: Integer;

begin
//initializing array called RatingOfDishes
  for i := 1 to 3 do
    for j := 1 to 3 do
      RatingOfDishes[i, j] := 0;

//initializng loop
  Direction := 1;

//the beginning of the order entry cycle.
//in the beginning there is a choice from
//categories: soups, snacks and desserts.
//then, after choosing a category, dishes
//from this category are provided.
//then the program creates a link and writes
// the necessary data there.
//at the end, the user is asked whether to
// place a new order or complete the shift
  while Direction = 1 do
  begin
(*output Menu and input Num Of Categories*)
    writeln ('Menu:');
    for i := 1 to 3 do
      writeln (i,' - ',Categories [i]);
    write ('Select a category: ');
    readln (NumOfCategories);

(*output dishes from the chosen category and input Num Of Dish*)
    for i := 1 to 3 do
      with Menu[NumOfCategories, i] do
        writeln (i, ' - ', Name, '____', Price:0:2, 'BYR');
    write ('Select a dish: ');
    readln (NumOfDish);

(*input Num Of Servings*)
    write  ('Enter the number of servings: x');
    readln (NumOfServings);

(*adding the number of dishes in the Rating Of Dishes*)
    RatingOfDishes[NumOfCategories, NumOfDish] := RatingOfDishes[NumOfCategories, NumOfDish] + NumOfServings;

(*if there are no links yet - create a link and mark the beginning of the list*)
    if CurrOrder = nil then
      begin
{creation of a link}
        New(CurrOrder);

{initializing of the link and Order List}
        CurrOrder^.Prev := nil;
        OrderList.ListHead := CurrOrder;

{initializng Order Num and Num And Prices Of Orders}
        CurrOrder^.OrderNum := 1;
        SetLength (NumAndPricesOfOrders, 1)
      end

(*otherwise, a new link is created and everything goes on as usual*)
    else
      begin
{creation of a link}
        New(CurrOrder^.Next);

{initializing of the link}
        CurrOrder^.Next^.Prev := CurrOrder;
        CurrOrder := CurrOrder^.Next;

{initializng Order Num and Num And Prices Of Orders}
        CurrOrder^.OrderNum := CurrOrder^.Prev^.OrderNum + 1;
        SetLength (NumAndPricesOfOrders, Length(NumAndPricesOfOrders) + 1)
      end;

(*input Table Num, Dish, Num Of Dishes, Total Payment, Num And Prices Of Orders*)
    CurrOrder^.TableNum := random (10) + 1;
    CurrOrder^.Dish := Menu[NumOfCategories, NumOfDish];
    CurrOrder^.NumOfDishes := NumOfServings;
    CurrOrder^.TotalPayment := CurrOrder^.Dish.Price * NumOfServings;
    NumAndPricesOfOrders[Length(NumAndPricesOfOrders) - 1].Price := CurrOrder^.TotalPayment;
    NumAndPricesOfOrders[Length(NumAndPricesOfOrders) - 1].NumOfOrder := CurrOrder^.OrderNum;

(*user are choosing - New ORder or End of Shift*)
    writeln ('Select an action:');
    writeln ('1 - New Order');
    writeln ('2 - End of Shift');
    readln  (Direction)
  end;

//completion of the list
    CurrOrder^.Next := nil;
    OrderList.ListTail := CurrOrder;

//cycle to find the most popular dish in each category
  for NumOfCategories := 1 to 3 do
  begin
(*initializing Top Dish*)
    TopDish := 1;

(*cycle to find the most popular dish in each category*)
    for NumOfDish := 2 to 3 do
    begin
{if Top Dish less popular then Current Dish then Top Dish - Current Dish}
      if RatingOfDishes[NumOfCategories, TopDish] < RatingOfDishes[NumOfCategories, NumOfDish] then
        TopDish := NumOfDish
    end;

(*if Rating Of Dishes not equal to zero then output it*)
    if RatingOfDishes[NumOfCategories, TopDish] <> 0 then
      writeln ('Best dish in category ', Categories[NumOfCategories], ': ', Menu[NumOfCategories, TopDish].Name);
  end;

(*initializing loop*)
    CurrOrder := OrderList.ListHead;
    ProfitableOrder := CurrOrder^.TotalPayment;
    NumOfOrder := 1;

(*cycle to find the most profitable order*)
    while CurrOrder^.Next <> nil do
    begin
{if Total Payment of Next Order more then Current -
Profitable Order if changing}
      if ProfitableOrder < CurrOrder^.Next^.TotalPayment then
      begin
        ProfitableOrder := CurrOrder^.Next^.TotalPayment;
        NumOfOrder := CurrOrder^.Next^.OrderNum
      end;

{transition to the next link}
      CurrOrder := CurrOrder^.Next
    end;

(*ouptput Most Profitable Order*)
    writeln ('Number of the most profitable order: ', NumOfOrder);

//initializin loop
  f1 := True;

//if length of NumAndPricesOfOrders more then 1 - go next
  if length(NumAndPricesOfOrders) > 1 then

//bubble sorting cycle with flag
//while f1 - true do sorting
    while f1 do
    begin
(*initializing flag*)
      f1 := False;

(*bubble sorting cycle*)
      for i := 0 to length(NumAndPricesOfOrders) - 2 do
        if  NumAndPricesOfOrders[i].Price < NumAndPricesOfOrders[i + 1].Price then
        begin

{variable switch}
          NumAndPriceOfOrder := NumAndPricesOfOrders[i + 1];
          NumAndPricesOfOrders[i + 1] := NumAndPricesOfOrders[i];
          NumAndPricesOfOrders[i] := NumAndPriceOfOrder;

{change flag}
          f1 := True
        end
    end;

//output order numbers in descending order
  write ('Order numbers in descending order: ', NumAndPricesOfOrders[0].NumOfOrder);
  for i := 1 to length(NumAndPricesOfOrders) - 1 do
    write (', ', NumAndPricesOfOrders[i].NumOfOrder);

//deleting a dynamic list  
  with OrderList do
    while ListHead <> nil do
    begin
      CurrOrder := ListHead^.Next;
      Dispose(ListHead);
      ListHead := CurrOrder
    end;

  readln
end.
