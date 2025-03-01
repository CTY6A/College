Program InfixPostfix;
//program converts expression in infix entry to postfix

{$APPTYPE CONSOLE}

Uses
  SysUtils;
Const
  Operands = ['A'..'Z','a'..'z','0'..'9'];
  Operators = ['*','/','+','-','(',')','^'];

Var
  Expression, Steck, Misspelling: String;
//Expression - input Expression
//Steck - Steck

  FMiss, FLogic: Boolean;
//FMiss - Flag of Misspelling
//FLogic - Flag of Logic

  i: Integer;
  Rang: Integer = 0;


Begin
//input Expression
    Write ('input expression (in infix record): ');
    Readln (Expression);

//initializing cycle for typing errors
    FMiss := False;
    i := 1;

//cycle that scans each character and if it's not
//intended for work it deletes it
    While i <= Length(Expression) do

(*if symbol is not intended for operation - he is removed*)
      If not (Expression[i] in Operands) and not (Expression[i] in Operators)then
      Begin
        Misspelling := Misspelling + Expression[i] + ' | ';
        Delete (Expression, i, 1);
        FMiss := True
      End

(*else - 'go ahead'*)
      Else
        i := 1 + i;


    for i := 1 to Length(Expression) do
      if Expression[i] = '(' then Inc(Rang)
      else
        if Expression[i] = ')' then Dec(Rang);

    if Rang <> 0 then
      writeln('found extra brace');

//initializing cycle to search for logical errors
    FLogic := False;
    i := 2;

//cycle finds logical errors and eliminates them
    While i <= Length(Expression) do
    Begin

(*if sign is not multiplied before or after the parenthesis - it is placed*)
      If (Expression[i] = '(') and  (Expression[i - 1] in Operands) or
         (Expression[i] in Operands) and  (Expression[i - 1] = ')') then
      Begin
        Insert ('*', Expression, i);
        FLogic := True
      End;
      i := i + 1
    End;

//if there was a Mispelling, user is notified of changes
    If FMiss and (Rang = 0) then
    Begin
      Writeln ('found and removed following characters: ', Misspelling);
      Write ('if it suits you - enter 1, if not - any other figure: ');
      Readln (i);

(*if the user agrees with the changes -
'go ahead', otherwise - everything repeats*)
      If i = 1 then
        FMiss := False
    End;

//if there was logical error, user is notified of changes
    If FLogic and (Rang = 0) then
    Begin
      Writeln ('found and fixed bugs');
      Write ('if it suits you - enter 1, if not - any other figure: ');
      Readln (i);

(*if the user agrees with the changes -
'go ahead', otherwise - everything repeats*)
      If i = 1 then
        FLogic := False
    End;

   for i := 1 to Length(Expression) do
    if (Expression[i] in Operators) and (Expression[i] <> '(') and (Expression[i] <> ')') then
      Dec(Rang)
    else if Expression[i] in Operands then
      Inc(Rang);
//cycle is repeated until either user enters correct
//expression or agrees with program changes
  if not FMiss and not FLogic and (Rang = 1) then
  begin
  //initializing cycle
    Steck:= Steck + ' ';
    i:= 1;

  //cycle for converting expression in infix entry to postfix record
    While i <= Length(Expression) do
    Begin

  (*program looks at the value of single character*)
      Case Expression[i] of

  (*if it is bracket - it is stored in Steck and is replaced in expression ' '*)
      '(': Begin
             Delete (Expression, i, 1);
             if i <> 1 then
               If Expression[i - 1] <> ' ' then
               begin
                Insert (' ', Expression, i);
                i := 1 + i
               end;
  (*go ahead*)

             Steck := Steck + '('
           End;

  (*if this operand - simply 'go ahead'*)
      'A'..'Z','a'..'z','0'..'9': i := i + 1;

  (*if it is '+' or '-' - all operators from
  Steck with priority higher or the same*)
      '+','-': Begin

  (*until there are no more symbols left on stack, or until we reach bracket,
  we write out the symbols in expression*)
                 While (Steck[Length(Steck)] <> '(') and (Length(Steck) <> 1) do
                 Begin
                   Insert (' ' + Steck[Length(Steck)], Expression, i);
                   Delete (Steck, Length(Steck), 1);
                   i := i + 2
                 End;

  (*add operator in Steck and remove it from expression*)
                 Steck := Steck + Expression[i];
                 Delete (Expression,i,1);
                 Insert (' ', Expression, i);

  (*go ahead*)
                 i := 1 + i;
               End;

  (*if it is '*' or '/' - all operators from
  Steck with priority higher or the same*)
      '*', '/': Begin
                 while ((Steck[Length(Steck)]<> '+') or (Steck[Length(Steck)] = '-')) and (Length(Steck) <> 1) do
                 Begin
                   Insert(' ' + Steck[Length(Steck)], Expression, i);
                   Delete (Steck, Length(Steck), 1);
                   i := i + 2
                 End;

  (*add operator in Steck and remove it from expression*)
                 Steck := Steck + Expression[i];
                 Delete (Expression,i,1);
                 Insert (' ', Expression, i);
                 i := 1 + i;
               End;

  (* if this is ')' - all operators from
  Steck are output to expression before '('*)
      ')': Begin

  (*delete ')'*)
             Delete (Expression,i,1);

  (*output Steck to expression before '('*)
             While Steck[Length(Steck)] <> '(' do
             Begin
               Insert(' ' + Steck[Length(Steck)], Expression, i);
               Delete (Steck,Length(Steck), 1);
               i := i + 2;
             End;
             Delete (Steck,Length(Steck), 1)
           End;
      '^' : begin
              Delete (Expression,i,1);
              Insert(' ',Expression,i);
              inc(i);
              Steck := Steck + '^';
            end;
      End
    End;

  //delete extra character
    Delete (Steck,1, 1);

  //if there are operators left on stack - displays them
    While Length(Steck) <> 0 do
    Begin
      Expression := Expression + ' ' + Steck[Length(Steck)];
      Delete (Steck,Length(Steck), 1)
    End;

  //output data
    Writeln (Expression)
  end
  else Writeln('expression is''nt carroct');
  Writeln('rang = ' ,Rang);
  Readln
End.
