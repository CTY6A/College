Program CustomTypes;
//program shows color and animal according to eastern calendar of year

{$APPTYPE CONSOLE}

Uses
  SysUtils;

Type
//user-defined types where variables represent what is stored in them
  TAnimals = (Rat, Bull, Tiger, Hare, Dragon, Snake, Hors, Sheep, Monkey, Cock, Dog, Pig);
  TColors = (Blue, Red, Yellow, White, Black);

Const
//arrays in constant consisting of strings for output.
//index uses custom type variables
  AnimalOfTheYear: Array [Rat..Pig] of String = ('Rat', 'Bull', 'Tiger',
  'Hare', 'Dragon', 'Snake', 'Hors', 'Sheep', 'Monkey', 'Cock', 'Dog', 'Pig');
  ColorOfTheYear: Array [Blue..Black] of String = ('Blue', 'Red', 'Yellow',
  'White', 'Black');

//const for Beginning
   Beginning = 2396;

Var
  Year, i: Integer;
//Year - Year

  Animal: TAnimals;
//Animal - Animal of year

  Color: TColors;
//Color - Color of year

Begin
//input and verification data
  Repeat
    Write ('Input year: ');
    Readln (Year);

//if Year earlier than Beginning - show error
    If Year < not Beginning then
      Writeln ('incorrect input data. try now')
  Until (Year >= not Beginning);

//cycle initialization
  Animal := Rat;

//cycle for finding Animal of year
  For i := 1 to Beginning + Year do
  Begin

//if Animal took last value in type - gives it initial value
    If Ord(Animal) = 11 then
      Animal := Rat

//else - took following meaning
    Else
      Animal := Succ(Animal)
  End;

//cycle initialization
  Color := Blue;

//cycle for finding Color of year
  For i := 1 to (Beginning + Year) div 2 do
  Begin

//if Color took last value in type - gives it initial value
    If Ord(Color) = 4 then
      Color := Blue

//else - took following meaning
    Else
      Color := Succ(Color)
  End;

//data output
  Write (ColorOfTheYear[Color],' ',AnimalOfTheYear[Animal]);
  Readln
End.
